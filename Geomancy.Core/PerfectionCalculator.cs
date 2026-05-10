using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace GeomancyApp
{
    public static class PerfectionCalculator
    {
        /*  Eight mobile figures (first word only).  */
        private static readonly HashSet<string> MobileFigures =
            new HashSet<string>(
                new[]
                {
                    "Via", "Conjunctio", "Cauda", "Fortuna Minor", "Laetitia",
                    "Amissio", "Puer", "Rubeus"
                },
                StringComparer.OrdinalIgnoreCase);

        // Speed hierarchy for translation (Moon > Mercury > Venus > Sun > Mars > Jupiter > Saturn)
        private static readonly Dictionary<string, int> SpeedOrder = new Dictionary<string, int>
        {
            {"Via", 1},           // Moon
            {"Cauda", 2},         // Mercury  
            {"Puella", 3},        // Venus
            {"Fortuna Minor", 4}, // Sun
            {"Puer", 5},          // Mars
            {"Acquisitio", 6},    // Jupiter
            {"Carcer", 7},        // Saturn
            {"Conjunctio", 8},    // Saturnian
            {"Laetitia", 9},      // Saturnian
            {"Amissio", 10},      // Saturnian
            {"Rubeus", 11}        // Saturnian
        };

        // ── NEW ──  Return every perfection in the chart
        public static IEnumerable<PerfectionResult> FindAll(HouseChart chart)
        {
            var list = new List<PerfectionResult>();

            // test every ordered pair (Q, X) once
            for (int q = 1; q <= 12; q++)
                for (int x = q + 1; x <= 12; x++)
                {
                    var pr = Find(chart, q, x);
                    if (pr.Mode != PerfectionType.None)
                    {
                        pr.QuerentHouse = q;
                        pr.QuesitedHouse = x;
                        list.Add(pr);
                    }
                }
            return list;
        }

        public static PerfectionResult Find(HouseChart chart,
                                            int querentHouse,
                                            int quesitedHouse)
        {
            var res = new PerfectionResult();
            var Q = chart.GetHouseFigure(querentHouse);
            var X = chart.GetHouseFigure(quesitedHouse);

            if (Q == null || X == null)
            {
                res.Notes.Add("Chart not complete – significators missing.");
                return res;
            }

            // Declare figure names once for reuse throughout the method
            var querentFigureName = Root(Q.Name);
            var quesitedFigureName = Root(X.Name);

            /* 1 — Occupation */
            // Occupation occurs when the same figure appears in both the querent's and quesited's houses
            if (querentFigureName.Equals(quesitedFigureName, StringComparison.OrdinalIgnoreCase))
            {
                res.Mode = PerfectionType.Occupation;
                res.Notes.Add($"Both significators are {querentFigureName} (occupation).");
                AddInterpretationTip(res, chart, querentHouse, quesitedHouse);
                return res;
            }

            /* 2 — Conjunction */
            // Conjunction occurs when one significator moves to a house adjacent to the other significator's house
            // Check if querent's figure appears in a house adjacent to quesited's house
            for (int h = 1; h <= 12; h++)
            {
                if (h == querentHouse) continue; // Skip the querent's own house
                var houseFigure = chart.GetHouseFigure(h);
                if (houseFigure != null && Root(houseFigure.Name).Equals(querentFigureName, StringComparison.OrdinalIgnoreCase))
                {
                    if (AreAdjacentHouses(h, quesitedHouse))
                    {
                        res.Mode = PerfectionType.Conjunction;
                        res.Notes.Add($"Querent figure {querentFigureName} in house {h} is adjacent to quesited's house {quesitedHouse}.");
                        AddInterpretationTip(res, chart, querentHouse, quesitedHouse);
                        return res;
                    }
                }
            }

            // Check if quesited's figure appears in a house adjacent to querent's house
            for (int h = 1; h <= 12; h++)
            {
                if (h == quesitedHouse) continue; // Skip the quesited's own house
                var houseFigure = chart.GetHouseFigure(h);
                if (houseFigure != null && Root(houseFigure.Name).Equals(quesitedFigureName, StringComparison.OrdinalIgnoreCase))
                {
                    if (AreAdjacentHouses(h, querentHouse))
                    {
                        res.Mode = PerfectionType.Conjunction;
                        res.Notes.Add($"Quesited figure {quesitedFigureName} in house {h} is adjacent to querent's house {querentHouse}.");
                        AddInterpretationTip(res, chart, querentHouse, quesitedHouse);
                        return res;
                    }
                }
            }

            /* 3 — Favorable Aspect */
            // Favorable aspect occurs when one significator moves into a positive position (sextile or trine) 
            // relative to the other significator's house
            // Note: We do NOT check static aspects between houses - only aspects formed when significators "pass" to other houses
            
            // Check if querent's figure appears in other houses (Translation) and aspects the quesited's house
            // Use Dexter/Sinister logic to determine aspect type
            for (int h = 1; h <= 12; h++)
            {
                if (h == querentHouse) continue; // Skip the querent's own house
                var houseFigure = chart.GetHouseFigure(h);
                if (houseFigure != null && Root(houseFigure.Name).Equals(querentFigureName, StringComparison.OrdinalIgnoreCase))
                {
                    var (aspect, direction) = CalculateAspectWithDirection(h, quesitedHouse);
                    if (aspect == AspectType.Sextile || aspect == AspectType.Trine)
                    {
                        res.Mode = PerfectionType.Aspect;
                        res.AspectBetweenSignificators = aspect;
                        if (direction.Contains("Dexter")) res.AspectDirection = "Dexter";
                        else if (direction.Contains("Sinister")) res.AspectDirection = "Sinister";
                        else if (direction.Contains("Opposition")) res.AspectDirection = "Opposition";
                        res.Notes.Add($"Querent figure {querentFigureName} in house {h} (translation) aspects quesited's house {quesitedHouse} by {direction}.");
                        AddInterpretationTip(res, chart, querentHouse, quesitedHouse);
                        return res;
                    }
                }
            }
            
            // Check if quesited's figure appears in other houses (Translation) and aspects the querent's house
            // Use Dexter/Sinister logic to determine aspect type
            for (int h = 1; h <= 12; h++)
            {
                if (h == quesitedHouse) continue; // Skip the quesited's own house
                var houseFigure = chart.GetHouseFigure(h);
                if (houseFigure != null && Root(houseFigure.Name).Equals(quesitedFigureName, StringComparison.OrdinalIgnoreCase))
                {
                    var (aspect, direction) = CalculateAspectWithDirection(h, querentHouse);
                    if (aspect == AspectType.Sextile || aspect == AspectType.Trine)
                    {
                        res.Mode = PerfectionType.Aspect;
                        res.AspectBetweenSignificators = aspect;
                        if (direction.Contains("Dexter")) res.AspectDirection = "Dexter";
                        else if (direction.Contains("Sinister")) res.AspectDirection = "Sinister";
                        else if (direction.Contains("Opposition")) res.AspectDirection = "Opposition";
                        res.Notes.Add($"Quesited figure {quesitedFigureName} in house {h} (translation) aspects querent's house {querentHouse} by {direction}.");
                        AddInterpretationTip(res, chart, querentHouse, quesitedHouse);
                        return res;
                    }
                }
            }

            /* 4 — Translation of Light */
            // Translation occurs when a figure other than the two significators appears in houses 
            // adjacent to both the querent's and quesited's houses
            
            // Find all houses adjacent to querent's house
            var housesAdjacentToQuerent = new List<int>();
            for (int h = 1; h <= 12; h++)
            {
                if (h != querentHouse && AreAdjacentHouses(h, querentHouse))
                {
                    housesAdjacentToQuerent.Add(h);
                }
            }
            
            // Find all houses adjacent to quesited's house
            var housesAdjacentToQuesited = new List<int>();
            for (int h = 1; h <= 12; h++)
            {
                if (h != quesitedHouse && AreAdjacentHouses(h, quesitedHouse))
                {
                    housesAdjacentToQuesited.Add(h);
                }
            }
            
            // Check each house adjacent to querent for a translating figure
            foreach (var adjQuerentHouse in housesAdjacentToQuerent)
            {
                var translatorFigure = chart.GetHouseFigure(adjQuerentHouse);
                if (translatorFigure == null) continue;
                
                var translatorName = Root(translatorFigure.Name);
                
                // Translator must be different from both significators
                if (translatorName.Equals(querentFigureName, StringComparison.OrdinalIgnoreCase) ||
                    translatorName.Equals(quesitedFigureName, StringComparison.OrdinalIgnoreCase))
                    continue;
                
                // Check if this same figure also appears in a house adjacent to quesited
                foreach (var adjQuesitedHouse in housesAdjacentToQuesited)
                {
                    var figureInAdjQuesited = chart.GetHouseFigure(adjQuesitedHouse);
                    if (figureInAdjQuesited != null && 
                        Root(figureInAdjQuesited.Name).Equals(translatorName, StringComparison.OrdinalIgnoreCase))
            {
                res.Mode = PerfectionType.Translation;
                        res.TranslatorHouse = adjQuerentHouse; // Return the first translator house found
                        var translatorFullName = chart.GetHouseFigure(adjQuerentHouse)?.Name ?? translatorName;
                        res.Notes.Add($"{translatorFullName} in house {adjQuerentHouse} (adjacent to querent) and house {adjQuesitedHouse} (adjacent to quesited) translates the light.");
                        AddInterpretationTip(res, chart, querentHouse, quesitedHouse);
                return res;
                    }
                }
            }

            /* 5 — Mutation */
            // Mutation occurs when both significators pass to neighboring houses elsewhere in the chart
            
            // Find all houses where querent's figure appears (excluding querent's own house)
            var querentFigureHouses = new List<int>();
            for (int h = 1; h <= 12; h++)
            {
                if (h == querentHouse) continue;
                var houseFigure = chart.GetHouseFigure(h);
                if (houseFigure != null && Root(houseFigure.Name).Equals(querentFigureName, StringComparison.OrdinalIgnoreCase))
                {
                    querentFigureHouses.Add(h);
                }
            }
            
            // Find all houses where quesited's figure appears (excluding quesited's own house)
            var quesitedFigureHouses = new List<int>();
            for (int h = 1; h <= 12; h++)
            {
                if (h == quesitedHouse) continue;
                var houseFigure = chart.GetHouseFigure(h);
                if (houseFigure != null && Root(houseFigure.Name).Equals(quesitedFigureName, StringComparison.OrdinalIgnoreCase))
                {
                    quesitedFigureHouses.Add(h);
                }
            }
            
            // Check if any querent figure house is adjacent to any quesited figure house
            foreach (var qHouse in querentFigureHouses)
            {
                foreach (var xHouse in quesitedFigureHouses)
                {
                    if (AreAdjacentHouses(qHouse, xHouse))
            {
                res.Mode = PerfectionType.Mutation;
                        res.Notes.Add($"Both significators pass to neighboring houses: {querentFigureName} in house {qHouse} and {quesitedFigureName} in house {xHouse}.");
                        AddInterpretationTip(res, chart, querentHouse, quesitedHouse);
                return res;
                    }
                }
            }

            /* 6 — Company of Houses */
            // Company occurs when a significator is in company with the figure in its paired house,
            // and that figure in company can create perfection with the other significator
            var companyResult = CheckCompanyOfHouses(chart, querentHouse, quesitedHouse, Q, X);
            if (companyResult != null)
            {
                return companyResult;
            }

            /* — No classical perfection — */
            // Note: We do NOT record static aspects here - only aspects formed when significators "pass" to other houses
            res.Notes.Add("No classical mode of perfection found.");
            res.QuerentHouse = querentHouse;
            res.QuesitedHouse = quesitedHouse;
            return res;
        }

        // Helper method to find which house contains a given figure
        private static int GetHouseOfFigure(HouseChart chart, GeomanticFigure figure)
        {
            for (int i = 1; i <= 12; i++)
            {
                var houseFigure = chart.GetHouseFigure(i);
                if (houseFigure != null &&
                    Root(houseFigure.Name).Equals(Root(figure.Name), StringComparison.OrdinalIgnoreCase))
                    return i;
            }
            return 0; // not found
        }

        // Helper method to sanitize figure root names by removing hidden characters
        private static string Root(string name)
        {
            return name.Split(' ')[0].Trim().Trim('\u200b', '\u200c', '\uFEFF'); // strip BOM & zero-width spaces
        }

        // Helper method to add interpretation tips to notes based on perfection mode
        private static void AddInterpretationTip(PerfectionResult result, HouseChart chart, int querentHouse, int quesitedHouse)
        {
            string tip = "";
            
            switch (result.Mode)
            {
                case PerfectionType.Occupation:
                    tip = "Interpretation: Occupation — The same geomantic figure stands in both the querent's house and the quesited's house, which medieval and Renaissance handbooks treat as the clearest classical link between the parties. The matter tends to perfect in a direct, concentrated way rather than through long detours. Always temper the story with the figure's own dignity: a fortunate occupant usually reads as welcome fulfillment, while a difficult one can still grant the outcome in a form that later feels costly or tight.";
                    break;
                    
                case PerfectionType.Conjunction:
                    tip = "Interpretation: Conjunction — One significator has passed into a house immediately beside the other's home house, so the chart pictures literal proximity: meetings, messages, or someone stepping one door closer on the wheel. Traditional authors describe conjunction as nearly as affirmative as occupation, though it normally costs motion, initiative, or a short journey. Timing is relatively prompt compared with slower modes, yet the virtues of the two figures still tell you whether that contact is gentle or prickly.";
                    break;
                    
                case PerfectionType.Aspect:
                    if (result.AspectBetweenSignificators == AspectType.Sextile || result.AspectBetweenSignificators == AspectType.Trine)
                    {
                        if (result.AspectDirection == "Dexter")
                        {
                            tip = result.AspectBetweenSignificators == AspectType.Trine 
                                ? "Interpretation: Dexter Trine — Because a significator has sprung to another house, a trine forms on the dexter (backward) side of the chart, which geomancers following Greer's house-aspects table usually read as vigorous, outgoing grace. Help arrives with visible momentum: resources, introductions, or circumstances visibly swing toward agreement. It remains a harmonious link, so weigh the elemental friendship of the figures, but the geometry favors a generous, relatively quick resolution."
                                : "Interpretation: Dexter Sextile — The benefic sixty-degree link runs dexter—against the forward march of houses—so classical direction lore treats it as the more assertive sextile flavor. Openings tend to appear as active nudges, timely offers, or allies who move before you have to beg. It is still the milder benefic compared with a trine, yet you can expect quicker catalysts rather than a slow background drift toward the outcome.";
                        }
                        else if (result.AspectDirection == "Sinister")
                        {
                            tip = result.AspectBetweenSignificators == AspectType.Trine
                                ? "Interpretation: Sinister Trine — The same harmonious trine, cast sinister (forward along the house ring), is read as steady, even-tempered flow rather than a sudden flash. Benefits accrue on a natural timetable and often feel easy to ride once the pattern is noticed. Authors still rank it among the strong yes-style connections; think of dexter trines as brighter accelerants and sinister trines as deep currents that carry the matter quietly to shore."
                                : "Interpretation: Sinister Sextile — Here the favorable sextile follows the sinister direction, so help is present but gathers gradually or waits on your cooperation. You may need to request assistance, return calls, or repeat modest efforts before the door fully opens. The aspect remains kindly, yet classical nuance calls this the more passive sextile: patience and follow-through matter as much as raw opportunity.";
                        }
                        else
                        {
                            tip = "Interpretation: Benefic Aspect via Translation — A sextile or trine appears only because one significator has passed into another house, which geomantic doctrine treats as movement carrying the goodwill instead of a static promise between the original seats. That usually signals travel, rescheduling, a second venue, or someone operating out of sight who tilts the odds toward yes. Read the translated figure and the houses it touches to see who must relocate, speak, or mediate next.";
                        }
                    }
                    else if (result.AspectBetweenSignificators == AspectType.Square)
                    {
                        tip = result.AspectDirection == "Dexter"
                            ? "Interpretation: Dexter Square — The square is already the geomantic image of strain, and casting it dexter—backward around the wheel—traditionally sharpens the edge. Obstructions arrive with force: disputes spike quickly, rivals dig in, or a single stubborn factor refuses to be charmed away. Classical readers still acknowledge contact, but they warn that you must spend energy, money, or reputation to clear the path before the matter can settle cleanly."
                            : result.AspectDirection == "Sinister"
                            ? "Interpretation: Sinister Square — A forward-running square tends to manifest as grinding friction rather than a single dramatic clash: paperwork, mixed signals, stretched budgets, or morale that erodes while everyone stays polite. The denial is real yet often workable if you chip away at details, reset timelines, and refuse to ignore small warnings. Think of dexter squares as sudden snags and sinister squares as slow leaks that still demand attention."
                            : "Interpretation: Square — The significators meet by square, the ninety-degree stress aspect carried into geomancy from classical astrology but judged here only after movement creates the tie. It names tension, tests, or competing agendas that must be negotiated before anything can perfect. Severity still depends on the figures and supporting witnesses, yet you should plan for obstacles that must be managed, not hand-waved away.";
                    }
                    else if (result.AspectBetweenSignificators == AspectType.Opposition)
                    {
                        tip = "Interpretation: Opposition — Opposition spans six whole houses on the geomantic wheel, mirroring two parties staring across a table with contrary aims. Traditional doctrine treats it as the strongest malefic aspect: the querent and quesited remain aware of each other yet refuse to merge, so you often see stalemates, cancellations, or endings that will not convert into cooperation without a new figure or house story. It is rarely softened into a simple yes unless the rest of the chart loudly disagrees.";
                    }
                    break;
                    
                case PerfectionType.Translation:
                    var translatorName = result.TranslatorHouse > 0 ? chart.GetHouseFigure(result.TranslatorHouse)?.Name : "Unknown";
                    tip = $"Interpretation: Translation of Light — A third figure, {translatorName}, appears in houses adjacent to both significators, carrying light between them the way a courier ferries letters when principals cannot meet directly. Medieval instructions stress reading the translator's own character: favorable translators describe generous brokers, while difficult ones can perfect the matter through sharp lessons or unwelcome news. Weak translators sometimes mean odd luck or unlikely introductions, yet the geometry still insists that intermediaries—not solitary willpower—close the gap.";
                    break;
                    
                case PerfectionType.Mutation:
                    tip = "Interpretation: Mutation — Both significators have sprung into neighboring houses elsewhere on the chart, so the matter perfects only after each party relocates—literally or symbolically—into a new arena. Classical authors use mutation for unexpected channels: pay close attention to the new houses, because they name the friends, workplaces, or environments that will host the breakthrough. Timing is moderate and situational; you are steering toward the outcome by pivoting routes rather than hammering the old position.";
                    break;
                    
                case PerfectionType.Company:
                    if (result.MadeThroughCompany)
                    {
                        string companyNote = "Interpretation: Company of Houses — Traditional company pairs odd-even houses so that a companion figure beside your significator can speak for friends, kin, or business partners woven into the question. ";
                        if (result.BaseMode == PerfectionType.Aspect)
                        {
                            companyNote += result.AspectBetweenSignificators == AspectType.Sextile || result.AspectBetweenSignificators == AspectType.Trine
                                ? "Here that paired figure completes a helpful aspect, so allies or associates actively broker the benefic link, though the chain is slightly more indirect than raw occupation. Honor the companion figure's dignity: it shows whether helpers are steady patrons or enthusiastic amateurs who still sway the timetable."
                                : "Here the companion feeds a square or opposition, so the people beside you may import rivalry, mixed motives, or logistical drag even while they stay central to events. Classical company lore warns that associates can be the very channel the chart blames; read their figure carefully before trusting the outcome to teamwork alone.";
                        }
                        else
                        {
                            companyNote += "The companion now completes another classical mode—occupation, conjunction, translation, or mutation—so the answer cannot be told without that auxiliary voice in the room. Expect side conversations, family ties, or partners whose choices co-author the result; their figure's nature and elemental story deserve as much weight as the primary significators themselves.";
                        }
                        tip = companyNote;
                    }
                    break;
                    
                case PerfectionType.None:
                    tip = "Interpretation: Impedition — None of the major perfecting modes fire: no occupation, conjunction, benefic aspect, translation, mutation, or helpful company path ties the querent to the quesited on the wheel, which geomancers traditionally label impetition and read toward denial unless Judge and Witnesses strongly disagree. The chart is warning that indirect forces, wrong timing, or missing allies block the story you asked about. Reformulate the question, shift real-world conditions, or cast again later rather than forcing the same static pattern.";
                    break;
            }
            
            if (!string.IsNullOrEmpty(tip))
            {
                result.Notes.Add(tip);
            }
        }

        // Helper method to calculate zodiacal distance (0-11 forward distance)
        private static int ZodiacDistance(int from, int to)
        {
            int d = (to - from) % 12;
            return d < 0 ? d + 12 : d;
        }

        // Helper method to check if two houses are adjacent (next to each other in the circle)
        private static bool AreAdjacentHouses(int house1, int house2)
        {
            if (house1 == house2) return false; // Same house is not adjacent to itself
            int diff = Math.Abs(house1 - house2);
            // Houses are adjacent if they differ by 1, or if one is 1 and the other is 12 (wrapping around)
            return diff == 1 || (house1 == 1 && house2 == 12) || (house1 == 12 && house2 == 1);
        }

        // Helper method to check if translator is earlier than target (within 1-6 houses clockwise)
        private static bool EarlierThan(int translator, int target)
        {
            int d = (target - translator + 12) % 12;   // 1‥11
            return d > 0 && d <= 6;                      // 1‥6 = "earlier"
        }

        // Helper method to find translator with proper speed hierarchy and aspect restrictions
        private static int FindTranslator(HouseChart chart, int querentHouse, int quesitedHouse)
        {
            var Q = chart.GetHouseFigure(querentHouse);
            var X = chart.GetHouseFigure(quesitedHouse);
            
            if (Q == null || X == null) return -1;
            
            var querentFigure = Root(Q.Name);
            var quesitedFigure = Root(X.Name);
            
            var potentialTranslators = new List<(int house, int speed, string name)>();

            for (int h = 1; h <= 12; h++)
            {
                if (h == querentHouse || h == quesitedHouse) continue;

                var f = chart.GetHouseFigure(h);
                if (f == null) continue;

                string root = Root(f.Name);
                if (!MobileFigures.Contains(root)) continue;

                // Rule 4: Translator must be a different figure from both significators
                if (String.Equals(root, querentFigure, StringComparison.OrdinalIgnoreCase) ||
                    String.Equals(root, quesitedFigure, StringComparison.OrdinalIgnoreCase))
                    continue;

                // Rule 3: Translator must be earlier in zodiac than both significators
                if (!EarlierThan(h, querentHouse) || !EarlierThan(h, quesitedHouse))
                    continue;

                var a1 = GeomanticAspects.GetAspect(h, querentHouse);
                var a2 = GeomanticAspects.GetAspect(h, quesitedHouse);

                // Rule 2: Translator must aspect both significators by sextile or trine
                if ((a1 != AspectType.Sextile && a1 != AspectType.Trine) ||
                    (a2 != AspectType.Sextile && a2 != AspectType.Trine))
                    continue;

                // Rule 1: Translator must be mobile (already checked above)
                // Rule 5: Speed hierarchy (optional tiebreaker)
                int speed = SpeedOrder.ContainsKey(root) ? SpeedOrder[root] : 999;
                potentialTranslators.Add((h, speed, root));
            }

            // Return -1 if no translators found, otherwise return the fastest translator
            if (!potentialTranslators.Any()) return -1;
            
            return potentialTranslators
                .OrderBy(t => t.speed)   // fastest first
                .ThenBy(t => t.house)    // earliest house as tie-breaker
                .Select(t => t.house)
                .FirstOrDefault();
        }

        // Return every perfection in the chart for a specific pair
        public static IEnumerable<PerfectionResult> FindAll(HouseChart chart, int querentHouse, int quesitedHouse)
        {
            var list = new List<PerfectionResult>();
            var results = Find(chart, querentHouse, quesitedHouse, true); // true = return all modes
            if (results != null)
            {
                foreach (var pr in results)
                {
                    if (pr.Mode != PerfectionType.None)
                    {
                        pr.QuerentHouse = querentHouse;
                        pr.QuesitedHouse = quesitedHouse;
                        list.Add(pr);
                    }
                }
            }
            return list;
        }

        // Overload of Find that returns all perfection modes for a pair
        public static List<PerfectionResult> Find(HouseChart chart, int querentHouse, int quesitedHouse, bool returnAllModes)
        {
            if (!returnAllModes)
            {
                var singleResult = Find(chart, querentHouse, quesitedHouse);
                return new List<PerfectionResult> { singleResult };
            }
            
            // Return all perfections for this pair
            return FindAllPerfections(chart, querentHouse, quesitedHouse);
        }

        // Find all perfections (and aspects) for a specific querent/quesited pair
        private static List<PerfectionResult> FindAllPerfections(HouseChart chart, int querentHouse, int quesitedHouse)
        {
            var results = new List<PerfectionResult>();
            var Q = chart.GetHouseFigure(querentHouse);
            var X = chart.GetHouseFigure(quesitedHouse);

            if (Q == null || X == null)
            {
                var errorResult = new PerfectionResult();
                errorResult.Notes.Add("Chart not complete – significators missing.");
                return new List<PerfectionResult> { errorResult };
            }

            var querentFigureName = Root(Q.Name);
            var quesitedFigureName = Root(X.Name);
            
            // REMOVED: The static "directAspect" check. 
            // In Greer's system, static aspects do not create Perfection. 
            // The figure must MOVE (appear elsewhere) to create an aspect.

            // First, check if company exists to determine which houses to exclude from regular perfections
            int querentPairedHouse = GetPairedHouse(querentHouse);
            int quesitedPairedHouse = GetPairedHouse(quesitedHouse);
            var querentPairedFigure = chart.GetHouseFigure(querentPairedHouse);
            var quesitedPairedFigure = chart.GetHouseFigure(quesitedPairedHouse);
            bool querentInCompany = querentPairedFigure != null && AreInCompany(Q, querentPairedFigure);
            bool quesitedInCompany = quesitedPairedFigure != null && AreInCompany(X, quesitedPairedFigure);

            // --- 1. Occupation (Strongest, overrides movement rule) ---
            if (querentFigureName.Equals(quesitedFigureName, StringComparison.OrdinalIgnoreCase))
            {
                var res = new PerfectionResult
                {
                    Mode = PerfectionType.Occupation,
                    QuerentHouse = querentHouse,
                    QuesitedHouse = quesitedHouse
                };
                var querentFullName = chart.GetHouseFigure(querentHouse)?.Name ?? querentFigureName;
                res.Notes.Add($"Both significators are {querentFullName} (occupation).");
                AddInterpretationTip(res, chart, querentHouse, quesitedHouse);
                results.Add(res);
            }

            // --- 2. Conjunction (Requires Movement: checked inside the method) ---
            CheckConjunction(chart, querentHouse, quesitedHouse, querentFigureName, quesitedFigureName, results, querentInCompany, querentPairedHouse, quesitedInCompany, quesitedPairedHouse);

            // --- 3. Aspects (Requires Movement: Translated/Mutated positions only) ---
            // Note: We do NOT check static aspects between houses - only aspects formed when significators "pass" to other houses
            // CheckAllAspects finds aspects when significators appear in other houses (translation)
            CheckAllAspects(chart, querentHouse, quesitedHouse, querentFigureName, quesitedFigureName, results, querentInCompany, querentPairedHouse, quesitedInCompany, quesitedPairedHouse);

            // --- 4. Translation of Light ---
            CheckTranslation(chart, querentHouse, quesitedHouse, querentFigureName, quesitedFigureName, results);

            // --- 5. Mutation ---
            CheckMutation(chart, querentHouse, quesitedHouse, querentFigureName, quesitedFigureName, results);

            // --- 6. Company of Houses ---
            var companyResults = CheckAllCompanyOfHouses(chart, querentHouse, quesitedHouse, Q, X);
            results.AddRange(companyResults);

            // If no perfections OR aspects found, return a "None" result (triggering Impeditions)
            if (results.Count == 0)
            {
                var noResult = new PerfectionResult
                {
                    Mode = PerfectionType.None,
                    // We can still optionally log the static aspect for info, but it creates NO Mode.
                    AspectBetweenSignificators = GeomanticAspects.GetAspect(querentHouse, quesitedHouse), 
                    QuerentHouse = querentHouse,
                    QuesitedHouse = quesitedHouse
                };
                noResult.Notes.Add("No classical mode of perfection found.");
                AddInterpretationTip(noResult, chart, querentHouse, quesitedHouse);
                results.Add(noResult);
            }

            return results;
        }

        // Helper method to check for Conjunction
        private static void CheckConjunction(HouseChart chart, int querentHouse, int quesitedHouse, 
            string querentFigureName, string quesitedFigureName, List<PerfectionResult> results,
            bool querentInCompany, int querentPairedHouse, bool quesitedInCompany, int quesitedPairedHouse)
        {
            // Check if querent's figure appears in a house adjacent to quesited's house
            for (int h = 1; h <= 12; h++)
            {
                if (h == querentHouse) continue;
                // Skip the querent's paired house if company exists (it will be reported as company perfection)
                if (querentInCompany && h == querentPairedHouse) continue;
                
                var houseFigure = chart.GetHouseFigure(h);
                if (houseFigure != null && Root(houseFigure.Name).Equals(querentFigureName, StringComparison.OrdinalIgnoreCase))
                {
                    if (AreAdjacentHouses(h, quesitedHouse))
                    {
                        var res = new PerfectionResult
                        {
                            Mode = PerfectionType.Conjunction,
                            QuerentHouse = querentHouse,
                            QuesitedHouse = quesitedHouse
                        };
                        res.Notes.Add($"Querent figure {querentFigureName} in house {h} is adjacent to quesited's house {quesitedHouse}.");
                        AddInterpretationTip(res, chart, querentHouse, quesitedHouse);
                        results.Add(res);
                        return; // Only need one conjunction
                    }
                }
            }

            // Check if quesited's figure appears in a house adjacent to querent's house
            for (int h = 1; h <= 12; h++)
            {
                if (h == quesitedHouse) continue;
                // Skip the quesited's paired house if company exists (it will be reported as company perfection)
                if (quesitedInCompany && h == quesitedPairedHouse) continue;
                
                var houseFigure = chart.GetHouseFigure(h);
                if (houseFigure != null && Root(houseFigure.Name).Equals(quesitedFigureName, StringComparison.OrdinalIgnoreCase))
                {
                    if (AreAdjacentHouses(h, querentHouse))
                    {
                        var res = new PerfectionResult
                        {
                            Mode = PerfectionType.Conjunction,
                            QuerentHouse = querentHouse,
                            QuesitedHouse = quesitedHouse
                        };
                        var querentFullName = chart.GetHouseFigure(querentHouse)?.Name ?? querentFigureName;
                        var quesitedFullName = chart.GetHouseFigure(quesitedHouse)?.Name ?? quesitedFigureName;
                        res.Notes.Add($"Quesited figure {quesitedFullName} in house {h} is adjacent to querent's house {querentHouse} ({querentFullName}).");
                        AddInterpretationTip(res, chart, querentHouse, quesitedHouse);
                        results.Add(res);
                        return; // Only need one conjunction
                    }
                }
            }
        }

        // Helper method to calculate aspect type with Dexter/Sinister detection
        // Based on forward distance: diff = (target - h + 12) % 12
        private static (AspectType aspect, string direction) CalculateAspectWithDirection(int fromHouse, int toHouse)
        {
            // Calculate forward distance
            int diff = (toHouse - fromHouse + 12) % 12;
            
            // Sextile (60°)
            if (diff == 2)
                return (AspectType.Sextile, "Sinister Sextile (Forward)");
            if (diff == 10)
                return (AspectType.Sextile, "Dexter Sextile (Backward)");
            
            // Square (90°) [Malefic]
            if (diff == 3)
                return (AspectType.Square, "Sinister Square (Forward)");
            if (diff == 9)
                return (AspectType.Square, "Dexter Square (Backward)");
            
            // Trine (120°)
            if (diff == 4)
                return (AspectType.Trine, "Sinister Trine (Forward)");
            if (diff == 8)
                return (AspectType.Trine, "Dexter Trine (Backward)");
            
            // Opposition (180°) [Malefic]
            if (diff == 6)
                return (AspectType.Opposition, "Opposition");
            
            return (AspectType.None, "");
        }

        // Helper method to check for Aspects (both benefic and malefic) via Translation
        // Implements "Translation" logic: checks for significators in other houses
        private static void CheckAllAspects(HouseChart chart, int querentHouse, int quesitedHouse,
            string querentFigureName, string quesitedFigureName, List<PerfectionResult> results,
            bool querentInCompany, int querentPairedHouse, bool quesitedInCompany, int quesitedPairedHouse)
        {
            // Check if querent's figure appears in other houses (Translation) and aspects the quesited's house
            for (int h = 1; h <= 12; h++)
            {
                if (h == querentHouse) continue; // Skip the querent's own house
                if (querentInCompany && h == querentPairedHouse) continue;
                
                var houseFigure = chart.GetHouseFigure(h);
                if (houseFigure != null && Root(houseFigure.Name).Equals(querentFigureName, StringComparison.OrdinalIgnoreCase))
                {
                    var (aspect, direction) = CalculateAspectWithDirection(h, quesitedHouse);
                    if (aspect != AspectType.None)
                    {
                        var querentFullName = chart.GetHouseFigure(querentHouse)?.Name ?? querentFigureName;
                        var quesitedFullName = chart.GetHouseFigure(quesitedHouse)?.Name ?? quesitedFigureName;
                        var translatedFigureName = houseFigure.Name;
                        var res = new PerfectionResult
                        {
                            Mode = PerfectionType.Aspect,
                            AspectBetweenSignificators = aspect,
                            QuerentHouse = querentHouse,
                            QuesitedHouse = quesitedHouse
                        };
                        // Extract direction for mode display (Dexter, Sinister, or Opposition)
                        if (direction.Contains("Dexter"))
                            res.AspectDirection = "Dexter";
                        else if (direction.Contains("Sinister"))
                            res.AspectDirection = "Sinister";
                        else if (direction.Contains("Opposition"))
                            res.AspectDirection = "Opposition";
                        res.Notes.Add($"Querent figure {querentFullName} in house {h} (via translation) aspects quesited's house {quesitedHouse} ({quesitedFullName}) by {direction}.");
                        AddInterpretationTip(res, chart, querentHouse, quesitedHouse);
                        results.Add(res);
                    }
                }
            }

            // Check if quesited's figure appears in other houses (Translation) and aspects the querent's house
            for (int h = 1; h <= 12; h++)
            {
                if (h == quesitedHouse) continue; // Skip the quesited's own house
                if (quesitedInCompany && h == quesitedPairedHouse) continue;
                
                var houseFigure = chart.GetHouseFigure(h);
                if (houseFigure != null && Root(houseFigure.Name).Equals(quesitedFigureName, StringComparison.OrdinalIgnoreCase))
                {
                    var (aspect, direction) = CalculateAspectWithDirection(h, querentHouse);
                    if (aspect != AspectType.None)
                    {
                        var querentFullName = chart.GetHouseFigure(querentHouse)?.Name ?? querentFigureName;
                        var quesitedFullName = chart.GetHouseFigure(quesitedHouse)?.Name ?? quesitedFigureName;
                        var res = new PerfectionResult
                        {
                            Mode = PerfectionType.Aspect,
                            AspectBetweenSignificators = aspect,
                            QuerentHouse = querentHouse,
                            QuesitedHouse = quesitedHouse
                        };
                        // Extract direction for mode display (Dexter, Sinister, or Opposition)
                        if (direction.Contains("Dexter"))
                            res.AspectDirection = "Dexter";
                        else if (direction.Contains("Sinister"))
                            res.AspectDirection = "Sinister";
                        else if (direction.Contains("Opposition"))
                            res.AspectDirection = "Opposition";
                        res.Notes.Add($"Quesited figure {quesitedFullName} in house {h} (via translation) aspects querent's house {querentHouse} ({querentFullName}) by {direction}.");
                        AddInterpretationTip(res, chart, querentHouse, quesitedHouse);
                        results.Add(res);
                    }
                }
            }
        }

        // Helper method to check for Translation
        private static void CheckTranslation(HouseChart chart, int querentHouse, int quesitedHouse,
            string querentFigureName, string quesitedFigureName, List<PerfectionResult> results)
        {
            // Find all houses adjacent to querent's house
            var housesAdjacentToQuerent = new List<int>();
            for (int h = 1; h <= 12; h++)
            {
                if (h != querentHouse && AreAdjacentHouses(h, querentHouse))
                {
                    housesAdjacentToQuerent.Add(h);
                }
            }

            // Find all houses adjacent to quesited's house
            var housesAdjacentToQuesited = new List<int>();
            for (int h = 1; h <= 12; h++)
            {
                if (h != quesitedHouse && AreAdjacentHouses(h, quesitedHouse))
                {
                    housesAdjacentToQuesited.Add(h);
                }
            }

            // Check each house adjacent to querent for a translating figure
            foreach (var adjQuerentHouse in housesAdjacentToQuerent)
            {
                var translatorFigure = chart.GetHouseFigure(adjQuerentHouse);
                if (translatorFigure == null) continue;

                var translatorName = Root(translatorFigure.Name);

                // Translator must be different from both significators
                if (translatorName.Equals(querentFigureName, StringComparison.OrdinalIgnoreCase) ||
                    translatorName.Equals(quesitedFigureName, StringComparison.OrdinalIgnoreCase))
                    continue;

                // Check if this same figure also appears in a house adjacent to quesited
                foreach (var adjQuesitedHouse in housesAdjacentToQuesited)
                {
                    var figureInAdjQuesited = chart.GetHouseFigure(adjQuesitedHouse);
                    if (figureInAdjQuesited != null &&
                        Root(figureInAdjQuesited.Name).Equals(translatorName, StringComparison.OrdinalIgnoreCase))
                    {
                        var res = new PerfectionResult
                        {
                            Mode = PerfectionType.Translation,
                            TranslatorHouse = adjQuerentHouse,
                            QuerentHouse = querentHouse,
                            QuesitedHouse = quesitedHouse
                        };
                        res.Notes.Add($"{translatorName} in house {adjQuerentHouse} (adjacent to querent) and house {adjQuesitedHouse} (adjacent to quesited) translates the light.");
                        AddInterpretationTip(res, chart, querentHouse, quesitedHouse);
                        results.Add(res);
                        return; // Only need one translation
                    }
                }
            }
        }

        // Helper method to check for Mutation
        private static void CheckMutation(HouseChart chart, int querentHouse, int quesitedHouse,
            string querentFigureName, string quesitedFigureName, List<PerfectionResult> results)
        {
            // Find all houses where querent's figure appears (excluding querent's own house)
            var querentFigureHouses = new List<int>();
            for (int h = 1; h <= 12; h++)
            {
                if (h == querentHouse) continue;
                var houseFigure = chart.GetHouseFigure(h);
                if (houseFigure != null && Root(houseFigure.Name).Equals(querentFigureName, StringComparison.OrdinalIgnoreCase))
                {
                    querentFigureHouses.Add(h);
                }
            }

            // Find all houses where quesited's figure appears (excluding quesited's own house)
            var quesitedFigureHouses = new List<int>();
            for (int h = 1; h <= 12; h++)
            {
                if (h == quesitedHouse) continue;
                var houseFigure = chart.GetHouseFigure(h);
                if (houseFigure != null && Root(houseFigure.Name).Equals(quesitedFigureName, StringComparison.OrdinalIgnoreCase))
                {
                    quesitedFigureHouses.Add(h);
                }
            }

            // Check if any querent figure house is adjacent to any quesited figure house
            foreach (var qHouse in querentFigureHouses)
            {
                foreach (var xHouse in quesitedFigureHouses)
                {
                    if (AreAdjacentHouses(qHouse, xHouse))
                    {
                        var res = new PerfectionResult
                        {
                            Mode = PerfectionType.Mutation,
                            QuerentHouse = querentHouse,
                            QuesitedHouse = quesitedHouse
                        };
                        res.Notes.Add($"Both significators pass to neighboring houses: {querentFigureName} in house {qHouse} and {quesitedFigureName} in house {xHouse}.");
                        AddInterpretationTip(res, chart, querentHouse, quesitedHouse);
                        results.Add(res);
                        return; // Only need one mutation
                    }
                }
            }
        }

        // Helper method to get the paired house for a given house
        // Houses are paired: 1-2, 3-4, 5-6, 7-8, 9-10, 11-12
        private static int GetPairedHouse(int house)
        {
            if (house < 1 || house > 12) return 0;
            // If odd, return next (even); if even, return previous (odd)
            return house % 2 == 1 ? house + 1 : house - 1;
        }

        // Helper method to check if two figures are in company (any of the four types)
        private static bool AreInCompany(GeomanticFigure figure1, GeomanticFigure figure2)
        {
            if (figure1 == null || figure2 == null) return false;

            // Company simple: same figure
            if (Root(figure1.Name).Equals(Root(figure2.Name), StringComparison.OrdinalIgnoreCase))
                return true;

            // Company demi-simple: same planet (with special handling for Dragon nodes)
            if (AreInDemiSimpleCompany(figure1, figure2))
                return true;

            // Company compound: opposite figures
            if (AreOppositeFigures(figure1, figure2))
                return true;

            // Company capitular: same Fire line (HeadLine)
            if (figure1.HeadLine == figure2.HeadLine)
                return true;

            return false;
        }

        // Helper method to determine the CompanyType enum from two figures
        private static CompanyType GetCompanyType(GeomanticFigure figure1, GeomanticFigure figure2)
        {
            if (figure1 == null || figure2 == null) return CompanyType.None;

            // Company simple: same figure
            if (Root(figure1.Name).Equals(Root(figure2.Name), StringComparison.OrdinalIgnoreCase))
            {
                return CompanyType.Simple;
            }

            // Company demi-simple: same planet (with special handling for Dragon nodes)
            if (AreInDemiSimpleCompany(figure1, figure2))
            {
                return CompanyType.DemiSimple;
            }

            // Company compound: opposite figures
            if (AreOppositeFigures(figure1, figure2))
            {
                return CompanyType.Compound;
            }

            // Company capitular: same Fire line (HeadLine)
            if (figure1.HeadLine == figure2.HeadLine)
            {
                return CompanyType.Capitular;
            }

            return CompanyType.None;
        }

        // Helper method to get detailed company type information
        private static string GetCompanyTypeDescription(GeomanticFigure figure1, GeomanticFigure figure2)
        {
            if (figure1 == null || figure2 == null) return "";

            var companyType = GetCompanyType(figure1, figure2);
            
            switch (companyType)
            {
                case CompanyType.Simple:
                    // Strongest of the four. Greer treats matching figures in paired houses as
                    // a single voice spread across two seats: doubled force, perfectly aligned
                    // motives, and a sense that the helper and significator are essentially the
                    // same agency operating from two angles.
                    return "Company Simple (same figure) — the companion is identical to its partner, doubling the figure's voice and presenting one unified intent in the room. Read it as kindred spirits, mirrored agendas, or a single party operating from two seats.";
                case CompanyType.DemiSimple:
                    return GetDemiSimpleCompanyInfo(figure1, figure2);
                case CompanyType.Compound:
                    // The structural inverse pair (each line flipped). Classical authors describe
                    // it as opposites yoked together: complementary but tense, like spouses or
                    // business partners who balance each other while still pulling in distinct
                    // directions.
                    return "Company Compound (opposite figures) — the companion is the figure's structural inverse, every line flipped. Read it as complementary partners forced into the same arena: each completes the other yet imports its own contrary aim, so cooperation comes with friction.";
                case CompanyType.Capitular:
                    // Weakest of the four. The pair shares only the topmost Fire line (the
                    // "head"), which Greer ties to leadership/initiative. The connection is
                    // surface-level: people brought together by occasion or office rather than
                    // deep affinity.
                    return "Company Capitular (same Fire line / head line) — the companions share only their topmost Fire row, the line of initiative and outward face. Read it as a shallow but real bond: acquaintances joined by occasion, role, or shared cause rather than by deep temperament.";
                default:
                    return "";
            }
        }

        // Returns the short structural label from a CompanyTypeDescription, which
        // is the prose preceding the " — " separator (e.g., "Company Compound
        // (opposite figures)"). Used for the inline Notes mechanism line so the
        // bullet stays terse, while the full interpretive sentence remains
        // available on the PerfectionResult.CompanyTypeDescription field for the
        // detail panel to render.
        private static string GetCompanyTypeShortLabel(string description)
        {
            if (string.IsNullOrEmpty(description)) return string.Empty;
            var idx = description.IndexOf(" \u2014 ", StringComparison.Ordinal); // " — "
            return idx > 0 ? description.Substring(0, idx) : description;
        }

        // Helper method to get demi-simple company information with planet details
        private static string GetDemiSimpleCompanyInfo(GeomanticFigure figure1, GeomanticFigure figure2)
        {
            if (string.IsNullOrEmpty(figure1.Planet) || string.IsNullOrEmpty(figure2.Planet))
                return "";

            var name1 = Root(figure1.Name);
            var name2 = Root(figure2.Name);
            var planet1 = figure1.Planet;
            var planet2 = figure2.Planet;

            // Direct planet match: companions share the same planetary patron.
            // Read as kindred allies who cooperate by temperament, not by identity.
            if (planet1.Equals(planet2, StringComparison.OrdinalIgnoreCase))
            {
                return $"Company Demi-Simple (same planet: {planet1}) — the companions answer to the same planetary patron, so they pull in the same direction without being the same figure. Read it as friends, allies, or co-workers united by shared temperament rather than identical interests.";
            }

            // Special handling for Cauda Draconis (South node of the Moon).
            // Cauda carries malefic, releasing energy and is grouped in company
            // with Saturn (Carcer, Tristitia) and Mars (Puer, Rubeus): all three
            // share the temperament of restriction, ending, or sharp action.
            if (name1.Equals("Cauda", StringComparison.OrdinalIgnoreCase))
            {
                if (planet2.Equals("Saturn", StringComparison.OrdinalIgnoreCase))
                    return "Company Demi-Simple (Cauda Draconis with Saturn) — the South Node keeps company with Saturn's figures because both rule release, contraction, and endings. Read the helper as a sober, slow-moving force that closes doors as much as it opens them.";
                if (planet2.Equals("Mars", StringComparison.OrdinalIgnoreCase))
                    return "Company Demi-Simple (Cauda Draconis with Mars) — the South Node and Mars share the same scorching, cutting temperament. Read the helper as a sharp, decisive ally whose aid arrives quickly but can wound as easily as it heals.";
            }
            if (name2.Equals("Cauda", StringComparison.OrdinalIgnoreCase))
            {
                if (planet1.Equals("Saturn", StringComparison.OrdinalIgnoreCase))
                    return "Company Demi-Simple (Cauda Draconis with Saturn) — the South Node keeps company with Saturn's figures because both rule release, contraction, and endings. Read the helper as a sober, slow-moving force that closes doors as much as it opens them.";
                if (planet1.Equals("Mars", StringComparison.OrdinalIgnoreCase))
                    return "Company Demi-Simple (Cauda Draconis with Mars) — the South Node and Mars share the same scorching, cutting temperament. Read the helper as a sharp, decisive ally whose aid arrives quickly but can wound as easily as it heals.";
            }

            // Special handling for Caput Draconis (North node of the Moon).
            // Caput carries benefic, opening energy and is grouped in company
            // with Jupiter (Acquisitio, Laetitia) and Venus (Amissio, Puella):
            // all three share the temperament of growth, gain, and welcome.
            if (name1.Equals("Caput", StringComparison.OrdinalIgnoreCase))
            {
                if (planet2.Equals("Jupiter", StringComparison.OrdinalIgnoreCase))
                    return "Company Demi-Simple (Caput Draconis with Jupiter) — the North Node sides with Jupiter's figures because both signify increase, openings, and good fortune. Read the helper as a generous, expansive ally bringing growth, opportunity, or a welcome new chapter.";
                if (planet2.Equals("Venus", StringComparison.OrdinalIgnoreCase))
                    return "Company Demi-Simple (Caput Draconis with Venus) — the North Node sides with Venus's figures because both speak of attraction, harmony, and fresh starts. Read the helper as a kindly, magnetic ally whose presence smooths relationships and softens edges.";
            }
            if (name2.Equals("Caput", StringComparison.OrdinalIgnoreCase))
            {
                if (planet1.Equals("Jupiter", StringComparison.OrdinalIgnoreCase))
                    return "Company Demi-Simple (Caput Draconis with Jupiter) — the North Node sides with Jupiter's figures because both signify increase, openings, and good fortune. Read the helper as a generous, expansive ally bringing growth, opportunity, or a welcome new chapter.";
                if (planet1.Equals("Venus", StringComparison.OrdinalIgnoreCase))
                    return "Company Demi-Simple (Caput Draconis with Venus) — the North Node sides with Venus's figures because both speak of attraction, harmony, and fresh starts. Read the helper as a kindly, magnetic ally whose presence smooths relationships and softens edges.";
            }

            return "";
        }

        // Helper method to check company demi-simple (same planet, with special rules for Dragon nodes)
        // According to Table 6-2:
        // - Cauda Draconis is in company with Saturn (Carcer, Tristitia) and Mars (Puer, Rubeus)
        // - Caput Draconis is in company with Jupiter (Acquisitio, Laetitia) and Venus (Amissio, Puella)
        private static bool AreInDemiSimpleCompany(GeomanticFigure figure1, GeomanticFigure figure2)
        {
            if (string.IsNullOrEmpty(figure1.Planet) || string.IsNullOrEmpty(figure2.Planet))
                return false;

            var name1 = Root(figure1.Name);
            var name2 = Root(figure2.Name);
            var planet1 = figure1.Planet;
            var planet2 = figure2.Planet;

            // Direct planet match
            if (planet1.Equals(planet2, StringComparison.OrdinalIgnoreCase))
                return true;

            // Special handling for Cauda Draconis (South node of the Moon)
            // It's in company with Saturn and Mars figures
            if (name1.Equals("Cauda", StringComparison.OrdinalIgnoreCase))
            {
                if (planet2.Equals("Saturn", StringComparison.OrdinalIgnoreCase) ||
                    planet2.Equals("Mars", StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            if (name2.Equals("Cauda", StringComparison.OrdinalIgnoreCase))
            {
                if (planet1.Equals("Saturn", StringComparison.OrdinalIgnoreCase) ||
                    planet1.Equals("Mars", StringComparison.OrdinalIgnoreCase))
                    return true;
            }

            // Special handling for Caput Draconis (North node of the Moon)
            // It's in company with Jupiter and Venus figures
            if (name1.Equals("Caput", StringComparison.OrdinalIgnoreCase))
            {
                if (planet2.Equals("Jupiter", StringComparison.OrdinalIgnoreCase) ||
                    planet2.Equals("Venus", StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            if (name2.Equals("Caput", StringComparison.OrdinalIgnoreCase))
            {
                if (planet1.Equals("Jupiter", StringComparison.OrdinalIgnoreCase) ||
                    planet1.Equals("Venus", StringComparison.OrdinalIgnoreCase))
                    return true;
            }

            return false;
        }

        // Helper method to check if two figures are opposite (company compound)
        private static bool AreOppositeFigures(GeomanticFigure figure1, GeomanticFigure figure2)
        {
            var name1 = Root(figure1.Name);
            var name2 = Root(figure2.Name);

            // Company compound pairs from Table 6-2
            var oppositePairs = new HashSet<(string, string)>
            {
                ("Puer", "Puella"),
                ("Amissio", "Acquisitio"),
                ("Albus", "Rubeus"),
                ("Populus", "Via"),
                ("Fortuna Major", "Fortuna Minor"),
                ("Conjunctio", "Carcer"),
                ("Tristitia", "Laetitia"),
                ("Cauda Draconis", "Caput Draconis")
            };

            // Check both directions
            return oppositePairs.Contains((name1, name2)) || oppositePairs.Contains((name2, name1));
        }

        // Main method to check Company of Houses (returns all company perfections)
        // Company is ONLY checked between:
        // 1. The querent's house and its paired house (1-2, 3-4, 5-6, 7-8, 9-10, 11-12)
        // 2. The quesited's house and its paired house
        // We do NOT check company between all houses - only the paired houses for each significator
        private static List<PerfectionResult> CheckAllCompanyOfHouses(HouseChart chart, int querentHouse, int quesitedHouse, 
            GeomanticFigure querentFigure, GeomanticFigure quesitedFigure)
        {
            var results = new List<PerfectionResult>();

            // Check if querent's figure is in company with the figure in its paired house
            // Houses are paired: 1-2, 3-4, 5-6, 7-8, 9-10, 11-12
            int querentPairedHouse = GetPairedHouse(querentHouse);
            var querentPairedFigure = chart.GetHouseFigure(querentPairedHouse);
            
            if (querentPairedFigure != null && AreInCompany(querentFigure, querentPairedFigure))
            {
                // The figure in company acts as a second significator
                // Check if it can create perfection with the quesited
                var companyPerfection = FindPerfectionWithCompanyFigure(chart, querentPairedHouse, quesitedHouse, 
                    querentPairedFigure, quesitedFigure, true);
                if (companyPerfection != null)
                {
                    // Mark that this perfection is made through company
                    companyPerfection.MadeThroughCompany = true;
                    companyPerfection.BaseMode = companyPerfection.Mode;  // Store the underlying mode
                    companyPerfection.Mode = PerfectionType.Company;
                    companyPerfection.QuerentHouse = querentHouse;
                    companyPerfection.QuesitedHouse = quesitedHouse;
                    
                    // Set company type enum and description
                    companyPerfection.CompanyType = GetCompanyType(querentFigure, querentPairedFigure);
                    var companyTypeDescription = GetCompanyTypeDescription(querentFigure, querentPairedFigure);
                    companyPerfection.CompanyTypeDescription = companyTypeDescription;
                    
                    var companyNote = $"Company of houses: {Root(querentFigure.Name)} in house {querentHouse} is in company with {Root(querentPairedFigure.Name)} in house {querentPairedHouse}.";
                    var shortLabel = GetCompanyTypeShortLabel(companyTypeDescription);
                    if (!string.IsNullOrEmpty(shortLabel))
                    {
                        companyNote += $" Type: {shortLabel}.";
                    }
                    companyPerfection.Notes.Insert(0, companyNote);
                    AddInterpretationTip(companyPerfection, chart, querentHouse, quesitedHouse);
                    results.Add(companyPerfection);
                }
            }

            // Check if quesited's figure is in company with the figure in its paired house
            // Houses are paired: 1-2, 3-4, 5-6, 7-8, 9-10, 11-12
            int quesitedPairedHouse = GetPairedHouse(quesitedHouse);
            var quesitedPairedFigure = chart.GetHouseFigure(quesitedPairedHouse);
            
            if (quesitedPairedFigure != null && AreInCompany(quesitedFigure, quesitedPairedFigure))
            {
                // The figure in company (quesitedPairedFigure) acts as a second significator
                // Check if it can create perfection with the querent
                // Note: figure1 should be the company figure, figure2 should be the other significator (querent)
                var companyPerfection = FindPerfectionWithCompanyFigure(chart, quesitedPairedHouse, querentHouse,
                    quesitedPairedFigure, querentFigure, false);
                if (companyPerfection != null)
                {
                    // Mark that this perfection is made through company
                    companyPerfection.MadeThroughCompany = true;
                    companyPerfection.BaseMode = companyPerfection.Mode;  // Store the underlying mode
                    companyPerfection.Mode = PerfectionType.Company;
                    companyPerfection.QuerentHouse = querentHouse;
                    companyPerfection.QuesitedHouse = quesitedHouse;
                    
                    // Set company type enum and description
                    companyPerfection.CompanyType = GetCompanyType(quesitedFigure, quesitedPairedFigure);
                    var companyTypeDescription = GetCompanyTypeDescription(quesitedFigure, quesitedPairedFigure);
                    companyPerfection.CompanyTypeDescription = companyTypeDescription;
                    
                    var companyNote = $"Company of houses: {Root(quesitedFigure.Name)} in house {quesitedHouse} is in company with {Root(quesitedPairedFigure.Name)} in house {quesitedPairedHouse}.";
                    var shortLabel = GetCompanyTypeShortLabel(companyTypeDescription);
                    if (!string.IsNullOrEmpty(shortLabel))
                    {
                        companyNote += $" Type: {shortLabel}.";
                    }
                    companyPerfection.Notes.Insert(0, companyNote);
                    AddInterpretationTip(companyPerfection, chart, querentHouse, quesitedHouse);
                    results.Add(companyPerfection);
                }
            }

            return results;
        }

        // Legacy method for backward compatibility (returns first company perfection only)
        private static PerfectionResult CheckCompanyOfHouses(HouseChart chart, int querentHouse, int quesitedHouse, 
            GeomanticFigure querentFigure, GeomanticFigure quesitedFigure)
        {
            var allResults = CheckAllCompanyOfHouses(chart, querentHouse, quesitedHouse, querentFigure, quesitedFigure);
            return allResults.FirstOrDefault();
        }

        // Helper method to find perfection when a figure in company is used as a significator
        private static PerfectionResult FindPerfectionWithCompanyFigure(HouseChart chart, int companyHouse, int targetHouse,
            GeomanticFigure companyFigure, GeomanticFigure targetFigure, bool querentInCompany)
        {
            // 1. Occupation: same figure
            if (Root(companyFigure.Name).Equals(Root(targetFigure.Name), StringComparison.OrdinalIgnoreCase))
            {
                var res = new PerfectionResult();
                res.Mode = PerfectionType.Occupation;
                res.Notes.Add($"Figure in company {companyFigure.Name} occupies both houses {companyHouse} and {targetHouse}.");
                AddInterpretationTip(res, chart, companyHouse, targetHouse);
                return res;
            }

            var companyFigureName = Root(companyFigure.Name);

            // 2. Conjunction: figure in company appears adjacent to other significator's house
            for (int h = 1; h <= 12; h++)
            {
                if (h == companyHouse) continue;
                var houseFigure = chart.GetHouseFigure(h);
                if (houseFigure != null && Root(houseFigure.Name).Equals(companyFigureName, StringComparison.OrdinalIgnoreCase))
                {
                    if (AreAdjacentHouses(h, targetHouse))
                    {
                        var res = new PerfectionResult();
                        res.Mode = PerfectionType.Conjunction;
                        var companyFullName = companyFigure.Name;
                        var targetFullName = chart.GetHouseFigure(targetHouse)?.Name ?? Root(targetFigure.Name);
                        res.Notes.Add($"Figure in company {companyFullName} in house {h} is adjacent to house {targetHouse} ({targetFullName}).");
                        AddInterpretationTip(res, chart, companyHouse, targetHouse);
                        return res;
                    }
                }
            }

            // 3. Aspect: Check DIRECT aspect from the Company House first!
            // (This fixes the missing Square from House 2)
            var (directCompanyAspect, dir) = CalculateAspectWithDirection(companyHouse, targetHouse);
            if (directCompanyAspect != AspectType.None)
            {
                var res = new PerfectionResult();
                res.Mode = PerfectionType.Aspect;
                res.AspectBetweenSignificators = directCompanyAspect;
                res.MadeThroughCompany = true;
                // Extract direction for mode display (Dexter, Sinister, or Opposition)
                if (dir.Contains("Dexter"))
                    res.AspectDirection = "Dexter";
                else if (dir.Contains("Sinister"))
                    res.AspectDirection = "Sinister";
                else if (dir.Contains("Opposition"))
                    res.AspectDirection = "Opposition";
                var companyFullName = companyFigure.Name;
                var targetFullName = chart.GetHouseFigure(targetHouse)?.Name ?? Root(targetFigure.Name);
                res.Notes.Add($"Figure in company {companyFullName} in house {companyHouse} (direct) aspects house {targetHouse} ({targetFullName}) by {dir}.");
                AddInterpretationTip(res, chart, companyHouse, targetHouse);
                return res;
            }

            // 4. Aspect via Translation (Company Figure appearing elsewhere)
            for (int h = 1; h <= 12; h++)
            {
                if (h == companyHouse) continue;
                
                // CRITICAL FIX: Don't let the company figure circle back to the Paired House
                // (e.g. If checking House 2's company, don't use House 1 (the original querent) as a translator)
                int originalSignificatorHouse = GetPairedHouse(companyHouse);
                if (h == originalSignificatorHouse) continue;

                var houseFigure = chart.GetHouseFigure(h);
                if (houseFigure != null && Root(houseFigure.Name).Equals(companyFigureName, StringComparison.OrdinalIgnoreCase))
                {
                    var (aspect, direction) = CalculateAspectWithDirection(h, targetHouse);
                    if (aspect != AspectType.None)
                    {
                        var res = new PerfectionResult();
                        res.Mode = PerfectionType.Aspect;
                        res.AspectBetweenSignificators = aspect;
                        res.MadeThroughCompany = true;
                        // Extract direction for mode display (Dexter, Sinister, or Opposition)
                        if (direction.Contains("Dexter"))
                            res.AspectDirection = "Dexter";
                        else if (direction.Contains("Sinister"))
                            res.AspectDirection = "Sinister";
                        else if (direction.Contains("Opposition"))
                            res.AspectDirection = "Opposition";
                        var companyFullName = companyFigure.Name;
                        var targetFullName = chart.GetHouseFigure(targetHouse)?.Name ?? Root(targetFigure.Name);
                        res.Notes.Add($"Figure in company {companyFullName} in house {h} (translation) aspects house {targetHouse} ({targetFullName}) by {direction}.");
                        AddInterpretationTip(res, chart, companyHouse, targetHouse);
                        return res;
                    }
                }
            }

            // 5. Translation: figure in company appears adjacent to both houses
            var housesAdjacentToHouse1 = new List<int>();
            for (int h = 1; h <= 12; h++)
            {
                if (h != companyHouse && AreAdjacentHouses(h, companyHouse))
                    housesAdjacentToHouse1.Add(h);
            }
            var housesAdjacentToHouse2 = new List<int>();
            for (int h = 1; h <= 12; h++)
            {
                if (h != targetHouse && AreAdjacentHouses(h, targetHouse))
                    housesAdjacentToHouse2.Add(h);
            }

            foreach (var adjHouse1 in housesAdjacentToHouse1)
            {
                var translatorFigure = chart.GetHouseFigure(adjHouse1);
                if (translatorFigure == null) continue;
                var translatorName = Root(translatorFigure.Name);
                
                if (translatorName.Equals(companyFigureName, StringComparison.OrdinalIgnoreCase) ||
                    translatorName.Equals(Root(targetFigure.Name), StringComparison.OrdinalIgnoreCase))
                    continue;

                foreach (var adjHouse2 in housesAdjacentToHouse2)
                {
                    // Translator must be in two different houses
                    if (adjHouse1 == adjHouse2) continue;
                    
                    var figureInAdj2 = chart.GetHouseFigure(adjHouse2);
                    if (figureInAdj2 != null && 
                        Root(figureInAdj2.Name).Equals(translatorName, StringComparison.OrdinalIgnoreCase))
                    {
                        var res = new PerfectionResult();
                        res.Mode = PerfectionType.Translation;
                        res.TranslatorHouse = adjHouse1;
                        var translatorFullName = chart.GetHouseFigure(adjHouse1)?.Name ?? translatorName;
                        res.Notes.Add($"Figure in company enables translation: {translatorFullName} in houses {adjHouse1} and {adjHouse2}.");
                        AddInterpretationTip(res, chart, companyHouse, targetHouse);
                        return res;
                    }
                }
            }

            // 6. Mutation: figure in company and other significator pass to neighboring houses
            var companyFigureHouses = new List<int>();
            for (int h = 1; h <= 12; h++)
            {
                if (h == companyHouse) continue;
                // Exclude original significator to avoid circular logic
                int originalSignificatorHouse = GetPairedHouse(companyHouse);
                if (h == originalSignificatorHouse) continue;

                var houseFigure = chart.GetHouseFigure(h);
                if (houseFigure != null && Root(houseFigure.Name).Equals(companyFigureName, StringComparison.OrdinalIgnoreCase))
                    companyFigureHouses.Add(h);
            }

            var otherFigureName = Root(targetFigure.Name);
            var otherFigureHouses = new List<int>();
            for (int h = 1; h <= 12; h++)
            {
                if (h == targetHouse) continue;
                var houseFigure = chart.GetHouseFigure(h);
                if (houseFigure != null && Root(houseFigure.Name).Equals(otherFigureName, StringComparison.OrdinalIgnoreCase))
                    otherFigureHouses.Add(h);
            }

            foreach (var ch in companyFigureHouses)
            {
                foreach (var oh in otherFigureHouses)
                {
                    if (AreAdjacentHouses(ch, oh))
                    {
                        var res = new PerfectionResult();
                        res.Mode = PerfectionType.Mutation;
                        var companyFullName = companyFigure.Name;
                        var otherFullName = targetFigure.Name;
                        res.Notes.Add($"Figure in company {companyFullName} in house {ch} and {otherFullName} in house {oh} are in neighboring houses.");
                        AddInterpretationTip(res, chart, companyHouse, targetHouse);
                        return res;
                    }
                }
            }

            return null;
        }

        // Calculate the favorable score for a perfection result based on Table 6-3
        // Favorable Indications:
        //   Occupation: +5
        //   Conjunction: +4
        //   Mutation: +4
        //   Translation: +4
        //   Trine aspect: +3 per trine
        //   Sextile aspect: +3 per sextile
        //   Company of Houses: Subtract 1 from any relationship or aspect made by a figure in company
        public static int CalculateScore(PerfectionResult result)
        {
            if (result == null || result.Mode == PerfectionType.None)
                return 0;

            int baseScore = 0;
            PerfectionType modeToScore = result.Mode;

            // If made through company, use the base mode for scoring
            if (result.MadeThroughCompany && result.BaseMode != PerfectionType.None)
            {
                modeToScore = result.BaseMode;
            }

            // Calculate base score based on mode
            switch (modeToScore)
            {
                case PerfectionType.Occupation:
                    baseScore = 5;
                    break;
                case PerfectionType.Conjunction:
                    baseScore = 4;
                    break;
                case PerfectionType.Mutation:
                    baseScore = 4;
                    break;
                case PerfectionType.Translation:
                    baseScore = 4;
                    break;
                case PerfectionType.Aspect:
                    // Aspect scores depend on the aspect type
                    if (result.AspectBetweenSignificators == AspectType.Trine)
                        baseScore = 3;
                    else if (result.AspectBetweenSignificators == AspectType.Sextile)
                        baseScore = 3;
                    else
                        baseScore = 0;  // Other aspects are not favorable
                    break;
                case PerfectionType.Company:
                    // Company itself doesn't have a base score, it modifies other modes
                    // If we have a base mode, score that instead
                    if (result.BaseMode != PerfectionType.None)
                    {
                        return CalculateScore(new PerfectionResult
                        {
                            Mode = result.BaseMode,
                            AspectBetweenSignificators = result.AspectBetweenSignificators,
                            MadeThroughCompany = true
                        });
                    }
                    return 0;
                default:
                    return 0;
            }

            // If made through company, subtract 1 (Table 6-3 rule)
            if (result.MadeThroughCompany)
            {
                baseScore = Math.Max(0, baseScore - 1);  // Don't go below 0
            }

            return baseScore;
        }

        // Calculate the unfavorable score for a perfection result based on Table 6-3
        // Unfavorable Indications:
        //   Impedition: -5
        //   Opposite aspect: -4 per opposition
        //   Square aspect: -3 per square
        // Updated Method: Handles Company penalty for negative aspects and Impedition
        public static int CalculateUnfavorableScore(PerfectionResult result)
        {
            if (result == null) return 0;

            // Check for Impedition first (PerfectionType.None)
            if (result.Mode == PerfectionType.None)
            {
                return -5; // Impedition: -5
            }

            int unfavorableScore = 0;

            // Check for unfavorable aspects
            if (result.AspectBetweenSignificators == AspectType.Opposition)
            {
                unfavorableScore = -4; 
            }
            else if (result.AspectBetweenSignificators == AspectType.Square)
            {
                unfavorableScore = -3; 
            }

            // Apply Company Penalty (Table 6-3: Subtract 1 from ANY relationship)
            // If we have a negative score and it's made through company, we subtract 1 more.
            if (unfavorableScore < 0 && result.MadeThroughCompany)
            {
                unfavorableScore -= 1; // e.g., -3 becomes -4
            }

            return unfavorableScore;
        }

        // Check for Impedition (no relationship between significators)
        // Returns -5 if impedited, 0 otherwise
        public static int CheckImpedition(HouseChart chart, int querentHouse, int quesitedHouse)
        {
            var result = Find(chart, querentHouse, quesitedHouse);
            
            // If no perfection found, it's impedited
            if (result == null || result.Mode == PerfectionType.None)
            {
                return -5;  // Impedition: -5
            }

            return 0;
        }

        // Calculate total favorable indication score for a chart based on Table 6-3
        // Returns the sum of all perfection scores found in the chart
        public static int CalculateTotalFavorableScore(HouseChart chart, int querentHouse, int quesitedHouse)
        {
            var result = Find(chart, querentHouse, quesitedHouse);
            return CalculateScore(result);
        }

        // Calculate total favorable indication score for all perfections in a chart
        public static int CalculateTotalFavorableScoreForAll(HouseChart chart)
        {
            int totalScore = 0;
            var allPerfections = FindAll(chart);
            
            foreach (var perfection in allPerfections)
            {
                totalScore += CalculateScore(perfection);
            }
            
            return totalScore;
        }


        // Calculate total unfavorable indication score for a chart based on Table 6-3
        // Returns the sum of all unfavorable scores (impedition, opposition, square)
        // Updated Method: Uses the results list directly instead of re-scanning
        public static int CalculateTotalUnfavorableScore(HouseChart chart, int querentHouse, int quesitedHouse)
        {
            int totalUnfavorable = 0;

            // Get ALL indications (positive and negative)
            var results = FindAll(chart, querentHouse, quesitedHouse).ToList();

            // 1. Check for Impedition
            // Impedition (-5) only happens if there are NO relationships at all.
            // If the list is empty (or contains only a "None" result), we have Impedition.
            if (results.Count == 0 || (results.Count == 1 && results[0].Mode == PerfectionType.None))
            {
                return -5; 
            }

            // 2. Sum up unfavorable scores from the found results
            // (Squares and Oppositions are now inside the results list)
            foreach (var result in results)
            {
                totalUnfavorable += CalculateUnfavorableScore(result);
            }

            return totalUnfavorable;
        }

        // Calculate net score (favorable - unfavorable) for a chart based on Table 6-3
        // Updated Method: unified calculation
        public static int CalculateNetScore(HouseChart chart, int querentHouse, int quesitedHouse)
        {
            var results = FindAll(chart, querentHouse, quesitedHouse).ToList();

            // Check Impedition first (special case: -5 if list is essentially empty)
            if (results.Count == 0 || (results.Count == 1 && results[0].Mode == PerfectionType.None))
            {
                return -5;
            }

            int totalScore = 0;

            foreach (var result in results)
            {
                // Add favorable points (Occupations, Trines, Sextiles, Translations, etc.)
                totalScore += CalculateScore(result);

                // Add unfavorable points (Squares, Oppositions)
                // Note: CalculateUnfavorableScore returns negative numbers, so we just add them.
                totalScore += CalculateUnfavorableScore(result);
            }

            return totalScore;
        }

        /// <summary>
        /// Determines if a result represents an actual perfection (not a denial)
        /// </summary>
        private static bool IsActualPerfection(PerfectionResult result)
        {
            if (result == null) return false;
            
            // Occupation, Conjunction, Translation, Mutation are always perfections
            if (result.Mode == PerfectionType.Occupation ||
                result.Mode == PerfectionType.Conjunction ||
                result.Mode == PerfectionType.Translation ||
                result.Mode == PerfectionType.Mutation)
                return true;
            
            // Company is a perfection if it has a favorable base mode
            if (result.Mode == PerfectionType.Company)
            {
                // Check if base mode is favorable
                if (result.BaseMode == PerfectionType.Occupation ||
                    result.BaseMode == PerfectionType.Conjunction ||
                    result.BaseMode == PerfectionType.Translation ||
                    result.BaseMode == PerfectionType.Mutation ||
                    result.BaseMode == PerfectionType.Aspect)
                {
                    // If base mode is Aspect, check if the aspect is favorable
                    if (result.BaseMode == PerfectionType.Aspect)
                    {
                        return result.AspectBetweenSignificators == AspectType.Sextile ||
                               result.AspectBetweenSignificators == AspectType.Trine;
                    }
                    // Other base modes (Occupation, Conjunction, Translation, Mutation) are always favorable
                    return true;
                }
                return false;
            }
            
            // Aspect is a perfection only if favorable (Sextile/Trine)
            if (result.Mode == PerfectionType.Aspect)
            {
                return result.AspectBetweenSignificators == AspectType.Sextile ||
                       result.AspectBetweenSignificators == AspectType.Trine;
            }
            
            return false;
        }

        /// <summary>
        /// Returns comprehensive perfection analysis including all perfections and calculated scores
        /// </summary>
        public static PerfectionAnalysis AnalyzePerfections(HouseChart chart, int querentHouse, int quesitedHouse)
        {
            var results = FindAll(chart, querentHouse, quesitedHouse).ToList();
            
            var analysis = new PerfectionAnalysis
            {
                QuerentHouse = querentHouse,
                QuesitedHouse = quesitedHouse,
                Perfections = new List<PerfectionResult>(),
                Denials = new List<PerfectionResult>(),
                PositiveAspects = new List<AspectRecord>(),
                NegativeAspects = new List<AspectRecord>()
            };

            // Check if we have any actual perfections
            var hasPerfections = results.Any(r => IsActualPerfection(r));

            // Separate perfections and denials
            if (hasPerfections)
            {
                // We have actual perfections - separate them from unfavorable aspects
                analysis.Perfections = results.Where(r => IsActualPerfection(r)).ToList();
                
                // Unfavorable aspects that appear alongside perfections are "difficulties" - 
                // they're still in results for scoring but not in Perfections list
                // They'll be captured in NegativeAspects below
            }
            else
            {
                // No perfections - check if we have unfavorable aspects (these are denials)
                var unfavorableAspects = results.Where(r => r.Mode == PerfectionType.Aspect && 
                    (r.AspectBetweenSignificators == AspectType.Square || 
                     r.AspectBetweenSignificators == AspectType.Opposition)).ToList();
                
                // Also check for Company-made unfavorable aspects that deny perfection
                var unfavorableCompany = results.Where(r => r.Mode == PerfectionType.Company &&
                    (r.AspectBetweenSignificators == AspectType.Square || 
                     r.AspectBetweenSignificators == AspectType.Opposition)).ToList();
                
                var allDenials = new List<PerfectionResult>();
                allDenials.AddRange(unfavorableAspects);
                allDenials.AddRange(unfavorableCompany);
                
                // Always create Impedition when there are no perfections
                // (whether there are negative aspects or not)
                var impeditionResult = results.FirstOrDefault(r => r.Mode == PerfectionType.None);
                if (impeditionResult == null)
                {
                    // Create Impedition result if it doesn't exist
                    impeditionResult = new PerfectionResult
                    {
                        Mode = PerfectionType.None,
                        AspectBetweenSignificators = AspectType.None,
                        QuerentHouse = querentHouse,
                        QuesitedHouse = quesitedHouse
                    };
                    impeditionResult.Notes.Add("No classical mode of perfection found. Impedition: indirect factors are too strong for the querent to overcome.");
                    AddInterpretationTip(impeditionResult, chart, querentHouse, quesitedHouse);
                    // Add to results list so it's included in scoring
                    results.Add(impeditionResult);
                }
                else
                {
                    // Ensure interpretation tip is added if it wasn't already
                    if (!impeditionResult.Notes.Any(n => n.Contains("Interpretation")))
                    {
                        AddInterpretationTip(impeditionResult, chart, querentHouse, quesitedHouse);
                    }
                }
                
                // Add all denials (unfavorable aspects + impedition)
                analysis.Denials = allDenials;
                // Always add impedition to denials (if not already there)
                if (!analysis.Denials.Any(d => d.Mode == PerfectionType.None))
                {
                    analysis.Denials.Add(impeditionResult);
                }
                analysis.Perfections = new List<PerfectionResult>(); // Empty - no perfections
            }

            // Extract all aspects from results and categorize them
            foreach (var result in results)
            {
                // Only process results that have aspects
                if (result.AspectBetweenSignificators != AspectType.None)
                {
                    var aspectRecord = new AspectRecord
                    {
                        AspectType = result.AspectBetweenSignificators,
                        MadeThroughCompany = result.MadeThroughCompany
                    };

                    // Extract information from notes
                    string fullNote = string.Join(" ", result.Notes);
                    aspectRecord.Description = fullNote;

                    // Parse direction from notes (e.g., "Dexter Sextile (Backward)", "Sinister Trine (Forward)")
                    // Set IsMajorAspect: Dexter aspects and Opposition are Major (high energy/dominant)
                    if (fullNote.Contains("Dexter"))
                    {
                        aspectRecord.Direction = "Dexter (Backward)";
                        aspectRecord.IsMajorAspect = true;
                    }
                    else if (fullNote.Contains("Sinister"))
                    {
                        aspectRecord.Direction = "Sinister (Forward)";
                        aspectRecord.IsMajorAspect = false;
                    }
                    else if (fullNote.Contains("Opposition"))
                    {
                        aspectRecord.Direction = "Opposition";
                        aspectRecord.IsMajorAspect = true; // Opposition is total denial, considered Major
                    }
                    else
                    {
                        aspectRecord.Direction = result.AspectBetweenSignificators.ToString();
                        // Default: Opposition is Major, others depend on context
                        aspectRecord.IsMajorAspect = result.AspectBetweenSignificators == AspectType.Opposition;
                    }

                    // Extract house numbers from notes
                    // Pattern: "house X" or "House X" or "house X (translation)"
                    var houseMatches = Regex.Matches(fullNote, @"[Hh]ouse\s+(\d+)");
                    if (houseMatches.Count >= 2)
                    {
                        aspectRecord.FromHouse = int.Parse(houseMatches[0].Groups[1].Value);
                        aspectRecord.ToHouse = int.Parse(houseMatches[1].Groups[1].Value);
                    }
                    else if (houseMatches.Count == 1)
                    {
                        // For direct aspects, use querent/quesited houses
                        aspectRecord.FromHouse = querentHouse;
                        aspectRecord.ToHouse = quesitedHouse;
                    }
                    else
                    {
                        // Fallback: try to extract from "in house X" pattern
                        var inHouseMatches = Regex.Matches(fullNote, @"in house (\d+)");
                        if (inHouseMatches.Count > 0)
                        {
                            aspectRecord.FromHouse = int.Parse(inHouseMatches[0].Groups[1].Value);
                            // Determine target house from context
                            if (fullNote.Contains("quesited"))
                                aspectRecord.ToHouse = quesitedHouse;
                            else if (fullNote.Contains("querent"))
                                aspectRecord.ToHouse = querentHouse;
                            else
                                aspectRecord.ToHouse = quesitedHouse; // Default
                        }
                        else
                        {
                            // Last resort: use querent/quesited
                            aspectRecord.FromHouse = querentHouse;
                            aspectRecord.ToHouse = quesitedHouse;
                        }
                    }

                    // Categorize as positive or negative
                    if (result.AspectBetweenSignificators == AspectType.Sextile || 
                        result.AspectBetweenSignificators == AspectType.Trine)
                    {
                        analysis.PositiveAspects.Add(aspectRecord);
                    }
                    else if (result.AspectBetweenSignificators == AspectType.Square || 
                             result.AspectBetweenSignificators == AspectType.Opposition)
                    {
                        analysis.NegativeAspects.Add(aspectRecord);
                    }
                }
            }

            // Calculate scores for each result and totals
            foreach (var result in results)
            {
                // Calculate individual scores for each result (for display purposes)
                // These will be used by the API to show per-result scoring
                int favorableScore = CalculateScore(result);
                int unfavorableScore = CalculateUnfavorableScore(result);
                
                // Store in a way that can be accessed (we'll need to add properties or use notes)
                // For now, we'll calculate these on-demand in the API layer
                
                // Add to totals
                analysis.TotalFavorableScore += favorableScore;
                analysis.TotalUnfavorableScore += unfavorableScore;
            }

            // Calculate net score
            analysis.NetScore = analysis.TotalFavorableScore + analysis.TotalUnfavorableScore;

            return analysis;
        }
    }
}
