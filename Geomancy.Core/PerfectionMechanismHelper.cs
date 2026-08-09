using System;
using System.Collections.Generic;

namespace GeomancyApp
{
    /// <summary>
    /// Structured "How this formed" explanation for aspect / company-aspect relationships.
    /// Built from typed cast fields — never from regex of note prose.
    /// </summary>
    public static class PerfectionMechanismHelper
    {
        public sealed class MechanismExplanation
        {
            public string Title { get; set; } = "How this formed";
            public List<string> Steps { get; set; } = new List<string>();
            public CastSummary Cast { get; set; }
            public string DirectionHint { get; set; } = string.Empty;
            public bool HasStructuredCast => Cast != null && Cast.FromHouse > 0 && Cast.ToHouse > 0;
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

            bool isCompanyAspect = madeThroughCompany
                || string.Equals(mode, "Company", StringComparison.OrdinalIgnoreCase)
                || string.Equals(baseMode, "Aspect", StringComparison.OrdinalIgnoreCase)
                   && madeThroughCompany;

            if (isCompanyAspect || madeThroughCompany)
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

            var dirLabel = string.IsNullOrEmpty(explanation.Cast.Direction)
                ? aspectType
                : $"{explanation.Cast.Direction} {aspectType}";

            explanation.Steps.Add(
                $"{fromFigure} in House {aspectFromHouse} casts a {dirLabel} to House {aspectToHouse} ({toFigure}).");

            explanation.DirectionHint = DirectionHintText(explanation.Cast.Direction);
            return explanation;
        }

        /// <summary>
        /// Build from a perfection engine result plus figure names at cast houses.
        /// </summary>
        public static MechanismExplanation ExplainFromPerfectionResult(
            PerfectionResult result,
            string fromFigure,
            string toFigure)
        {
            if (result == null)
                return new MechanismExplanation();

            bool isAspect = result.Mode == PerfectionType.Aspect
                || (result.Mode == PerfectionType.Company && result.BaseMode == PerfectionType.Aspect);

            if (!isAspect || result.AspectFromHouse <= 0 || result.AspectToHouse <= 0)
                return new MechanismExplanation();

            return ExplainAspectCast(
                result.AspectFromHouse,
                result.AspectToHouse,
                fromFigure,
                toFigure,
                result.AspectBetweenSignificators.ToString(),
                result.AspectDirection,
                result.QuerentHouse,
                result.QuesitedHouse,
                null,
                null,
                result.MadeThroughCompany || result.Mode == PerfectionType.Company,
                result.CompanyType.ToString(),
                result.CompanyTypeDescription,
                result.Mode.ToString(),
                result.BaseMode != PerfectionType.None ? result.BaseMode.ToString() : string.Empty);
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
                // Direct company-house cast when significator house itself is the company actor.
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

        private static string CompanyTypeLabel(string companyType, string companyTypeDescription)
        {
            if (!string.IsNullOrWhiteSpace(companyTypeDescription))
            {
                // Descriptions often look like "Company Compound (opposite figures)" — shorten.
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
