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

        public static bool TryGetAnchor(int house, out Point point)
        {
            if (house >= 1 && house <= 12 && Anchors.TryGetValue(house, out point))
                return true;
            point = default;
            return false;
        }

        /// <summary>
        /// Quadratic control point pulled toward chart center so links read as chart arcs
        /// rather than chords through figure cards.
        /// </summary>
        public static Point ArcControl(Point from, Point to, double pull = 0.28)
        {
            const double cx = 500;
            const double cy = 500;
            var mx = (from.X + to.X) / 2.0;
            var my = (from.Y + to.Y) / 2.0;
            return new Point(
                mx + (cx - mx) * pull,
                my + (cy - my) * pull);
        }

        public static string ArcPath(Point from, Point to, double pull = 0.28)
        {
            var c = ArcControl(from, to, pull);
            return $"M {from.X:0.##},{from.Y:0.##} Q {c.X:0.##},{c.Y:0.##} {to.X:0.##},{to.Y:0.##}";
        }
    }
}
