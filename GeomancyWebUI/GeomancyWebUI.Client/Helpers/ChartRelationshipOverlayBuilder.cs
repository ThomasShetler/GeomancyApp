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
                    TryAddCompanyPair(links, querent, quesited, a.FromHouse, a.CompanyType);
            }
            else if (selection.Perfection != null)
            {
                var p = selection.Perfection;
                querent = p.QuerentHouse > 0 ? p.QuerentHouse : querent;
                quesited = p.QuesitedHouse > 0 ? p.QuesitedHouse : quesited;

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
                else if (p.PathFromHouse > 0 && p.PathToHouse > 0 && p.PathFromHouse != p.PathToHouse)
                {
                    links.Add(new ChartAspectLink
                    {
                        FromHouse = p.PathFromHouse,
                        ToHouse = p.PathToHouse,
                        Kind = "path",
                        Label = string.IsNullOrEmpty(p.BaseMode) || p.BaseMode == "None" ? p.Mode : p.BaseMode,
                        Description = $"{(string.IsNullOrEmpty(p.BaseMode) || p.BaseMode == "None" ? p.Mode : p.BaseMode)} path H{p.PathFromHouse} → H{p.PathToHouse}."
                    });
                }

                if (p.MadeThroughCompany || string.Equals(p.Mode, "Company", StringComparison.OrdinalIgnoreCase))
                {
                    var companionHint = p.AspectFromHouse > 0 ? p.AspectFromHouse
                        : (p.PathFromHouse > 0 ? p.PathFromHouse : p.PathToHouse);
                    TryAddCompanyPair(links, querent, quesited, companionHint, p.CompanyType);
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
            }

            return set;
        }

        public static bool HasDrawableGeometry(ChartRelationshipOverlay? overlay) =>
            overlay != null && (overlay.Links.Count > 0
                || (overlay.QuerentHouse is >= 1 and <= 12)
                || (overlay.QuesitedHouse is >= 1 and <= 12));

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
            int companionHint,
            string? companyType)
        {
            int? pairOf = null;
            if (companionHint == PairedHouse(querent) && querent > 0)
                pairOf = querent;
            else if (companionHint == PairedHouse(quesited) && quesited > 0)
                pairOf = quesited;
            else if (companionHint == querent && querent > 0)
                pairOf = querent;
            else if (companionHint == quesited && quesited > 0)
                pairOf = quesited;

            if (pairOf == null) return;
            var companion = PairedHouse(pairOf.Value);
            if (companion <= 0 || companion == pairOf.Value) return;
            if (links.Any(l => l.Kind == "company-pair"
                && ((l.FromHouse == pairOf && l.ToHouse == companion)
                    || (l.FromHouse == companion && l.ToHouse == pairOf))))
                return;

            var shortCo = PerfectionDetailCopy.FormatCompanyShort(companyType ?? string.Empty);
            var label = string.IsNullOrEmpty(shortCo)
                ? $"H{pairOf}↔H{companion}"
                : $"H{pairOf}↔H{companion} · {shortCo}";

            links.Add(new ChartAspectLink
            {
                FromHouse = pairOf.Value,
                ToHouse = companion,
                Kind = "company-pair",
                Label = label,
                Description = PerfectionDetailCopy.CompanyHoverText(
                    $"{PerfectionDetailCopy.FormatCompanyType(companyType ?? string.Empty)} — odd–even paired houses only (not 2–3 or 10–11).",
                    string.Empty,
                    pairOf.Value,
                    companion)
            });
        }

        private static int PairedHouse(int house)
        {
            if (house < 1 || house > 12) return 0;
            return house % 2 == 1 ? house + 1 : house - 1;
        }
    }
}
