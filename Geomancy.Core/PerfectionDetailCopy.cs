using System;

namespace GeomancyApp
{
    /// <summary>
    /// Reader-facing titles and short copy for perfection / aspect detail headers.
    /// Keeps company terminology secondary to the relationship mode (esp. aspects).
    /// </summary>
    public static class PerfectionDetailCopy
    {
        public static string ResolveModeTitle(
            string mode,
            string baseMode,
            string companyType,
            string aspectType)
        {
            if (string.Equals(mode, "None", StringComparison.OrdinalIgnoreCase))
                return "No Perfection";
            if (string.Equals(mode, "Impedition", StringComparison.OrdinalIgnoreCase))
                return "Impedition";

            var aspectLabel = FormatAspectType(aspectType);
            var companyLabel = FormatCompanyType(companyType);

            if (string.Equals(mode, "Company", StringComparison.OrdinalIgnoreCase))
            {
                var via = string.IsNullOrEmpty(companyLabel) ? "Company" : companyLabel;
                if (!string.IsNullOrEmpty(aspectLabel)
                    && (string.Equals(baseMode, "Aspect", StringComparison.OrdinalIgnoreCase)
                        || !string.IsNullOrEmpty(aspectType)))
                {
                    return $"{aspectLabel} · via {via}";
                }

                if (!string.IsNullOrEmpty(baseMode)
                    && !string.Equals(baseMode, "Company", StringComparison.OrdinalIgnoreCase)
                    && !string.Equals(baseMode, "None", StringComparison.OrdinalIgnoreCase)
                    && !string.Equals(baseMode, "Aspect", StringComparison.OrdinalIgnoreCase))
                {
                    return $"{baseMode} · via {via}";
                }

                return via;
            }

            if (string.Equals(mode, "Aspect", StringComparison.OrdinalIgnoreCase)
                && !string.IsNullOrEmpty(aspectLabel))
            {
                return aspectLabel;
            }

            return string.IsNullOrWhiteSpace(mode) ? "Perfection" : mode;
        }

        /// <summary>
        /// Primary glossary line under the title — aspect/mode first, never company overview.
        /// </summary>
        public static string ResolvePrimaryGlossary(
            string mode,
            string baseMode,
            string aspectType,
            string aspectDirection,
            Func<string, string, string> aspectGlossary,
            Func<string, string> modeGlossary)
        {
            var effectiveMode = string.Equals(mode, "Company", StringComparison.OrdinalIgnoreCase)
                && !string.IsNullOrWhiteSpace(baseMode)
                && !string.Equals(baseMode, "None", StringComparison.OrdinalIgnoreCase)
                ? baseMode
                : mode;

            if (string.Equals(effectiveMode, "Aspect", StringComparison.OrdinalIgnoreCase)
                || (!string.IsNullOrWhiteSpace(aspectType)
                    && !aspectType.Equals("None", StringComparison.OrdinalIgnoreCase)
                    && string.Equals(mode, "Company", StringComparison.OrdinalIgnoreCase)))
            {
                var aspectLine = aspectGlossary(aspectType, aspectDirection ?? string.Empty);
                if (!string.IsNullOrEmpty(aspectLine))
                    return aspectLine;
            }

            return modeGlossary(effectiveMode ?? string.Empty);
        }

        public static string FormatCompanyType(string companyType)
        {
            if (string.IsNullOrWhiteSpace(companyType)
                || companyType.Equals("None", StringComparison.OrdinalIgnoreCase))
                return string.Empty;

            return companyType switch
            {
                "Simple" => "Company Simple",
                "DemiSimple" => "Company Demi-Simple",
                "Compound" => "Company Compound",
                "Capitular" => "Company Capitular",
                _ => companyType.StartsWith("Company", StringComparison.OrdinalIgnoreCase)
                    ? companyType
                    : $"Company {companyType}"
            };
        }

        public static string FormatCompanyShort(string companyType)
        {
            if (string.IsNullOrWhiteSpace(companyType)
                || companyType.Equals("None", StringComparison.OrdinalIgnoreCase))
                return string.Empty;

            return companyType switch
            {
                "Simple" => "Co. Simple",
                "DemiSimple" => "Co. Demi",
                "Compound" => "Co. Comp.",
                "Capitular" => "Co. Cap.",
                _ => companyType
            };
        }

        public static string FormatAspectType(string aspectType)
        {
            if (string.IsNullOrWhiteSpace(aspectType)
                || aspectType.Equals("None", StringComparison.OrdinalIgnoreCase))
                return string.Empty;
            return aspectType.Trim();
        }

        /// <summary>
        /// Compact list label for standalone or company-mediated aspect rows.
        /// </summary>
        public static string AspectListLabel(string aspectType, bool madeThroughCompany, string companyType)
        {
            var abbrev = aspectType?.Trim().ToLowerInvariant() switch
            {
                "sextile" => "Sx",
                "trine" => "Tr",
                "square" => "Sq",
                "opposition" => "Opp",
                "conjunction" => "Cj",
                _ => FormatAspectType(aspectType)
            };

            if (string.IsNullOrEmpty(abbrev))
                return string.Empty;

            if (!madeThroughCompany
                && (string.IsNullOrWhiteSpace(companyType)
                    || companyType.Equals("None", StringComparison.OrdinalIgnoreCase)))
                return abbrev;

            var co = FormatCompanyShort(companyType);
            return string.IsNullOrEmpty(co) ? $"{abbrev} · via Co." : $"{abbrev} · via {co}";
        }

        /// <summary>
        /// One-line hover text: prefer tagline, else a short mechanism sentence.
        /// Optionally prefix with the concrete house pair (e.g. H7 with H8).
        /// </summary>
        public static string CompanyHoverText(string tagline, string mechanismSummary, int significatorHouse = 0, int companionHouse = 0)
        {
            var body = string.Empty;
            if (!string.IsNullOrWhiteSpace(tagline))
                body = tagline.Trim();
            else if (!string.IsNullOrWhiteSpace(mechanismSummary))
            {
                var m = mechanismSummary.Trim();
                body = m.Length > 180 ? m.Substring(0, 177) + "..." : m;
            }
            else
            {
                body = "Company of Houses — a paired-house companion can help form the link.";
            }

            if (significatorHouse is >= 1 and <= 12 && companionHouse is >= 1 and <= 12
                && significatorHouse != companionHouse)
            {
                return $"H{significatorHouse} with H{companionHouse} — {body}";
            }

            return body;
        }

        public static string AspectGlossary(string aspectType, string direction) =>
            GeomanticAspects.GlossaryLine(aspectType, direction ?? string.Empty);

        public static string AspectRelationLabel(int fromHouse, int toHouse, string aspectType, string direction)
        {
            if (fromHouse <= 0 || toHouse <= 0) return string.Empty;
            var described = GeomanticAspects.DescribeAspect(fromHouse, toHouse, aspectType, direction);
            if (!string.IsNullOrEmpty(described))
                return GeomanticAspects.ShortLabel(aspectType, direction);
            return string.Empty;
        }
    }
}
