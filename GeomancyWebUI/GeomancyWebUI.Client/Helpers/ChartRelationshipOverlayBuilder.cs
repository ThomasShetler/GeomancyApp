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
                    TryAddCompanyPair(links, querent, quesited, a.FromHouse, a.ToHouse, a.CompanyType, string.Empty);
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
                    TryAddPathLink(links, p.PathFromHouse, p.PathToHouse,
                        string.IsNullOrEmpty(p.BaseMode) || p.BaseMode == "None" ? p.Mode : p.BaseMode,
                        $"{(string.IsNullOrEmpty(p.BaseMode) || p.BaseMode == "None" ? p.Mode : p.BaseMode)} path H{p.PathFromHouse} → H{p.PathToHouse}.");
                }

                if (p.MadeThroughCompany || string.Equals(p.Mode, "Company", StringComparison.OrdinalIgnoreCase))
                {
                    TryAddCompanyPair(
                        links,
                        querent,
                        quesited,
                        p.AspectFromHouse > 0 ? p.AspectFromHouse : p.PathFromHouse,
                        p.AspectToHouse > 0 ? p.AspectToHouse : p.PathToHouse,
                        p.CompanyType,
                        p.PathActor);
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
                TryAddPathLink(links, t1, querent, "Trans.",
                    $"{figNote} in H{t1} touches the querent in H{querent}.");
            }

            if (quesited > 0 && t2 != quesited)
            {
                TryAddPathLink(links, t2, quesited, "Trans.",
                    $"{figNote} in H{t2} touches the quesited in H{quesited}.");
            }

            // Two distinct translator seats: also show the courier span when useful.
            if (t1 != t2 && t1 > 0 && t2 > 0)
            {
                TryAddPathLink(links, t1, t2, "Trans.",
                    $"{figNote} carries light between H{t1} and H{t2}.");
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
                    + (string.IsNullOrEmpty(p.PathFigure) ? "." : $" ({p.PathFigure})."));
            }

            if (quesited > 0 && passX != quesited)
            {
                TryAddPathLink(links, quesited, passX, "Qst. pass",
                    $"Quesited passes from H{quesited} to H{passX}"
                    + (string.IsNullOrEmpty(p.PathSecondaryFigure) ? "." : $" ({p.PathSecondaryFigure})."));
            }

            if (passQ != passX)
            {
                TryAddPathLink(links, passQ, passX, "Mutation",
                    $"Pass houses H{passQ} and H{passX} sit next to each other.");
            }
        }

        private static void TryAddPathLink(
            List<ChartAspectLink> links,
            int from,
            int to,
            string label,
            string description)
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
                Description = description
            });
        }

        private static ChartAspectLink BuildAspectLink(int from, int to, string? aspectType, string? direction)
        {
            var type = aspectType ?? string.Empty;
            var dir = direction ?? string.Empty;
            return new ChartAspectLink
            {
                FromHouse = from,
                ToHouse = to,
                AspectType = type,
                Direction = dir,
                Kind = "aspect",
                Label = GeomanticAspects.ShortLabel(type, dir),
                Description = GeomanticAspects.DescribeAspect(from, to, type, dir),
                IntermediateHouses = GeomanticAspects.IntermediateHouses(from, to)
            };
        }

        private static void TryAddCompanyPair(
            List<ChartAspectLink> links,
            int querent,
            int quesited,
            int fromHint,
            int toHint,
            string? companyType,
            string? pathActor)
        {
            var (sig, companion) = ResolveCompanyPair(querent, quesited, fromHint, toHint, pathActor);
            if (sig <= 0 || companion <= 0 || companion == sig)
                return;
            if (links.Any(l => l.Kind == "company-pair"
                && ((l.FromHouse == sig && l.ToHouse == companion)
                    || (l.FromHouse == companion && l.ToHouse == sig))))
                return;

            var shortCo = PerfectionDetailCopy.FormatCompanyShort(companyType ?? string.Empty);
            var label = string.IsNullOrEmpty(shortCo) ? "Co." : shortCo;
            var pairDesc = PerfectionDetailCopy.FormatCompanyPairLabel(sig, companion);

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
                    string.Empty,
                    sig,
                    companion)
            });
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
