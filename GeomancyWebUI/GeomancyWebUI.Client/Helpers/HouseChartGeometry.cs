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
        /// Connection endpoints on the inner-square rim (arcs hit here, away from figures).
        /// </summary>
        private static readonly IReadOnlyDictionary<int, Point> LinkAnchors =
            BuildProjectedAnchors(insetTowardCenter: 0.02);

        /// <summary>
        /// House numbers sit further inward so arc strokes don't cover the digits.
        /// </summary>
        private static readonly IReadOnlyDictionary<int, Point> NumberAnchors =
            BuildProjectedAnchors(insetTowardCenter: 0.22);

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

        public static bool TryGetNumberAnchor(int house, out Point point)
        {
            if (house >= 1 && house <= 12 && NumberAnchors.TryGetValue(house, out point))
                return true;
            point = default;
            return false;
        }

        /// <summary>
        /// Soft fill triangle for a house: inner-rim span + outer apex on the diamond.
        /// </summary>
        public static bool TryGetHouseFillPoints(int house, out Point left, out Point right, out Point outer)
        {
            left = right = outer = default;
            if (house < 1 || house > 12) return false;
            if (!LinkAnchors.TryGetValue(house, out var mid)
                || !LinkAnchors.TryGetValue(PrevHouse(house), out var prev)
                || !LinkAnchors.TryGetValue(NextHouse(house), out var next)
                || !Anchors.TryGetValue(house, out var fig))
                return false;

            left = Midpoint(prev, mid);
            right = Midpoint(mid, next);
            outer = ProjectOntoDiamond(fig);
            // Pull apex slightly inward so the stroke of the diamond stays visible.
            outer = new Point(
                outer.X + (CenterX - outer.X) * 0.04,
                outer.Y + (CenterY - outer.Y) * 0.04);
            return true;
        }

        /// <summary>
        /// Seat for a Querent/Quesited tag, between the rim number and the figure.
        /// </summary>
        public static bool TryGetRoleTagPoint(int house, out Point point)
        {
            point = default;
            if (!TryGetLinkAnchor(house, out var link) || !TryGetAnchor(house, out var fig))
                return false;
            point = new Point(
                link.X + (fig.X - link.X) * 0.32,
                link.Y + (fig.Y - link.Y) * 0.32);
            return true;
        }

        private static int PrevHouse(int house) => house == 1 ? 12 : house - 1;
        private static int NextHouse(int house) => house == 12 ? 1 : house + 1;

        private static Point Midpoint(Point a, Point b) =>
            new((a.X + b.X) / 2.0, (a.Y + b.Y) / 2.0);

        private static IReadOnlyDictionary<int, Point> BuildProjectedAnchors(double insetTowardCenter)
        {
            var map = new Dictionary<int, Point>(12);
            foreach (var (house, fig) in Anchors)
                map[house] = ProjectOntoInnerSquare(fig, insetTowardCenter);
            return map;
        }

        /// <summary>
        /// Ray from chart center through the figure hits the inner square;
        /// <paramref name="insetTowardCenter"/> pulls the point inward from the rim.
        /// </summary>
        private static Point ProjectOntoInnerSquare(Point figure, double insetTowardCenter)
        {
            var dx = figure.X - CenterX;
            var dy = figure.Y - CenterY;
            var scale = Math.Max(Math.Abs(dx), Math.Abs(dy));
            if (scale < 1e-6)
                return new Point(CenterX, CenterY - InnerHalf);

            var t = InnerHalf / scale;
            var hitX = CenterX + t * dx;
            var hitY = CenterY + t * dy;

            return new Point(
                hitX + (CenterX - hitX) * insetTowardCenter,
                hitY + (CenterY - hitY) * insetTowardCenter);
        }

        /// <summary>Ray from center through the figure onto the outer diamond (|dx|+|dy|=500).</summary>
        private static Point ProjectOntoDiamond(Point figure)
        {
            var dx = figure.X - CenterX;
            var dy = figure.Y - CenterY;
            var scale = Math.Abs(dx) + Math.Abs(dy);
            if (scale < 1e-6)
                return new Point(CenterX, CenterY - 500);

            var t = 500.0 / scale;
            return new Point(CenterX + t * dx, CenterY + t * dy);
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
