using System;
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
        private const double InnerMin = CenterX - InnerHalf; // 250
        private const double InnerMax = CenterX + InnerHalf; // 750
        /// <summary>Centers of the three equal edge thirds (symmetric house seats).</summary>
        private static readonly double EdgeA = InnerMin + InnerHalf * 2.0 / 6.0; // ~333.33
        private static readonly double EdgeB = CenterX;                          // 500
        private static readonly double EdgeC = InnerMax - InnerHalf * 2.0 / 6.0; // ~666.67

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
        /// Symmetric seats for house numbers and overlay endpoints (padded in from the rim).
        /// </summary>
        private static readonly IReadOnlyDictionary<int, Point> NumberAnchors =
            BuildSymmetricRimAnchors(padFromRim: 44);

        public static bool TryGetAnchor(int house, out Point point)
        {
            if (house >= 1 && house <= 12 && Anchors.TryGetValue(house, out point))
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
        /// Seat for a Querent/Quesited tag, between the house number and the figure.
        /// </summary>
        public static bool TryGetRoleTagPoint(int house, out Point point)
        {
            point = default;
            if (!TryGetNumberAnchor(house, out var num) || !TryGetAnchor(house, out var fig))
                return false;
            point = new Point(
                num.X + (fig.X - num.X) * 0.38,
                num.Y + (fig.Y - num.Y) * 0.38);
            return true;
        }

        /// <summary>
        /// House-cell triangle matching the chart grid: diamond lobes for 1/4/7/10,
        /// and the outer-corner pockets (split by the corner rays) for the other eight.
        /// </summary>
        public static bool TryGetHouseFillPolygon(int house, out IReadOnlyList<Point> points)
        {
            points = Array.Empty<Point>();
            if (house < 1 || house > 12)
                return false;

            // Chart lines: inner square 250..750, diamond tips at mid-sides, corner rays
            // to outer corners. Cardinals own a full diamond lobe; the other houses own
            // one half of an outside-diamond corner pocket.
            points = house switch
            {
                10 => new[] { new Point(InnerMin, InnerMin), new Point(InnerMax, InnerMin), new Point(CenterX, 0) },
                7 => new[] { new Point(InnerMax, InnerMin), new Point(InnerMax, InnerMax), new Point(1000, CenterY) },
                4 => new[] { new Point(InnerMin, InnerMax), new Point(InnerMax, InnerMax), new Point(CenterX, 1000) },
                1 => new[] { new Point(InnerMin, InnerMin), new Point(InnerMin, InnerMax), new Point(0, CenterY) },
                11 => new[] { new Point(CenterX, 0), new Point(0, 0), new Point(InnerMin, InnerMin) },
                12 => new[] { new Point(0, CenterY), new Point(0, 0), new Point(InnerMin, InnerMin) },
                9 => new[] { new Point(CenterX, 0), new Point(1000, 0), new Point(InnerMax, InnerMin) },
                8 => new[] { new Point(1000, 0), new Point(1000, CenterY), new Point(InnerMax, InnerMin) },
                6 => new[] { new Point(1000, CenterY), new Point(1000, 1000), new Point(InnerMax, InnerMax) },
                5 => new[] { new Point(1000, 1000), new Point(CenterX, 1000), new Point(InnerMax, InnerMax) },
                3 => new[] { new Point(CenterX, 1000), new Point(0, 1000), new Point(InnerMin, InnerMax) },
                2 => new[] { new Point(0, 1000), new Point(0, CenterY), new Point(InnerMin, InnerMax) },
                _ => Array.Empty<Point>()
            };
            return points.Count >= 3;
        }

        /// <summary>Legacy 3-point helper for callers that only need a triangle approximation.</summary>
        public static bool TryGetHouseFillPoints(int house, out Point left, out Point right, out Point outer)
        {
            left = right = outer = default;
            if (!TryGetHouseFillPolygon(house, out var pts) || pts.Count < 3)
                return false;
            left = pts[0];
            right = pts[1];
            outer = pts[2];
            return true;
        }

        /// <summary>
        /// Three equal seats per side of the inner square, padded inward along the edge normal.
        /// Top L→R: 11,10,9 · Right T→B: 8,7,6 · Bottom R→L: 5,4,3 · Left B→T: 2,1,12.
        /// </summary>
        private static IReadOnlyDictionary<int, Point> BuildSymmetricRimAnchors(double padFromRim)
        {
            var top = InnerMin + padFromRim;
            var right = InnerMax - padFromRim;
            var bottom = InnerMax - padFromRim;
            var left = InnerMin + padFromRim;

            return new Dictionary<int, Point>
            {
                [11] = new(EdgeA, top),
                [10] = new(EdgeB, top),
                [9] = new(EdgeC, top),
                [8] = new(right, EdgeA),
                [7] = new(right, EdgeB),
                [6] = new(right, EdgeC),
                [5] = new(EdgeC, bottom),
                [4] = new(EdgeB, bottom),
                [3] = new(EdgeA, bottom),
                [2] = new(left, EdgeC),
                [1] = new(left, EdgeB),
                [12] = new(left, EdgeA),
            };
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
