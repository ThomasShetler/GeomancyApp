using System;
using System.Collections.Generic;

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

        /// <summary>
        /// Short structural reason the company bond qualifies (paren from engine description, or type default).
        /// Prefer <see cref="CompanyMechanismFormationClause"/> for reader-facing "paired under" wording.
        /// </summary>
        public static string CompanyFormationReason(string companyType, string companyTypeDescription)
        {
            if (!string.IsNullOrWhiteSpace(companyTypeDescription))
            {
                var d = companyTypeDescription.Trim();
                var open = d.IndexOf('(');
                var close = d.IndexOf(')');
                if (open >= 0 && close > open)
                {
                    var reason = d.Substring(open + 1, close - open - 1).Trim();
                    if (!string.IsNullOrEmpty(reason))
                        return reason;
                }
            }

            return companyType switch
            {
                "Simple" => "same figure",
                "DemiSimple" => "same planetary patron (or Caput/Cauda node rule)",
                "Compound" => "compound opposite figures",
                "Capitular" => "same Fire / head line only",
                _ => string.Empty
            };
        }

        /// <summary>
        /// Binding planet for Demi-Simple from engine description paren text.
        /// Handles "same planet: Jupiter" and "Caput Draconis with Jupiter".
        /// </summary>
        public static string ExtractCompanyBondPlanet(string companyTypeDescription)
        {
            var reason = CompanyFormationReason("DemiSimple", companyTypeDescription);
            if (string.IsNullOrEmpty(reason))
                return string.Empty;

            const string samePlanetPrefix = "same planet:";
            if (reason.StartsWith(samePlanetPrefix, StringComparison.OrdinalIgnoreCase))
                return reason.Substring(samePlanetPrefix.Length).Trim();

            var withIdx = reason.LastIndexOf(" with ", StringComparison.OrdinalIgnoreCase);
            if (withIdx > 0 && withIdx + 6 < reason.Length)
                return reason.Substring(withIdx + 6).Trim();

            return string.Empty;
        }

        /// <summary>
        /// Compact mechanism clause: "paired under Jupiter", "identical figures", etc.
        /// </summary>
        public static string CompanyMechanismFormationClause(string companyType, string companyTypeDescription)
        {
            var key = (companyType ?? string.Empty).Trim();
            if (key.Equals("DemiSimple", StringComparison.OrdinalIgnoreCase)
                || key.IndexOf("Demi", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                var planet = ExtractCompanyBondPlanet(companyTypeDescription);
                if (!string.IsNullOrEmpty(planet))
                    return $"paired under {planet}";
                return "paired under the same planetary patron";
            }

            if (key.Equals("Simple", StringComparison.OrdinalIgnoreCase))
                return "identical figures";
            if (key.Equals("Compound", StringComparison.OrdinalIgnoreCase))
                return "compound opposite figures";
            if (key.Equals("Capitular", StringComparison.OrdinalIgnoreCase))
                return "the same Fire / head line only";

            var fallback = CompanyFormationReason(companyType, companyTypeDescription);
            return string.IsNullOrEmpty(fallback) ? string.Empty : fallback;
        }

        /// <summary>
        /// "This chart" sentence naming houses, figures, querent/quesited role, and Demi planet bond.
        /// </summary>
        public static string CompanyThisChartSentence(
            int significatorHouse,
            string significatorFigure,
            string significatorRole,
            int companionHouse,
            string companionFigure,
            string companyType,
            string companyTypeDescription)
        {
            if (significatorHouse is < 1 or > 12 || companionHouse is < 1 or > 12
                || significatorHouse == companionHouse)
                return string.Empty;

            var sigFig = string.IsNullOrWhiteSpace(significatorFigure) ? "its figure" : significatorFigure.Trim();
            var coFig = string.IsNullOrWhiteSpace(companionFigure) ? "its companion" : companionFigure.Trim();
            var role = string.IsNullOrWhiteSpace(significatorRole) ? "significator" : significatorRole.Trim().ToLowerInvariant();

            var key = (companyType ?? string.Empty).Trim();
            if (key.Equals("DemiSimple", StringComparison.OrdinalIgnoreCase)
                || key.IndexOf("Demi", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                var planet = ExtractCompanyBondPlanet(companyTypeDescription);
                var under = string.IsNullOrEmpty(planet) ? "the same planetary patron" : planet;
                return $"House {significatorHouse} ({sigFig}), the {role}, and House {companionHouse} ({coFig}) next to it are both paired under {under}.";
            }

            if (key.Equals("Simple", StringComparison.OrdinalIgnoreCase))
            {
                return $"House {significatorHouse} ({sigFig}), the {role}, and House {companionHouse} ({coFig}) next to it hold the same figure.";
            }

            if (key.Equals("Compound", StringComparison.OrdinalIgnoreCase)
                || key.IndexOf("Compound", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                var pair = MatchCompoundOppositePair(sigFig, coFig);
                if (pair != null)
                {
                    return $"House {significatorHouse} ({sigFig}), the {role}, and House {companionHouse} ({coFig}) next to it are compound opposite figures ({pair.Value.Left} ↔ {pair.Value.Right}).";
                }

                return $"House {significatorHouse} ({sigFig}), the {role}, and House {companionHouse} ({coFig}) next to it are compound opposite figures.";
            }

            if (key.Equals("Capitular", StringComparison.OrdinalIgnoreCase))
            {
                return $"House {significatorHouse} ({sigFig}), the {role}, and House {companionHouse} ({coFig}) next to it share only the Fire / head line.";
            }

            var clause = CompanyMechanismFormationClause(companyType, companyTypeDescription);
            if (string.IsNullOrEmpty(clause))
                return $"House {significatorHouse} ({sigFig}), the {role}, is in company with House {companionHouse} ({coFig}).";
            return $"House {significatorHouse} ({sigFig}), the {role}, and House {companionHouse} ({coFig}) next to it qualify as {clause}.";
        }

        /// <summary>
        /// Greer Table 6-2 planetary groups for Demi-Simple company (planet → figures).
        /// </summary>
        public static IReadOnlyList<(string Planet, string Figures)> DemiSimplePlanetTable { get; } =
            new List<(string, string)>
            {
                ("Saturn", "Carcer, Tristitia, Cauda Draconis"),
                ("Jupiter", "Acquisitio, Laetitia, Caput Draconis"),
                ("Mars", "Puer, Rubeus, Cauda Draconis"),
                ("Sun", "Fortuna Major, Fortuna Minor"),
                ("Venus", "Amissio, Puella, Caput Draconis"),
                ("Mercury", "Albus, Conjunctio"),
                ("Moon", "Populus, Via"),
            };

        public static string DemiSimpleHowFormsIntro =>
            "Paired figures share the same planet from Greer Table 6-2 (Caput with Jupiter/Venus; Cauda with Saturn/Mars):";

        /// <summary>
        /// Greer Table 6-2 opposite pairs for Company Compound.
        /// </summary>
        public static IReadOnlyList<(string Left, string Right)> CompoundOppositePairs { get; } =
            new List<(string, string)>
            {
                ("Puer", "Puella"),
                ("Amissio", "Acquisitio"),
                ("Albus", "Rubeus"),
                ("Populus", "Via"),
                ("Fortuna Major", "Fortuna Minor"),
                ("Conjunctio", "Carcer"),
                ("Tristitia", "Laetitia"),
                ("Cauda Draconis", "Caput Draconis"),
            };

        public static string CompoundHowFormsIntro =>
            "Company Compound exists between these opposite figure pairs:";

        /// <summary>
        /// Greer house-count geometry for classical aspects (houses between cast seats).
        /// </summary>
        public static IReadOnlyList<(string Aspect, int HousesBetween, string Reading)> AspectGeometryTable { get; } =
            new List<(string, int, string)>
            {
                ("Sextile", 1, "Mild favorable aspect"),
                ("Square", 2, "Challenging aspect of friction or tests"),
                ("Trine", 3, "Strong favorable aspect of easy agreement"),
                ("Opposition", 5, "Unfavorable aspect of confrontation or denial"),
            };

        public static string AspectHowFormsIntro =>
            "Geomantic aspects are house-wheel relationships counted by how many houses sit between the cast seats (not by figure affinity):";

        /// <summary>
        /// Resolve which geometry table row matches this aspect type.
        /// </summary>
        public static (string Aspect, int HousesBetween, string Reading)? MatchAspectGeometryRow(string aspectType)
        {
            var label = FormatAspectType(aspectType);
            if (string.IsNullOrEmpty(label))
                return null;

            foreach (var row in AspectGeometryTable)
            {
                if (row.Aspect.Equals(label, StringComparison.OrdinalIgnoreCase))
                    return row;
            }

            return null;
        }

        public static bool IsAspectGeometryRowActive(string rowAspect, string aspectType) =>
            MatchAspectGeometryRow(aspectType) is { } matched
            && matched.Aspect.Equals(rowAspect, StringComparison.OrdinalIgnoreCase);

        /// <summary>
        /// "This chart" sentence naming cast houses, figures, aspect geometry, and significator roles.
        /// </summary>
        public static string AspectThisChart(
            int fromHouse,
            string fromFigure,
            int toHouse,
            string toFigure,
            string aspectType,
            string direction,
            int querentHouse = 0,
            int quesitedHouse = 0)
        {
            if (fromHouse is < 1 or > 12 || toHouse is < 1 or > 12)
                return string.Empty;

            var aspectLabel = FormatAspectType(aspectType);
            if (string.IsNullOrEmpty(aspectLabel))
                return string.Empty;

            var fromFig = string.IsNullOrWhiteSpace(fromFigure) ? "its figure" : fromFigure.Trim();
            var toFig = string.IsNullOrWhiteSpace(toFigure) ? "its figure" : toFigure.Trim();

            var dir = (direction ?? string.Empty).Trim();
            bool dexter = dir.IndexOf("Dexter", StringComparison.OrdinalIgnoreCase) >= 0;
            bool sinister = dir.IndexOf("Sinister", StringComparison.OrdinalIgnoreCase) >= 0;
            bool opposition = aspectLabel.Equals("Opposition", StringComparison.OrdinalIgnoreCase)
                || dir.Equals("Opposition", StringComparison.OrdinalIgnoreCase);
            bool conjunction = aspectLabel.Equals("Conjunction", StringComparison.OrdinalIgnoreCase);

            string titledAspect;
            if (opposition || conjunction)
                titledAspect = aspectLabel;
            else if (dexter)
                titledAspect = $"Dexter {aspectLabel}";
            else if (sinister)
                titledAspect = $"Sinister {aspectLabel}";
            else
                titledAspect = aspectLabel;

            var fromRole = AspectSeatRoleClause(fromHouse, querentHouse, quesitedHouse);
            var toRole = AspectSeatRoleClause(toHouse, querentHouse, quesitedHouse);

            var cast = $"{fromFig} in House {fromHouse}{fromRole} casts a {titledAspect} to {toFig} in House {toHouse}{toRole}";

            if (conjunction)
                return $"{cast}: both occupy the same house seat.";

            var geometry = GeomanticAspects.DescribeAspect(fromHouse, toHouse, aspectType, direction);
            // DescribeAspect already includes type/houses; prefer the between + direction clause after the em dash.
            var dashIdx = geometry.IndexOf(" — ", StringComparison.Ordinal);
            var geometryTail = dashIdx >= 0 && dashIdx + 3 < geometry.Length
                ? geometry.Substring(dashIdx + 3).Trim()
                : geometry;

            var intermediates = GeomanticAspects.IntermediateHouses(fromHouse, toHouse);
            string betweenDetail = string.Empty;
            if (intermediates.Count > 0)
            {
                betweenDetail = intermediates.Count == 1
                    ? $" (House {intermediates[0]} between them)"
                    : $" (Houses {string.Join(", ", intermediates)} between them)";
            }

            if (!string.IsNullOrEmpty(geometryTail))
            {
                geometryTail = geometryTail.TrimEnd('.');
                // Avoid duplicating the parenthetical if DescribeAspect already named houses.
                if (!string.IsNullOrEmpty(betweenDetail)
                    && geometryTail.IndexOf("between H", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    return $"{cast}: {geometryTail}.";
                }

                return $"{cast}: {geometryTail}{betweenDetail}.";
            }

            return $"{cast}{betweenDetail}.";
        }

        private static string AspectSeatRoleClause(int house, int querentHouse, int quesitedHouse)
        {
            if (house > 0 && house == querentHouse && house == quesitedHouse)
                return " (querent and quesited)";
            if (house > 0 && house == querentHouse)
                return " (querent)";
            if (house > 0 && house == quesitedHouse)
                return " (quesited)";
            if (querentHouse > 0 || quesitedHouse > 0)
                return " (translated seat)";
            return string.Empty;
        }

        public static bool IsDemiSimpleCompanyType(string companyTypeKey) =>
            !string.IsNullOrWhiteSpace(companyTypeKey)
            && (companyTypeKey.Equals("DemiSimple", StringComparison.OrdinalIgnoreCase)
                || companyTypeKey.IndexOf("Demi", StringComparison.OrdinalIgnoreCase) >= 0);

        public static bool IsCompoundCompanyType(string companyTypeKey) =>
            !string.IsNullOrWhiteSpace(companyTypeKey)
            && companyTypeKey.IndexOf("Compound", StringComparison.OrdinalIgnoreCase) >= 0;

        /// <summary>
        /// Resolve which compound opposite row these two figures occupy (order-insensitive).
        /// </summary>
        public static (string Left, string Right)? MatchCompoundOppositePair(string figureA, string figureB)
        {
            var a = FigureNameHelper.Root(figureA ?? string.Empty);
            var b = FigureNameHelper.Root(figureB ?? string.Empty);
            if (string.IsNullOrEmpty(a) || string.IsNullOrEmpty(b))
                return null;

            foreach (var row in CompoundOppositePairs)
            {
                if ((row.Left.Equals(a, StringComparison.OrdinalIgnoreCase)
                        && row.Right.Equals(b, StringComparison.OrdinalIgnoreCase))
                    || (row.Left.Equals(b, StringComparison.OrdinalIgnoreCase)
                        && row.Right.Equals(a, StringComparison.OrdinalIgnoreCase)))
                {
                    return row;
                }
            }

            return null;
        }

        public static bool IsCompoundPairRowActive(string left, string right, string figureA, string figureB) =>
            MatchCompoundOppositePair(figureA, figureB) is { } matched
            && matched.Left.Equals(left, StringComparison.OrdinalIgnoreCase)
            && matched.Right.Equals(right, StringComparison.OrdinalIgnoreCase);

        /// <summary>
        /// Interpretive sentence after the em dash in CompanyTypeDescription, if present.
        /// </summary>
        public static string CompanyFormationReading(string companyTypeDescription)
        {
            if (string.IsNullOrWhiteSpace(companyTypeDescription))
                return string.Empty;

            var d = companyTypeDescription.Trim();
            var idx = d.IndexOf(" \u2014 ", StringComparison.Ordinal); // " — "
            if (idx < 0)
                idx = d.IndexOf(" - ", StringComparison.Ordinal);
            if (idx < 0 || idx + 3 >= d.Length)
                return string.Empty;

            return d.Substring(idx + 3).Trim();
        }

        public static string FormatCompanyPairLabel(
            int significatorHouse,
            int companionHouse,
            string significatorFigure = null,
            string companionFigure = null)
        {
            if (significatorHouse is < 1 or > 12 || companionHouse is < 1 or > 12
                || significatorHouse == companionHouse)
                return string.Empty;

            var left = $"H{significatorHouse}";
            if (!string.IsNullOrWhiteSpace(significatorFigure))
                left += $" · {significatorFigure.Trim()}";
            var right = $"H{companionHouse}";
            if (!string.IsNullOrWhiteSpace(companionFigure))
                right += $" · {companionFigure.Trim()}";
            return $"{left} ↔ {right}";
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
