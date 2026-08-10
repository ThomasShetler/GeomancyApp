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
        /// Soft fill triangle for a house: rim span between neighbors + outer apex on the diamond.
        /// </summary>
        public static bool TryGetHouseFillPoints(int house, out Point left, out Point right, out Point outer)
        {
            left = right = outer = default;
            if (house < 1 || house > 12) return false;
            if (!NumberAnchors.TryGetValue(house, out var mid)
                || !NumberAnchors.TryGetValue(PrevHouse(house), out var prev)
                || !NumberAnchors.TryGetValue(NextHouse(house), out var next)
                || !Anchors.TryGetValue(house, out var fig))
                return false;

            left = Midpoint(prev, mid);
            right = Midpoint(mid, next);
            outer = ProjectOntoDiamond(fig);
            outer = new Point(
                outer.X + (CenterX - outer.X) * 0.04,
                outer.Y + (CenterY - outer.Y) * 0.04);
            return true;
        }

        private static int PrevHouse(int house) => house == 1 ? 12 : house - 1;
        private static int NextHouse(int house) => house == 12 ? 1 : house + 1;

        private static Point Midpoint(Point a, Point b) =>
            new((a.X + b.X) / 2.0, (a.Y + b.Y) / 2.0);

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
