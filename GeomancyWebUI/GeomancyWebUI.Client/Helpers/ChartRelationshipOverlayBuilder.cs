using System;
using System.Collections.Generic;
using System.Linq;
using GeomancyApp;
using GeomancyWebUI.Client.Models;

namespace GeomancyWebUI.Client.Helpers
{
    /// <summary>
    /// Builds house-chart overlay geometry from a selected perfection / aspect row.
    /// </summary>
    public static class ChartRelationshipOverlayBuilder
    {
        public static ChartRelationshipOverlay? FromSelection(
            PerfectionSelection? selection,
            PerfectionAnalysisModel? analysis)
        {
            if (selection == null) return null;

            var links = new List<ChartAspectLink>();
            int querent = analysis?.QuerentHouse ?? 0;
            int quesited = analysis?.QuesitedHouse ?? 0;

            if (selection.Aspect != null)
            {
                var a = selection.Aspect;
                if (a.FromHouse > 0 && a.ToHouse > 0 && a.FromHouse != a.ToHouse)
                    links.Add(BuildAspectLink(a.FromHouse, a.ToHouse, a.AspectType, a.Direction));

                if (a.MadeThroughCompany)
                {
                    TryAddCompanyPair(
                        links, querent, quesited, a.FromHouse, a.ToHouse,
                        a.CompanyType, string.Empty, string.Empty, string.Empty, string.Empty);
                    TryAddCompanyFigureTranslationPath(
                        links, querent, quesited, a.FromHouse, a.ToHouse,
                        a.CompanyType, string.Empty, string.Empty, string.Empty, string.Empty);
                }
                else
                {
                    TryGetSignificatorFigures(analysis, out var qFig, out var xFig);
                    TryAddTranslatedAspectHomePath(
                        links, querent, quesited, a.FromHouse, a.ToHouse, qFig, xFig, string.Empty);
                }
            }
            else if (selection.Perfection != null)
            {
                var p = selection.Perfection;
                querent = p.QuerentHouse > 0 ? p.QuerentHouse : querent;
                quesited = p.QuesitedHouse > 0 ? p.QuesitedHouse : quesited;

                var effectiveMode = EffectiveMode(p);
                var hasAspect = p.AspectFromHouse > 0 && p.AspectToHouse > 0
                    && p.AspectFromHouse != p.AspectToHouse
                    && !string.IsNullOrWhiteSpace(p.AspectBetweenSignificators)
                    && !p.AspectBetweenSignificators.Equals("None", StringComparison.OrdinalIgnoreCase);

                if (hasAspect)
                {
                    links.Add(BuildAspectLink(
                        p.AspectFromHouse,
                        p.AspectToHouse,
                        p.AspectBetweenSignificators,
                        p.AspectDirection));
                }

                if (IsTranslationMode(effectiveMode))
                    AddTranslationSpokes(links, p, querent, quesited);
                else if (IsMutationMode(effectiveMode))
                    AddMutationTransfers(links, p, querent, quesited);
                else if (!hasAspect
                    && p.PathFromHouse > 0 && p.PathToHouse > 0 && p.PathFromHouse != p.PathToHouse)
                {
                    var modeLabel = string.IsNullOrEmpty(p.BaseMode) || p.BaseMode == "None" ? p.Mode : p.BaseMode;
                    TryAddPathLink(links, p.PathFromHouse, p.PathToHouse,
                        modeLabel,
                        $"{modeLabel} path H{p.PathFromHouse} → H{p.PathToHouse}.",
                        role: "neutral",
                        iconKind: "mode",
                        iconVariant: (modeLabel ?? string.Empty).Trim().ToLowerInvariant());
                }

                if (p.MadeThroughCompany || string.Equals(p.Mode, "Company", StringComparison.OrdinalIgnoreCase))
                {
                    var fromHint = p.AspectFromHouse > 0 ? p.AspectFromHouse : p.PathFromHouse;
                    var toHint = p.AspectToHouse > 0 ? p.AspectToHouse : p.PathToHouse;
                    var (sigPreview, companionPreview) = ResolveCompanyPair(
                        querent, quesited, fromHint, toHint, p.PathActor);
                    var sigFig = FigureForCompanySeat(sigPreview, querent, quesited, p.QuerentFigure, p.QuesitedFigure, p.PathFigure);
                    var coFig = FigureForCompanySeat(companionPreview, querent, quesited, p.QuerentFigure, p.QuesitedFigure, p.PathFigure);
                    TryAddCompanyPair(
                        links,
                        querent,
                        quesited,
                        fromHint,
                        toHint,
                        p.CompanyType,
                        p.CompanyTypeDescription,
                        p.PathActor,
                        sigFig,
                        coFig);
                    if (hasAspect)
                    {
                        TryAddCompanyFigureTranslationPath(
                            links,
                            querent,
                            quesited,
                            p.AspectFromHouse,
                            p.AspectToHouse,
                            p.CompanyType,
                            p.CompanyTypeDescription,
                            p.PathActor,
                            sigFig,
                            coFig);
                    }
                }
                else if (hasAspect)
                {
                    TryAddTranslatedAspectHomePath(
                        links,
                        querent,
                        quesited,
                        p.AspectFromHouse,
                        p.AspectToHouse,
                        p.QuerentFigure,
                        p.QuesitedFigure,
                        p.PathFigure);
                }
            }

            if (links.Count == 0 && querent <= 0 && quesited <= 0)
                return null;

            return new ChartRelationshipOverlay
            {
                Links = links,
                QuerentHouse = querent,
                QuesitedHouse = quesited
            };
        }

        public static HashSet<int> HighlightHouses(ChartRelationshipOverlay? overlay)
        {
            var set = new HashSet<int>();
            if (overlay == null) return set;

            if (overlay.QuerentHouse is >= 1 and <= 12) set.Add(overlay.QuerentHouse);
            if (overlay.QuesitedHouse is >= 1 and <= 12) set.Add(overlay.QuesitedHouse);

            foreach (var link in overlay.Links)
            {
                if (link.FromHouse is >= 1 and <= 12) set.Add(link.FromHouse);
                if (link.ToHouse is >= 1 and <= 12) set.Add(link.ToHouse);
                if (link.IntermediateHouses != null)
                {
                    foreach (var h in link.IntermediateHouses)
                    {
                        if (h is >= 1 and <= 12) set.Add(h);
                    }
                }
            }

            return set;
        }

        public static bool HasDrawableGeometry(ChartRelationshipOverlay? overlay) =>
            overlay != null && (overlay.Links.Count > 0
                || (overlay.QuerentHouse is >= 1 and <= 12)
                || (overlay.QuesitedHouse is >= 1 and <= 12));

        /// <summary>
        /// Auto-switch to the 12-house chart for aspect, company, translation, and mutation geometry.
        /// </summary>
        public static bool ShouldAutoSwitchToHouseChart(ChartRelationshipOverlay? overlay)
        {
            if (overlay?.Links == null || overlay.Links.Count == 0)
                return false;

            foreach (var link in overlay.Links)
            {
                if (string.Equals(link.Kind, "aspect", StringComparison.OrdinalIgnoreCase)
                    || string.Equals(link.Kind, "company-pair", StringComparison.OrdinalIgnoreCase)
                    || string.Equals(link.Kind, "company-pass", StringComparison.OrdinalIgnoreCase)
                    || string.Equals(link.Kind, "path", StringComparison.OrdinalIgnoreCase))
                    return true;
            }

            return false;
        }

        private static string EffectiveMode(PerfectionModel p)
        {
            if (string.Equals(p.Mode, "Company", StringComparison.OrdinalIgnoreCase)
                && !string.IsNullOrWhiteSpace(p.BaseMode)
                && !p.BaseMode.Equals("None", StringComparison.OrdinalIgnoreCase))
                return p.BaseMode;
            return p.Mode ?? string.Empty;
        }

        private static bool IsTranslationMode(string mode) =>
            string.Equals(mode, "Translation", StringComparison.OrdinalIgnoreCase);

        private static bool IsMutationMode(string mode) =>
            string.Equals(mode, "Mutation", StringComparison.OrdinalIgnoreCase);

        /// <summary>
        /// Translation: draw spokes from translator house(s) to querent and quesited
        /// (supports the shared-house case Hn→Hn when one seat is adjacent to both).
        /// </summary>
        private static void AddTranslationSpokes(
            List<ChartAspectLink> links,
            PerfectionModel p,
            int querent,
            int quesited)
        {
            int t1 = p.TranslatorHouse > 0 ? p.TranslatorHouse : p.PathFromHouse;
            int t2 = p.TranslatorHouseSecondary > 0 ? p.TranslatorHouseSecondary
                : (p.PathToHouse > 0 ? p.PathToHouse : t1);
            if (t1 <= 0) t1 = t2;
            if (t2 <= 0) t2 = t1;
            if (t1 <= 0) return;

            var fig = string.IsNullOrEmpty(p.TranslatorFigure) ? p.PathFigure : p.TranslatorFigure;
            var figNote = string.IsNullOrEmpty(fig) ? "translator" : fig;

            if (querent > 0 && t1 != querent)
            {
                TryAddPathLink(links, t1, querent, "Qrt",
                    $"{figNote} in H{t1} touches the querent in H{querent}.",
                    role: "querent",
                    iconKind: "mode",
                    iconVariant: "translation");
            }

            if (quesited > 0 && t2 != quesited)
            {
                TryAddPathLink(links, t2, quesited, "Qst",
                    $"{figNote} in H{t2} touches the quesited in H{quesited}.",
                    role: "quesited",
                    iconKind: "mode",
                    iconVariant: "translation");
            }

            // Two distinct translator seats: also show the courier span when useful.
            if (t1 != t2 && t1 > 0 && t2 > 0)
            {
                TryAddPathLink(links, t1, t2, "Trans.",
                    $"{figNote} carries light between H{t1} and H{t2}.",
                    role: "neutral",
                    iconKind: "mode",
                    iconVariant: "translation");
            }
        }

        /// <summary>
        /// Mutation: show each significator's pass, then the meeting adjacency between pass houses.
        /// </summary>
        private static void AddMutationTransfers(
            List<ChartAspectLink> links,
            PerfectionModel p,
            int querent,
            int quesited)
        {
            int passQ = p.PathFromHouse;
            int passX = p.PathToHouse;
            if (passQ <= 0 || passX <= 0)
                return;

            if (querent > 0 && passQ != querent)
            {
                TryAddPathLink(links, querent, passQ, "Q. pass",
                    $"Querent passes from H{querent} to H{passQ}"
                    + (string.IsNullOrEmpty(p.PathFigure) ? "." : $" ({p.PathFigure})."),
                    role: "querent",
                    iconKind: "mode",
                    iconVariant: "mutation");
            }

            if (quesited > 0 && passX != quesited)
            {
                TryAddPathLink(links, quesited, passX, "Qst. pass",
                    $"Quesited passes from H{quesited} to H{passX}"
                    + (string.IsNullOrEmpty(p.PathSecondaryFigure) ? "." : $" ({p.PathSecondaryFigure})."),
                    role: "quesited",
                    iconKind: "mode",
                    iconVariant: "mutation");
            }

            if (passQ != passX)
            {
                TryAddPathLink(links, passQ, passX, "Mutation",
                    $"Pass houses H{passQ} and H{passX} sit next to each other.",
                    role: "neutral",
                    iconKind: "mode",
                    iconVariant: "mutation");
            }
        }

        private static void TryAddPathLink(
            List<ChartAspectLink> links,
            int from,
            int to,
            string label,
            string description,
            string role = "neutral",
            string iconKind = "",
            string iconVariant = "")
        {
            if (from < 1 || from > 12 || to < 1 || to > 12 || from == to)
                return;
            if (links.Any(l => l.Kind == "path"
                && ((l.FromHouse == from && l.ToHouse == to)
                    || (l.FromHouse == to && l.ToHouse == from))))
                return;

            links.Add(new ChartAspectLink
            {
                FromHouse = from,
                ToHouse = to,
                Kind = "path",
                Label = label,
                Description = description,
                Role = string.IsNullOrWhiteSpace(role) ? "neutral" : role,
                IconKind = iconKind ?? string.Empty,
                IconVariant = iconVariant ?? string.Empty
            });
        }

        private static ChartAspectLink BuildAspectLink(int from, int to, string? aspectType, string? direction)
        {
            var type = aspectType ?? string.Empty;
            var dir = direction ?? string.Empty;
            var variant = string.IsNullOrWhiteSpace(type)
                ? "generic"
                : type.Trim().ToLowerInvariant();
            return new ChartAspectLink
            {
                FromHouse = from,
                ToHouse = to,
                AspectType = type,
                Direction = dir,
                Kind = "aspect",
                Label = GeomanticAspects.ShortLabel(type, dir),
                Description = GeomanticAspects.DescribeAspect(from, to, type, dir),
                IntermediateHouses = GeomanticAspects.IntermediateHouses(from, to),
                Role = "neutral",
                IconKind = "aspect",
                IconVariant = variant
            };
        }

        private static void TryAddCompanyPair(
            List<ChartAspectLink> links,
            int querent,
            int quesited,
            int fromHint,
            int toHint,
            string? companyType,
            string? companyTypeDescription,
            string? pathActor,
            string? significatorFigure,
            string? companionFigure)
        {
            var (sig, companion) = ResolveCompanyPair(querent, quesited, fromHint, toHint, pathActor);
            if (sig <= 0 || companion <= 0 || companion == sig)
                return;
            if (links.Any(l => l.Kind == "company-pair"
                && ((l.FromHouse == sig && l.ToHouse == companion)
                    || (l.FromHouse == companion && l.ToHouse == sig))))
                return;

            var label = PerfectionDetailCopy.FormatCompanyConnectorLabel(
                companyType ?? string.Empty,
                companyTypeDescription ?? string.Empty,
                sig,
                companion,
                significatorFigure,
                companionFigure);
            var pairDesc = PerfectionDetailCopy.FormatCompanyPairLabel(
                sig, companion, significatorFigure, companionFigure);
            var companyVariant = ChartLinkIconGlyphs.NormalizeCompanyVariant(companyType);

            links.Add(new ChartAspectLink
            {
                FromHouse = sig,
                ToHouse = companion,
                Kind = "company-pair",
                Label = label,
                Description = PerfectionDetailCopy.CompanyHoverText(
                    string.IsNullOrEmpty(pairDesc)
                        ? $"{PerfectionDetailCopy.FormatCompanyType(companyType ?? string.Empty)} — odd–even paired houses only."
                        : $"{pairDesc}. {PerfectionDetailCopy.FormatCompanyType(companyType ?? string.Empty)} — odd–even paired houses only.",
                    PerfectionDetailCopy.CompanyMechanismFormationClause(
                        companyType ?? string.Empty,
                        companyTypeDescription ?? string.Empty),
                    sig,
                    companion),
                Role = "neutral",
                IconKind = "company",
                IconVariant = companyVariant
            });
        }

        /// <summary>
        /// When the company figure casts from a translated seat (not the companion house itself),
        /// draw companion → cast-from so the chart shows why that aspect belongs to the company bond.
        /// Example: H11↔H12 company, Laetitia also in H5 casting Dex Tr to H1 → path H12 → H5.
        /// </summary>
        private static void TryAddCompanyFigureTranslationPath(
            List<ChartAspectLink> links,
            int querent,
            int quesited,
            int aspectFrom,
            int aspectTo,
            string? companyType,
            string? companyTypeDescription,
            string? pathActor,
            string? significatorFigure,
            string? companionFigure)
        {
            if (aspectFrom < 1 || aspectFrom > 12 || aspectTo < 1 || aspectTo > 12)
                return;

            var (sig, companion) = ResolveCompanyPair(querent, quesited, aspectFrom, aspectTo, pathActor);
            if (companion <= 0 || sig <= 0)
                return;

            // Direct cast from the companion seat — company pair + aspect already tell the story.
            if (aspectFrom == companion || aspectFrom == sig)
                return;

            var label = PerfectionDetailCopy.FormatCompanyConnectorLabel(
                companyType ?? string.Empty,
                companyTypeDescription ?? string.Empty,
                sig,
                companion,
                significatorFigure,
                companionFigure);
            if (string.IsNullOrEmpty(label) || label == "Co.")
                label = "Co. fig.";
            if (links.Any(l =>
                    (l.Kind == "path" || l.Kind == "company-pass")
                    && ((l.FromHouse == companion && l.ToHouse == aspectFrom)
                        || (l.FromHouse == aspectFrom && l.ToHouse == companion))))
                return;

            links.Add(new ChartAspectLink
            {
                FromHouse = companion,
                ToHouse = aspectFrom,
                Kind = "company-pass",
                Label = label,
                Description =
                    $"Company figure from H{companion} also appears in H{aspectFrom}, and from there aspects H{aspectTo}.",
                Role = "neutral",
                IconKind = "company",
                IconVariant = ChartLinkIconGlyphs.NormalizeCompanyVariant(companyType)
            });
        }

        /// <summary>
        /// Non-company translated aspect: link the cast seat back to the significator home
        /// whose figure is casting (e.g. Sin Sq H10→H1 with quesited figure in H10 → path H10↔H6).
        /// </summary>
        private static void TryAddTranslatedAspectHomePath(
            List<ChartAspectLink> links,
            int querent,
            int quesited,
            int aspectFrom,
            int aspectTo,
            string? querentFigure,
            string? quesitedFigure,
            string? castFigure)
        {
            if (aspectFrom < 1 || aspectFrom > 12)
                return;
            if (aspectFrom == querent || aspectFrom == quesited)
                return;

            var (home, role) = ResolveTranslatedAspectHome(
                castFigure, aspectTo, querent, quesited, querentFigure, quesitedFigure);
            if (home <= 0 || home == aspectFrom)
                return;

            var label = role switch
            {
                "querent" => "Q.",
                "quesited" => "Qst.",
                _ => "Home"
            };
            var figNote = string.IsNullOrWhiteSpace(castFigure) ? "This figure" : castFigure.Trim();
            TryAddPathLink(
                links,
                aspectFrom,
                home,
                label,
                $"{figNote} in H{aspectFrom} reflects the {role} significator in H{home}.",
                role: string.IsNullOrEmpty(role) ? "neutral" : role,
                iconKind: "mode",
                iconVariant: "generic");
        }

        private static string FigureForCompanySeat(
            int seat,
            int querent,
            int quesited,
            string? querentFigure,
            string? quesitedFigure,
            string? pathFigure)
        {
            if (seat <= 0)
                return pathFigure ?? string.Empty;
            if (seat == querent && !string.IsNullOrWhiteSpace(querentFigure))
                return querentFigure!;
            if (seat == quesited && !string.IsNullOrWhiteSpace(quesitedFigure))
                return quesitedFigure!;
            return pathFigure ?? string.Empty;
        }

        private static (int homeHouse, string role) ResolveTranslatedAspectHome(
            string? castFigure,
            int aspectToHouse,
            int querentHouse,
            int quesitedHouse,
            string? querentFigure,
            string? quesitedFigure)
        {
            var castRoot = FigureNameHelper.Root(castFigure ?? string.Empty);
            var qRoot = FigureNameHelper.Root(querentFigure ?? string.Empty);
            var xRoot = FigureNameHelper.Root(quesitedFigure ?? string.Empty);

            if (!string.IsNullOrEmpty(castRoot) && castRoot.Equals(qRoot, StringComparison.OrdinalIgnoreCase)
                && querentHouse > 0)
                return (querentHouse, "querent");

            if (!string.IsNullOrEmpty(castRoot) && castRoot.Equals(xRoot, StringComparison.OrdinalIgnoreCase)
                && quesitedHouse > 0)
                return (quesitedHouse, "quesited");

            // Cast toward one significator usually means the other side's figure has moved.
            if (aspectToHouse == quesitedHouse && querentHouse > 0)
                return (querentHouse, "querent");
            if (aspectToHouse == querentHouse && quesitedHouse > 0)
                return (quesitedHouse, "quesited");

            return (0, string.Empty);
        }

        private static void TryGetSignificatorFigures(
            PerfectionAnalysisModel? analysis,
            out string querentFigure,
            out string quesitedFigure)
        {
            querentFigure = string.Empty;
            quesitedFigure = string.Empty;
            if (analysis == null)
                return;

            foreach (var p in analysis.Perfections.Concat(analysis.Denials))
            {
                if (string.IsNullOrEmpty(querentFigure) && !string.IsNullOrEmpty(p.QuerentFigure))
                    querentFigure = p.QuerentFigure;
                if (string.IsNullOrEmpty(quesitedFigure) && !string.IsNullOrEmpty(p.QuesitedFigure))
                    quesitedFigure = p.QuesitedFigure;
                if (!string.IsNullOrEmpty(querentFigure) && !string.IsNullOrEmpty(quesitedFigure))
                    return;
            }
        }

        /// <summary>
        /// Resolve which significator is in company. Prefer cast-to (company aims at the other party),
        /// then companion-hint / actor, then querent fallback.
        /// </summary>
        private static (int sig, int companion) ResolveCompanyPair(
            int querent,
            int quesited,
            int fromHint,
            int toHint,
            string? pathActor)
        {
            // Company casts toward the other significator — toHint names that target.
            if (toHint == querent && quesited > 0)
                return (quesited, PairedHouse(quesited));
            if (toHint == quesited && querent > 0)
                return (querent, PairedHouse(querent));

            if (fromHint == PairedHouse(querent) && querent > 0)
                return (querent, fromHint);
            if (fromHint == PairedHouse(quesited) && quesited > 0)
                return (quesited, fromHint);

            if (querent > 0 && (fromHint == querent
                || string.Equals(pathActor, "Q.", StringComparison.OrdinalIgnoreCase)))
                return (querent, PairedHouse(querent));
            if (quesited > 0 && (fromHint == quesited
                || (pathActor ?? string.Empty).StartsWith("Qst", StringComparison.OrdinalIgnoreCase)))
                return (quesited, PairedHouse(quesited));

            if (querent > 0)
                return (querent, PairedHouse(querent));
            if (quesited > 0)
                return (quesited, PairedHouse(quesited));
            return (0, 0);
        }

        private static int PairedHouse(int house)
        {
            if (house < 1 || house > 12) return 0;
            return house % 2 == 1 ? house + 1 : house - 1;
        }
    }
}
