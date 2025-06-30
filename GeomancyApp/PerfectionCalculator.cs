using System;
using System.Collections.Generic;
using System.Linq;

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

            /* 1 — Conjunction */
            if (Root(Q.Name).Equals(Root(X.Name), StringComparison.OrdinalIgnoreCase))
            {
                res.Mode = PerfectionType.Conjunction;
                res.Notes.Add($"Both significators are {Root(Q.Name)}.");
                return res;
            }

            /* 2 — Occupation */
            int occupationHouse = GetHouseOfFigure(chart, X);
            if (occupationHouse == querentHouse)
            {
                res.Mode = PerfectionType.Occupation;
                res.Notes.Add($"Quesited figure {Root(X.Name)} occupies querent's house {querentHouse}.");
                return res;
            }

            occupationHouse = GetHouseOfFigure(chart, Q);
            if (occupationHouse == quesitedHouse)
            {
                res.Mode = PerfectionType.Occupation;
                res.Notes.Add($"Querent figure {Root(Q.Name)} occupies quesited's house {quesitedHouse}.");
                return res;
            }

            /* 3 — Direct Dexter Aspects */
            var aspect = GeomanticAspects.GetAspect(querentHouse, quesitedHouse);
            if (aspect == AspectType.Sextile || aspect == AspectType.Trine)
            {
                res.Mode = PerfectionType.Aspect;
                res.AspectBetweenSignificators = aspect;
                res.Notes.Add($"Significators aspect each other by {aspect.ToString().ToLower()}.");
                return res;
            }

            /* 4 — Translation of Light */
            int translator = FindTranslator(chart, querentHouse, quesitedHouse);
            if (translator != -1)
            {
                res.Mode = PerfectionType.Translation;
                res.TranslatorHouse = translator;
                res.Notes.Add(
                    $"House {translator} ({chart.GetHouseFigure(translator).Name}) translates the light.");
                return res;
            }

            /* 5 — Mutation (both figures mobile and applying by aspect) */
            bool bothMobile = MobileFigures.Contains(Root(Q.Name)) && MobileFigures.Contains(Root(X.Name));
            if (bothMobile && (aspect == AspectType.Sextile || aspect == AspectType.Trine || aspect == AspectType.Square))
            {
                res.Mode = PerfectionType.Mutation;
                res.AspectBetweenSignificators = aspect;
                res.Notes.Add($"Both figures are mobile and aspect each other by {aspect.ToString().ToLower()}.");
                return res;
            }

            /* — No classical perfection — */
            res.AspectBetweenSignificators = aspect;
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

        // Helper method to calculate zodiacal distance (0-11 forward distance)
        private static int ZodiacDistance(int from, int to)
        {
            int d = (to - from) % 12;
            return d < 0 ? d + 12 : d;
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
            var results = new List<PerfectionResult>();
            var mainResult = Find(chart, querentHouse, quesitedHouse);
            if (!returnAllModes)
            {
                results.Add(mainResult);
                return results;
            }
            // If you have logic for multiple modes, add them here. For now, just return the main result.
            results.Add(mainResult);
            return results;
        }
    }
}
