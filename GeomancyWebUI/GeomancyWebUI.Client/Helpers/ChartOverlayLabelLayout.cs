using System;
using System.Collections.Generic;
using GeomancyWebUI.Client.Models;

namespace GeomancyWebUI.Client.Helpers
{
    /// <summary>
    /// Places house-chart connector badges so stacked links (e.g. Occupation + Company
    /// on the same house pair) do not sit on top of each other.
    /// </summary>
    public static class ChartOverlayLabelLayout
    {
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
            // Same unordered house-pair → alternate label seats along the arc.
            var pairSlot = new Dictionary<long, int>();

            foreach (var link in links)
            {
                if (!HouseChartGeometry.TryGetNumberAnchor(link.FromHouse, out var from)
                    || !HouseChartGeometry.TryGetNumberAnchor(link.ToHouse, out var to))
                    continue;

                var isCompany = link.Kind is "company-pair" or "company-pass";
                var pull = isCompany ? 0.10 : 0.34;
                var iconInner = ChartLinkIconGlyphs.TryGetInnerMarkup(link.IconKind, link.IconVariant);
                var hasIcon = !string.IsNullOrEmpty(iconInner);
                var (w, h) = EstimateSize(link, hasIcon);

                var pairKey = PairKey(link.FromHouse, link.ToHouse);
                pairSlot.TryGetValue(pairKey, out var slotIndex);
                pairSlot[pairKey] = slotIndex + 1;

                // Spread labels along the arc when several links share endpoints.
                var t = slotIndex switch
                {
                    0 => 0.50,
                    1 => 0.32,
                    2 => 0.68,
                    3 => 0.22,
                    _ => 0.50 + ((slotIndex % 2 == 0) ? 0.12 : -0.12)
                };

                var inward = isCompany ? 0.04 : 0.12;
                var basePoint = HouseChartGeometry.PointOnArc(from, to, t, pull);
                var labelAt = new HouseChartGeometry.Point(
                    basePoint.X + (500 - basePoint.X) * inward,
                    basePoint.Y + (500 - basePoint.Y) * inward);

                // Fan perpendicular to the chord so badges stack clear of each other.
                var dx = to.X - from.X;
                var dy = to.Y - from.Y;
                var len = Math.Sqrt(dx * dx + dy * dy);
                if (len < 1) len = 1;
                var nx = -dy / len;
                var ny = dx / len;
                // Prefer outward from chart center so labels don't dive into house fills.
                var awayX = labelAt.X - 500;
                var awayY = labelAt.Y - 500;
                if (nx * awayX + ny * awayY < 0)
                {
                    nx = -nx;
                    ny = -ny;
                }

                labelAt = ResolveCollision(labelAt, w, h, nx, ny, occupied);
                occupied.Add((labelAt.X, labelAt.Y, w, h));

                results.Add(new PlacedBadge(
                    link,
                    string.Empty, // caller fills CSS
                    pull,
                    HouseChartGeometry.ArcPath(from, to, pull),
                    from,
                    to,
                    labelAt,
                    w,
                    h,
                    hasIcon,
                    iconInner));
            }

            return results;
        }

        private static HouseChartGeometry.Point ResolveCollision(
            HouseChartGeometry.Point seed,
            double w,
            double h,
            double nx,
            double ny,
            List<(double X, double Y, double W, double H)> occupied)
        {
            const double pad = 6;
            var x = seed.X;
            var y = seed.Y;
            for (var attempt = 0; attempt < 10; attempt++)
            {
                if (!Overlaps(x, y, w, h, pad, occupied))
                    return new HouseChartGeometry.Point(x, y);

                var step = 20 + attempt * 8;
                var side = attempt % 2 == 0 ? 1 : -1;
                x = seed.X + nx * step * side;
                y = seed.Y + ny * step * side;
                // Slight along-normal drift so we don't ping-pong on the same spot.
                x += ny * (attempt * 3);
                y -= nx * (attempt * 3);
            }

            return new HouseChartGeometry.Point(x, y);
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
