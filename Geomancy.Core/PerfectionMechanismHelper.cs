using System;
using System.Collections.Generic;

namespace GeomancyApp
{
    /// <summary>
    /// Structured relationship-reading explanation for perfection / denial / aspect entries.
    /// Built from typed Path*/Aspect*/Company fields — never from regex of note prose.
    /// </summary>
    public static class PerfectionMechanismHelper
    {
        public sealed class MechanismExplanation
        {
            public string Title { get; set; } = "How this formed";
            public string Flow { get; set; } = string.Empty;
            public List<string> Steps { get; set; } = new List<string>();
            public CastSummary Cast { get; set; }
            public string DirectionHint { get; set; } = string.Empty;
            public List<Participant> Participants { get; set; } = new List<Participant>();
            public bool HasStructuredCast => Cast != null && Cast.FromHouse > 0 && Cast.ToHouse > 0;
            public bool HasFormation =>
                !string.IsNullOrEmpty(Flow) || Steps.Count > 0 || HasStructuredCast;
        }

        public sealed class CastSummary
        {
            public int FromHouse { get; set; }
            public int ToHouse { get; set; }
            public string FromFigure { get; set; } = string.Empty;
            public string ToFigure { get; set; } = string.Empty;
            public string AspectType { get; set; } = string.Empty;
            public string Direction { get; set; } = string.Empty;
        }

        public sealed class Participant
        {
            public string Role { get; set; } = string.Empty;
            public int House { get; set; }
            public string FigureName { get; set; } = string.Empty;
        }

        /// <summary>
        /// Build from a perfection engine result plus figure names at cast / path houses.
        /// </summary>
        public static MechanismExplanation ExplainFromPerfectionResult(
            PerfectionResult result,
            string fromFigure,
            string toFigure)
        {
            if (result == null)
                return new MechanismExplanation();

            // Prefer explicit cast-house figures; fall back to path figures for classical modes.
            var pathFromFigure = !string.IsNullOrWhiteSpace(fromFigure) ? fromFigure : result.PathFigure;
            var pathToFigure = !string.IsNullOrWhiteSpace(toFigure)
                ? toFigure
                : (!string.IsNullOrWhiteSpace(result.PathSecondaryFigure)
                    ? result.PathSecondaryFigure
                    : result.PathFigure);

            return ExplainRelationship(
                mode: result.Mode.ToString(),
                baseMode: result.BaseMode != PerfectionType.None ? result.BaseMode.ToString() : string.Empty,
                querentHouse: result.QuerentHouse,
                quesitedHouse: result.QuesitedHouse,
                querentFigure: null,
                quesitedFigure: null,
                pathFromHouse: result.PathFromHouse,
                pathToHouse: result.PathToHouse,
                pathFigure: result.PathFigure,
                pathSecondaryFigure: result.PathSecondaryFigure,
                pathActor: result.PathActor,
                translatorHouse: result.TranslatorHouse,
                translatorHouseSecondary: result.TranslatorHouseSecondary,
                translatorFigure: !string.IsNullOrWhiteSpace(result.PathFigure)
                    ? result.PathFigure
                    : pathFromFigure,
                aspectFromHouse: result.AspectFromHouse,
                aspectToHouse: result.AspectToHouse,
                aspectFromFigure: pathFromFigure,
                aspectToFigure: pathToFigure,
                aspectType: result.AspectBetweenSignificators.ToString(),
                aspectDirection: result.AspectDirection,
                madeThroughCompany: result.MadeThroughCompany || result.Mode == PerfectionType.Company,
                companyType: result.CompanyType.ToString(),
                companyTypeDescription: result.CompanyTypeDescription);
        }

        /// <summary>
        /// Field-based entry used by the UI model layer.
        /// </summary>
        public static MechanismExplanation ExplainRelationship(
            string mode,
            string baseMode,
            int querentHouse,
            int quesitedHouse,
            string querentFigure,
            string quesitedFigure,
            int pathFromHouse,
            int pathToHouse,
            string pathFigure,
            string pathSecondaryFigure,
            string pathActor,
            int translatorHouse,
            int translatorHouseSecondary,
            string translatorFigure,
            int aspectFromHouse,
            int aspectToHouse,
            string aspectFromFigure,
            string aspectToFigure,
            string aspectType,
            string aspectDirection,
            bool madeThroughCompany,
            string companyType,
            string companyTypeDescription)
        {
            var explanation = new MechanismExplanation();

            var path = PerfectionPathDisplay.ForListRow(
                pathFromHouse,
                pathToHouse,
                pathFigure,
                pathSecondaryFigure,
                pathActor,
                aspectFromHouse,
                aspectToHouse);
            explanation.Flow = PerfectionPathDisplay.FormatFlow(path);

            var effectiveMode = ResolveEffectiveMode(mode, baseMode);
            bool isCompany = string.Equals(mode, "Company", StringComparison.OrdinalIgnoreCase)
                || madeThroughCompany;

            // Only use cast geometry when the effective mode is Aspect (standalone or company-aspect).
            if (IsAspectMode(effectiveMode) && IsAspectGeometry(aspectFromHouse, aspectToHouse, aspectType))
            {
                var aspectExplanation = ExplainAspectCast(
                    aspectFromHouse,
                    aspectToHouse,
                    aspectFromFigure,
                    aspectToFigure,
                    aspectType,
                    aspectDirection,
                    querentHouse,
                    quesitedHouse,
                    querentFigure,
                    quesitedFigure,
                    isCompany,
                    companyType,
                    companyTypeDescription,
                    mode,
                    baseMode);

                explanation.Cast = aspectExplanation.Cast;
                explanation.DirectionHint = aspectExplanation.DirectionHint;
                explanation.Steps.AddRange(aspectExplanation.Steps);
                if (string.IsNullOrEmpty(explanation.Flow) && explanation.HasStructuredCast)
                {
                    explanation.Flow = PerfectionPathDisplay.FormatFlow(
                        PerfectionPathDisplay.ForListRow(
                            aspectFromHouse,
                            aspectToHouse,
                            aspectFromFigure,
                            string.Empty,
                            string.Empty));
                }
            }
            else
            {
                if (isCompany)
                    AddCompanyContextSteps(explanation, querentHouse, quesitedHouse, querentFigure, quesitedFigure,
                        pathFromHouse, pathToHouse, pathFigure, pathActor, companyType, companyTypeDescription);

                switch (effectiveMode)
                {
                    case "Occupation":
                        AddOccupationSteps(explanation, pathFromHouse, pathToHouse, pathFigure, querentHouse, quesitedHouse);
                        break;
                    case "Conjunction":
                        AddConjunctionSteps(explanation, pathFromHouse, pathToHouse, pathFigure, pathActor);
                        break;
                    case "Translation":
                        AddTranslationSteps(explanation, pathFromHouse, pathToHouse, pathFigure,
                            translatorHouse, translatorHouseSecondary, translatorFigure);
                        break;
                    case "Mutation":
                        AddMutationSteps(explanation, pathFromHouse, pathToHouse, pathFigure, pathSecondaryFigure);
                        break;
                    case "None":
                    case "Impedition":
                        AddDenialSteps(explanation, path);
                        break;
                    default:
                        if (path.HasPath)
                        {
                            explanation.Steps.Add(
                                $"The relationship forms along {explanation.Flow}.");
                        }
                        break;
                }
            }

            explanation.Participants = BuildParticipants(
                querentHouse, quesitedHouse, querentFigure, quesitedFigure,
                pathFromHouse, pathToHouse, pathFigure, pathSecondaryFigure, pathActor,
                translatorHouse, translatorHouseSecondary, translatorFigure,
                aspectFromHouse, aspectToHouse, aspectFromFigure, aspectToFigure,
                effectiveMode, isCompany);

            return explanation;
        }

        public static MechanismExplanation ExplainAspectCast(
            int aspectFromHouse,
            int aspectToHouse,
            string fromFigure,
            string toFigure,
            string aspectType,
            string direction,
            int querentHouse,
            int quesitedHouse,
            string querentFigure,
            string quesitedFigure,
            bool madeThroughCompany,
            string companyType,
            string companyTypeDescription,
            string mode,
            string baseMode)
        {
            var explanation = new MechanismExplanation();

            if (aspectFromHouse <= 0 || aspectToHouse <= 0
                || string.IsNullOrWhiteSpace(aspectType)
                || aspectType.Equals("None", StringComparison.OrdinalIgnoreCase))
            {
                return explanation;
            }

            fromFigure = string.IsNullOrWhiteSpace(fromFigure) ? "the figure" : fromFigure.Trim();
            toFigure = string.IsNullOrWhiteSpace(toFigure) ? "the figure" : toFigure.Trim();
            aspectType = aspectType.Trim();
            direction = (direction ?? string.Empty).Trim();

            explanation.Cast = new CastSummary
            {
                FromHouse = aspectFromHouse,
                ToHouse = aspectToHouse,
                FromFigure = fromFigure,
                ToFigure = toFigure,
                AspectType = aspectType,
                Direction = NormalizeDirection(direction, aspectType)
            };

            explanation.Title = string.IsNullOrEmpty(aspectType)
                ? "How this formed"
                : $"How this {aspectType} formed";

            explanation.Flow = PerfectionPathDisplay.FormatFlow(
                PerfectionPathDisplay.ForListRow(
                    aspectFromHouse,
                    aspectToHouse,
                    fromFigure,
                    string.Empty,
                    string.Empty));

            bool isCompanyAspect = madeThroughCompany
                || string.Equals(mode, "Company", StringComparison.OrdinalIgnoreCase);

            var dirLabel = string.IsNullOrEmpty(explanation.Cast.Direction)
                ? aspectType
                : $"{explanation.Cast.Direction} {aspectType}";

            // Lead with the aspect cast — company is a supporting condition, not the headline.
            explanation.Steps.Add(
                $"{fromFigure} in House {aspectFromHouse} casts a {dirLabel} to House {aspectToHouse} ({toFigure}).");

            if (isCompanyAspect)
            {
                AddCompanyPairingStep(explanation, querentHouse, quesitedHouse, querentFigure, quesitedFigure,
                    aspectFromHouse, fromFigure, companyType, companyTypeDescription);
                AddCompanyCastRoleStep(explanation, aspectFromHouse, querentHouse, quesitedHouse);
            }
            else if (aspectFromHouse != querentHouse && aspectFromHouse != quesitedHouse)
            {
                explanation.Steps.Add(
                    $"{fromFigure} also appears in House {aspectFromHouse} (translation of the significator), away from its home house.");
            }

            explanation.DirectionHint = DirectionHintText(explanation.Cast.Direction);

            explanation.Participants = BuildParticipants(
                querentHouse, quesitedHouse, querentFigure, quesitedFigure,
                aspectFromHouse, aspectToHouse, fromFigure, string.Empty, string.Empty,
                0, 0, string.Empty,
                aspectFromHouse, aspectToHouse, fromFigure, toFigure,
                "Aspect", isCompanyAspect);

            return explanation;
        }

        private static void AddOccupationSteps(
            MechanismExplanation explanation,
            int pathFromHouse,
            int pathToHouse,
            string pathFigure,
            int querentHouse,
            int quesitedHouse)
        {
            var fig = string.IsNullOrWhiteSpace(pathFigure) ? "the same figure" : pathFigure.Trim();
            int from = pathFromHouse > 0 ? pathFromHouse : querentHouse;
            int to = pathToHouse > 0 ? pathToHouse : quesitedHouse;
            if (from > 0 && to > 0)
            {
                explanation.Steps.Add(
                    $"{fig} occupies both House {from} and House {to}, joining the significators by occupation.");
            }
            else
            {
                explanation.Steps.Add($"{fig} occupies both significator houses.");
            }
        }

        private static void AddConjunctionSteps(
            MechanismExplanation explanation,
            int pathFromHouse,
            int pathToHouse,
            string pathFigure,
            string pathActor)
        {
            var fig = string.IsNullOrWhiteSpace(pathFigure) ? "the significator" : pathFigure.Trim();
            var actor = NormalizeActorLabel(pathActor);
            if (pathFromHouse > 0 && pathToHouse > 0)
            {
                if (!string.IsNullOrEmpty(actor))
                {
                    explanation.Steps.Add(
                        $"{actor} figure {fig} passes from House {pathFromHouse} to House {pathToHouse}, adjacent to the other significator.");
                }
                else
                {
                    explanation.Steps.Add(
                        $"{fig} passes from House {pathFromHouse} to House {pathToHouse}, forming conjunction.");
                }
            }
            else
            {
                explanation.Steps.Add($"{fig} forms a conjunction by passing next door to the other significator.");
            }
        }

        private static void AddTranslationSteps(
            MechanismExplanation explanation,
            int pathFromHouse,
            int pathToHouse,
            string pathFigure,
            int translatorHouse,
            int translatorHouseSecondary,
            string translatorFigure)
        {
            var fig = FirstNonEmpty(translatorFigure, pathFigure, "a third figure");
            int h1 = pathFromHouse > 0 ? pathFromHouse : translatorHouse;
            int h2 = pathToHouse > 0 ? pathToHouse : translatorHouseSecondary;
            if (h1 > 0 && h2 > 0)
            {
                explanation.Steps.Add(
                    $"{fig} appears in House {h1} (adjacent to the querent) and House {h2} (adjacent to the quesited).");
                explanation.Steps.Add(
                    $"That shared placement translates the light between the significators along {explanation.Flow}.");
            }
            else
            {
                explanation.Steps.Add($"{fig} translates the light between houses adjacent to both significators.");
            }
        }

        private static void AddMutationSteps(
            MechanismExplanation explanation,
            int pathFromHouse,
            int pathToHouse,
            string pathFigure,
            string pathSecondaryFigure)
        {
            var a = string.IsNullOrWhiteSpace(pathFigure) ? "one significator" : pathFigure.Trim();
            var b = string.IsNullOrWhiteSpace(pathSecondaryFigure) ? "the other significator" : pathSecondaryFigure.Trim();
            if (pathFromHouse > 0 && pathToHouse > 0)
            {
                explanation.Steps.Add(
                    $"Both significators pass to neighboring houses: {a} in House {pathFromHouse} and {b} in House {pathToHouse}.");
                explanation.Steps.Add("Their contact away from the home houses forms mutation.");
            }
            else
            {
                explanation.Steps.Add($"Both significators mutate into neighboring houses ({a} · {b}).");
            }
        }

        private static void AddDenialSteps(
            MechanismExplanation explanation,
            PerfectionPathDisplay.ListRowPath path)
        {
            explanation.Title = "Why this denies";
            if (path != null && path.HasPath)
            {
                explanation.Steps.Add(
                    $"No classical perfection joins the parties; the chart only shows the difficult connection {PerfectionPathDisplay.FormatFlow(path)}.");
            }
            else
            {
                explanation.Steps.Add(
                    "No classical mode of perfection joins the querent and quesited (impedition).");
            }
        }

        private static void AddCompanyContextSteps(
            MechanismExplanation explanation,
            int querentHouse,
            int quesitedHouse,
            string querentFigure,
            string quesitedFigure,
            int pathFromHouse,
            int pathToHouse,
            string pathFigure,
            string pathActor,
            string companyType,
            string companyTypeDescription)
        {
            var typeLabel = CompanyTypeLabel(companyType, companyTypeDescription);
            var companionHouse = pathToHouse > 0 ? pathToHouse : pathFromHouse;
            var companionFigure = string.IsNullOrWhiteSpace(pathFigure) ? "the companion figure" : pathFigure.Trim();
            var actor = (pathActor ?? string.Empty).Trim();

            if (actor.StartsWith("Qst", StringComparison.OrdinalIgnoreCase) && quesitedHouse > 0 && companionHouse > 0)
            {
                var xFig = string.IsNullOrWhiteSpace(quesitedFigure) ? "the quesited figure" : quesitedFigure.Trim();
                explanation.Steps.Add(
                    $"House {quesitedHouse} ({xFig}) is in {typeLabel} company with House {companionHouse} ({companionFigure}).");
            }
            else if (actor.StartsWith("Q", StringComparison.OrdinalIgnoreCase) && querentHouse > 0 && companionHouse > 0)
            {
                var qFig = string.IsNullOrWhiteSpace(querentFigure) ? "the querent figure" : querentFigure.Trim();
                explanation.Steps.Add(
                    $"House {querentHouse} ({qFig}) is in {typeLabel} company with House {companionHouse} ({companionFigure}).");
            }
            else if (companionHouse > 0)
            {
                explanation.Steps.Add(
                    $"Made through company ({typeLabel}): {companionFigure} in House {companionHouse} acts for a significator in company.");
            }
        }

        private static List<Participant> BuildParticipants(
            int querentHouse,
            int quesitedHouse,
            string querentFigure,
            string quesitedFigure,
            int pathFromHouse,
            int pathToHouse,
            string pathFigure,
            string pathSecondaryFigure,
            string pathActor,
            int translatorHouse,
            int translatorHouseSecondary,
            string translatorFigure,
            int aspectFromHouse,
            int aspectToHouse,
            string aspectFromFigure,
            string aspectToFigure,
            string effectiveMode,
            bool isCompany)
        {
            var list = new List<Participant>();
            AddParticipant(list, "Querent", querentHouse, querentFigure);
            AddParticipant(list, "Quesited", quesitedHouse, quesitedFigure);

            if (string.Equals(effectiveMode, "Translation", StringComparison.OrdinalIgnoreCase))
            {
                int h1 = pathFromHouse > 0 ? pathFromHouse : translatorHouse;
                int h2 = pathToHouse > 0 ? pathToHouse : translatorHouseSecondary;
                var fig = FirstNonEmpty(translatorFigure, pathFigure);
                AddParticipant(list, "Translator", h1, fig);
                if (h2 > 0 && h2 != h1)
                    AddParticipant(list, "Translator (second house)", h2, fig);
            }
            else if (string.Equals(effectiveMode, "Conjunction", StringComparison.OrdinalIgnoreCase))
            {
                var role = NormalizeActorLabel(pathActor);
                if (string.IsNullOrEmpty(role)) role = "Passing significator";
                else role = $"{role} (pass)";
                AddParticipant(list, role, pathToHouse > 0 ? pathToHouse : pathFromHouse, pathFigure);
            }
            else if (string.Equals(effectiveMode, "Mutation", StringComparison.OrdinalIgnoreCase))
            {
                AddParticipant(list, "Querent pass", pathFromHouse, pathFigure);
                AddParticipant(list, "Quesited pass", pathToHouse, pathSecondaryFigure);
            }
            else if (string.Equals(effectiveMode, "Occupation", StringComparison.OrdinalIgnoreCase))
            {
                if (isCompany)
                    AddParticipant(list, "Company companion", pathToHouse > 0 ? pathToHouse : pathFromHouse, pathFigure);
                else
                    AddParticipant(list, "Shared figure", pathFromHouse, pathFigure);
            }
            else if (string.Equals(effectiveMode, "Aspect", StringComparison.OrdinalIgnoreCase))
            {
                AddParticipant(list, isCompany ? "Company casting figure" : "Casting figure",
                    aspectFromHouse > 0 ? aspectFromHouse : pathFromHouse,
                    FirstNonEmpty(aspectFromFigure, pathFigure));
                AddParticipant(list, "Receiving figure",
                    aspectToHouse > 0 ? aspectToHouse : pathToHouse,
                    FirstNonEmpty(aspectToFigure, pathSecondaryFigure));
            }
            else if (isCompany)
            {
                AddParticipant(list, "Company companion", pathToHouse > 0 ? pathToHouse : pathFromHouse, pathFigure);
            }

            return DeduplicateParticipants(list);
        }

        private static void AddParticipant(List<Participant> list, string role, int house, string figureName)
        {
            if (house <= 0 && string.IsNullOrWhiteSpace(figureName))
                return;
            list.Add(new Participant
            {
                Role = role ?? string.Empty,
                House = house,
                FigureName = figureName?.Trim() ?? string.Empty
            });
        }

        private static List<Participant> DeduplicateParticipants(List<Participant> list)
        {
            var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var result = new List<Participant>();
            foreach (var p in list)
            {
                var key = $"{p.Role}|{p.House}|{p.FigureName}";
                if (seen.Add(key))
                    result.Add(p);
            }
            return result;
        }

        private static void AddCompanyPairingStep(
            MechanismExplanation explanation,
            int querentHouse,
            int quesitedHouse,
            string querentFigure,
            string quesitedFigure,
            int castFromHouse,
            string castFromFigure,
            string companyType,
            string companyTypeDescription)
        {
            int querentPair = PairedHouse(querentHouse);
            int quesitedPair = PairedHouse(quesitedHouse);

            string typeLabel = CompanyTypeLabel(companyType, companyTypeDescription);

            if (castFromHouse == querentPair && querentHouse > 0)
            {
                var qFig = string.IsNullOrWhiteSpace(querentFigure) ? "the querent figure" : querentFigure.Trim();
                explanation.Steps.Add(
                    $"House {querentHouse} ({qFig}) is in {typeLabel} company with House {castFromHouse} ({castFromFigure}).");
            }
            else if (castFromHouse == quesitedPair && quesitedHouse > 0)
            {
                var xFig = string.IsNullOrWhiteSpace(quesitedFigure) ? "the quesited figure" : quesitedFigure.Trim();
                explanation.Steps.Add(
                    $"House {quesitedHouse} ({xFig}) is in {typeLabel} company with House {castFromHouse} ({castFromFigure}).");
            }
            else if (castFromHouse == querentHouse || castFromHouse == quesitedHouse)
            {
                explanation.Steps.Add(
                    $"The company companion relationship lets {castFromFigure} in House {castFromHouse} act as a second significator ({typeLabel} company).");
            }
            else
            {
                explanation.Steps.Add(
                    $"Made through company ({typeLabel}): {castFromFigure} in House {castFromHouse} acts for a significator in company.");
            }
        }

        private static void AddCompanyCastRoleStep(
            MechanismExplanation explanation,
            int castFromHouse,
            int querentHouse,
            int quesitedHouse)
        {
            int querentPair = PairedHouse(querentHouse);
            int quesitedPair = PairedHouse(quesitedHouse);

            if (castFromHouse == querentPair)
                explanation.Steps.Add("The casting house is the company companion of the querent.");
            else if (castFromHouse == quesitedPair)
                explanation.Steps.Add("The casting house is the company companion of the quesited.");
            else if (castFromHouse == querentHouse)
                explanation.Steps.Add("The cast leaves from the querent's house via the company relationship.");
            else if (castFromHouse == quesitedHouse)
                explanation.Steps.Add("The cast leaves from the quesited's house via the company relationship.");
        }

        private static string ResolveEffectiveMode(string mode, string baseMode)
        {
            if (string.Equals(mode, "Company", StringComparison.OrdinalIgnoreCase)
                && !string.IsNullOrWhiteSpace(baseMode)
                && !baseMode.Equals("None", StringComparison.OrdinalIgnoreCase))
            {
                return baseMode.Trim();
            }
            return string.IsNullOrWhiteSpace(mode) ? string.Empty : mode.Trim();
        }

        private static bool IsAspectMode(string mode) =>
            string.Equals(mode, "Aspect", StringComparison.OrdinalIgnoreCase);

        private static bool IsAspectGeometry(int from, int to, string aspectType) =>
            from > 0 && to > 0
            && !string.IsNullOrWhiteSpace(aspectType)
            && !aspectType.Equals("None", StringComparison.OrdinalIgnoreCase);

        private static string NormalizeActorLabel(string pathActor)
        {
            var a = (pathActor ?? string.Empty).Trim();
            if (a.StartsWith("Qst", StringComparison.OrdinalIgnoreCase)) return "Quesited";
            if (a.StartsWith("Q", StringComparison.OrdinalIgnoreCase)) return "Querent";
            return string.Empty;
        }

        private static string FirstNonEmpty(params string[] values)
        {
            foreach (var v in values)
            {
                if (!string.IsNullOrWhiteSpace(v))
                    return v.Trim();
            }
            return string.Empty;
        }

        private static string CompanyTypeLabel(string companyType, string companyTypeDescription)
        {
            if (!string.IsNullOrWhiteSpace(companyTypeDescription))
            {
                var d = companyTypeDescription.Trim();
                if (d.StartsWith("Company ", StringComparison.OrdinalIgnoreCase))
                    d = d.Substring("Company ".Length).Trim();
                var paren = d.IndexOf('(');
                if (paren > 0)
                    d = d.Substring(0, paren).Trim();
                if (!string.IsNullOrEmpty(d))
                    return d;
            }

            if (string.IsNullOrWhiteSpace(companyType) || companyType.Equals("None", StringComparison.OrdinalIgnoreCase))
                return "houses";

            return companyType switch
            {
                "Simple" => "Simple",
                "DemiSimple" => "Demi-simple",
                "Compound" => "Compound",
                "Capitular" => "Capitular",
                _ => companyType
            };
        }

        private static string NormalizeDirection(string direction, string aspectType)
        {
            if (aspectType.Equals("Opposition", StringComparison.OrdinalIgnoreCase))
                return "Opposition";
            if (string.IsNullOrEmpty(direction))
                return string.Empty;
            if (direction.IndexOf("Dexter", StringComparison.OrdinalIgnoreCase) >= 0)
                return "Dexter";
            if (direction.IndexOf("Sinister", StringComparison.OrdinalIgnoreCase) >= 0)
                return "Sinister";
            if (direction.IndexOf("Opposition", StringComparison.OrdinalIgnoreCase) >= 0)
                return "Opposition";
            return direction;
        }

        private static string DirectionHintText(string direction)
        {
            if (string.Equals(direction, "Dexter", StringComparison.OrdinalIgnoreCase))
                return "Dexter casts backward on the house wheel and tends to act more forcefully.";
            if (string.Equals(direction, "Sinister", StringComparison.OrdinalIgnoreCase))
                return "Sinister casts forward on the house wheel and tends to unfold more gradually.";
            if (string.Equals(direction, "Opposition", StringComparison.OrdinalIgnoreCase))
                return "Opposition is six houses apart — confrontation or denial.";
            return string.Empty;
        }

        private static int PairedHouse(int house)
        {
            if (house < 1 || house > 12) return 0;
            return house % 2 == 1 ? house + 1 : house - 1;
        }
    }
}
