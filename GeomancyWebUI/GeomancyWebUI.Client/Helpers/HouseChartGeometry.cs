using System.Collections.Generic;

namespace GeomancyWebUI.Client.Helpers
{
    /// <summary>
    /// Fixed SVG anchors for the traditional square house chart in ChartSurface
    /// (viewBox 0 0 1000 1000). Coordinates match the foreignObject placements.
    /// </summary>
    public static class HouseChartGeometry
    {
        public readonly record struct Point(double X, double Y);

        private const double CenterX = 500;
        private const double CenterY = 500;
        /// <summary>Half-side of the inner square (250..750).</summary>
        private const double InnerHalf = 250;

        private static readonly IReadOnlyDictionary<int, Point> Anchors = new Dictionary<int, Point>
        {
            [1] = new(150, 550),
            [2] = new(80, 814),
            [3] = new(250, 935),
            [4] = new(500, 900),
            [5] = new(750, 935),
            [6] = new(920, 814),
            [7] = new(850, 550),
            [8] = new(920, 286),
            [9] = new(750, 150),
            [10] = new(500, 200),
            [11] = new(250, 150),
            [12] = new(100, 300),
        };

        /// <summary>
        /// Points on the inner-square rim facing each house — used for house numbers
        /// and relationship overlay endpoints (so arcs avoid the figures).
        /// </summary>
        private static readonly IReadOnlyDictionary<int, Point> LinkAnchors = BuildLinkAnchors();

        public static bool TryGetAnchor(int house, out Point point)
        {
            if (house >= 1 && house <= 12 && Anchors.TryGetValue(house, out point))
                return true;
            point = default;
            return false;
        }

        public static bool TryGetLinkAnchor(int house, out Point point)
        {
            if (house >= 1 && house <= 12 && LinkAnchors.TryGetValue(house, out point))
                return true;
            point = default;
            return false;
        }

        private static IReadOnlyDictionary<int, Point> BuildLinkAnchors()
        {
            var map = new Dictionary<int, Point>(12);
            foreach (var (house, fig) in Anchors)
                map[house] = ProjectOntoInnerSquare(fig);
            return map;
        }

        /// <summary>
        /// Ray from chart center through the figure hits the inner square;
        /// nudge slightly inward so numbers sit just inside the rim.
        /// </summary>
        private static Point ProjectOntoInnerSquare(Point figure)
        {
            var dx = figure.X - CenterX;
            var dy = figure.Y - CenterY;
            var scale = Math.Max(Math.Abs(dx), Math.Abs(dy));
            if (scale < 1e-6)
                return new Point(CenterX, CenterY - InnerHalf);

            var t = InnerHalf / scale;
            var hitX = CenterX + t * dx;
            var hitY = CenterY + t * dy;

            const double inset = 0.08;
            return new Point(
                hitX + (CenterX - hitX) * inset,
                hitY + (CenterY - hitY) * inset);
        }

        /// <summary>
        /// Quadratic control point pulled toward chart center so links read as chart arcs
        /// rather than chords through figure cards.
        /// </summary>
        public static Point ArcControl(Point from, Point to, double pull = 0.42)
        {
            var mx = (from.X + to.X) / 2.0;
            var my = (from.Y + to.Y) / 2.0;
            return new Point(
                mx + (CenterX - mx) * pull,
                my + (CenterY - my) * pull);
        }

        public static string ArcPath(Point from, Point to, double pull = 0.42)
        {
            var c = ArcControl(from, to, pull);
            return $"M {from.X:0.##},{from.Y:0.##} Q {c.X:0.##},{c.Y:0.##} {to.X:0.##},{to.Y:0.##}";
        }

        /// <summary>
        /// Point on the quadratic arc at parameter t (0..1), used for label / tick placement.
        /// </summary>
        public static Point PointOnArc(Point from, Point to, double t, double pull = 0.42)
        {
            var c = ArcControl(from, to, pull);
            var u = 1.0 - t;
            return new Point(
                u * u * from.X + 2 * u * t * c.X + t * t * to.X,
                u * u * from.Y + 2 * u * t * c.Y + t * t * to.Y);
        }

        /// <summary>
        /// Label seat nudged slightly toward chart center from the arc midpoint.
        /// </summary>
        public static Point LabelPoint(Point from, Point to, double pull = 0.42, double inward = 0.12)
        {
            var mid = PointOnArc(from, to, 0.5, pull);
            return new Point(
                mid.X + (CenterX - mid.X) * inward,
                mid.Y + (CenterY - mid.Y) * inward);
        }
    }
}
