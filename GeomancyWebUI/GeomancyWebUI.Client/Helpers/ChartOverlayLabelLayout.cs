using System;
using System.Collections.Generic;
using System.Linq;
using GeomancyWebUI.Client.Models;

namespace GeomancyWebUI.Client.Helpers
{
    /// <summary>
    /// Places house-chart connector badges so stacked links (e.g. Qrt + Co. Demi)
    /// do not sit on top of each other. Long labels prefer the outer side of the arc
    /// (below the bow when the top is crowded).
    /// </summary>
    public static class ChartOverlayLabelLayout
    {
        private const double ChartCenterX = 500;
        private const double ChartCenterY = 500;
        private const int LongLabelChars = 12;

        public readonly record struct PlacedBadge(
            ChartAspectLink Link,
            string CssClass,
            double Pull,
            string Path,
            HouseChartGeometry.Point From,
            HouseChartGeometry.Point To,
            HouseChartGeometry.Point LabelAt,
            double BadgeWidth,
            double BadgeHeight,
            bool HasIcon,
            string? IconInner);

        public static IReadOnlyList<PlacedBadge> Place(IReadOnlyList<ChartAspectLink> links)
        {
            var results = new List<PlacedBadge>(links.Count);
            var occupied = new List<(double X, double Y, double W, double H)>();
            var pairSlot = new Dictionary<long, int>();

            // Short path labels first so long company badges can fall below them.
            foreach (var link in links.OrderBy(Priority))
            {
                if (!HouseChartGeometry.TryGetNumberAnchor(link.FromHouse, out var from)
                    || !HouseChartGeometry.TryGetNumberAnchor(link.ToHouse, out var to))
                    continue;

                var isCompany = link.Kind is "company-pair" or "company-pass";
                var isLong = IsLongLabel(link);
                var pull = isCompany ? 0.10 : 0.34;
                var iconInner = ChartLinkIconGlyphs.TryGetInnerMarkup(link.IconKind, link.IconVariant);
                var hasIcon = !string.IsNullOrEmpty(iconInner);
                var (w, h) = EstimateSize(link, hasIcon);

                var pairKey = PairKey(link.FromHouse, link.ToHouse);
                pairSlot.TryGetValue(pairKey, out var slotIndex);
                pairSlot[pairKey] = slotIndex + 1;

                var t = slotIndex switch
                {
                    0 => 0.50,
                    1 => 0.36,
                    2 => 0.64,
                    3 => 0.28,
                    _ => 0.50 + ((slotIndex % 2 == 0) ? 0.10 : -0.10)
                };

                var mid = HouseChartGeometry.PointOnArc(from, to, t, pull);
                var (ox, oy) = OuterUnit(mid);

                // Long / company badges sit clearly outside the arc bow (often "below"
                // on bottom-edge houses); short labels stay nearer the midpoint.
                var standOff = isLong ? 34 : (isCompany ? 22 : 12);
                if (slotIndex > 0 && isLong)
                    standOff += 10 * slotIndex;

                var seed = new HouseChartGeometry.Point(
                    mid.X + ox * standOff,
                    mid.Y + oy * standOff);

                // Prefer further along the outer side (and screen-down when that helps)
                // before flipping to the inner/top side of the arc.
                seed = ResolveCollision(seed, w, h, ox, oy, preferOuter: isLong || isCompany, occupied);
                occupied.Add((seed.X, seed.Y, w, h));

                results.Add(new PlacedBadge(
                    link,
                    string.Empty,
                    pull,
                    HouseChartGeometry.ArcPath(from, to, pull),
                    from,
                    to,
                    seed,
                    w,
                    h,
                    hasIcon,
                    iconInner));
            }

            return results;
        }

        private static int Priority(ChartAspectLink link)
        {
            if (IsLongLabel(link) || link.Kind is "company-pair" or "company-pass")
                return 2;
            if (string.Equals(link.Kind, "path", StringComparison.OrdinalIgnoreCase))
                return 0;
            return 1;
        }

        private static bool IsLongLabel(ChartAspectLink link) =>
            (link.Label?.Length ?? 0) >= LongLabelChars
            || link.Kind is "company-pair" or "company-pass";

        /// <summary>Unit vector from arc midpoint away from chart center (outer rim).</summary>
        private static (double X, double Y) OuterUnit(HouseChartGeometry.Point mid)
        {
            var dx = mid.X - ChartCenterX;
            var dy = mid.Y - ChartCenterY;
            var len = Math.Sqrt(dx * dx + dy * dy);
            if (len < 1)
                return (0, 1); // fallback: screen-down
            return (dx / len, dy / len);
        }

        private static HouseChartGeometry.Point ResolveCollision(
            HouseChartGeometry.Point seed,
            double w,
            double h,
            double ox,
            double oy,
            bool preferOuter,
            List<(double X, double Y, double W, double H)> occupied)
        {
            const double pad = 8;
            if (!Overlaps(seed.X, seed.Y, w, h, pad, occupied))
                return seed;

            // Candidate offsets: push further outside first for long labels, then
            // screen-down, then lateral, then (last) flip to the inner/top side.
            var candidates = preferOuter
                ? BuildOuterFirstOffsets(seed, ox, oy)
                : BuildAlternatingOffsets(seed, ox, oy);

            foreach (var (x, y) in candidates)
            {
                if (!Overlaps(x, y, w, h, pad, occupied))
                    return new HouseChartGeometry.Point(x, y);
            }

            // Last resort: keep going further outside.
            return new HouseChartGeometry.Point(
                seed.X + ox * 72,
                seed.Y + oy * 72 + 24);
        }

        private static List<(double X, double Y)> BuildOuterFirstOffsets(
            HouseChartGeometry.Point seed,
            double ox,
            double oy)
        {
            var list = new List<(double X, double Y)>(16);
            // Further outside the arc
            for (var i = 1; i <= 6; i++)
            {
                var step = 18 * i;
                list.Add((seed.X + ox * step, seed.Y + oy * step));
                // Bias screen-down so long badges clear Qrt sitting above
                list.Add((seed.X + ox * step, seed.Y + oy * step + 14 * i));
                list.Add((seed.X + ox * (step * 0.6) + oy * 16, seed.Y + oy * (step * 0.6) - ox * 16));
                list.Add((seed.X + ox * (step * 0.6) - oy * 16, seed.Y + oy * (step * 0.6) + ox * 16));
            }

            // Inner/top side only after outer options are exhausted
            for (var i = 1; i <= 3; i++)
            {
                var step = 20 * i;
                list.Add((seed.X - ox * step, seed.Y - oy * step));
            }

            return list;
        }

        private static List<(double X, double Y)> BuildAlternatingOffsets(
            HouseChartGeometry.Point seed,
            double ox,
            double oy)
        {
            var list = new List<(double X, double Y)>(12);
            for (var attempt = 1; attempt <= 8; attempt++)
            {
                var step = 16 + attempt * 8;
                var side = attempt % 2 == 0 ? 1 : -1;
                list.Add((seed.X + ox * step * side, seed.Y + oy * step * side));
                list.Add((seed.X + oy * step * side, seed.Y - ox * step * side));
            }

            return list;
        }

        private static bool Overlaps(
            double x,
            double y,
            double w,
            double h,
            double pad,
            List<(double X, double Y, double W, double H)> occupied)
        {
            var left = x - w / 2 - pad;
            var right = x + w / 2 + pad;
            var top = y - h / 2 - pad;
            var bottom = y + h / 2 + pad;

            foreach (var o in occupied)
            {
                var ol = o.X - o.W / 2;
                var orr = o.X + o.W / 2;
                var ot = o.Y - o.H / 2;
                var ob = o.Y + o.H / 2;
                if (left < orr && right > ol && top < ob && bottom > ot)
                    return true;
            }

            return false;
        }

        private static (double W, double H) EstimateSize(ChartAspectLink link, bool hasIcon)
        {
            const double height = 22;
            if (hasIcon && !string.IsNullOrEmpty(link.Label))
            {
                var textW = Math.Max(28, link.Label!.Length * 7.2);
                return (18 + textW + 8, height);
            }

            if (hasIcon)
                return (24, height);

            if (!string.IsNullOrEmpty(link.Label))
                return (Math.Max(36, link.Label!.Length * 7.5), height);

            return (24, height);
        }

        private static long PairKey(int a, int b)
        {
            var lo = Math.Min(a, b);
            var hi = Math.Max(a, b);
            return ((long)lo << 8) | (uint)hi;
        }
    }
}
