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

        /// <summary>Equal-third boundaries along each inner-square edge (length 500).</summary>
        private const double Third0 = InnerMin;                    // 250
        private const double Third1 = InnerMin + 500.0 / 3.0;      // ~416.67
        private const double Third2 = InnerMin + 1000.0 / 3.0;     // ~583.33
        private const double Third3 = InnerMax;                    // 750

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
        /// Polygon for the house cell: the third of the inner-square rim facing the house,
        /// opened out to the chart's outer diamond / square so the fill matches the figure's triangle.
        /// </summary>
        public static bool TryGetHouseFillPolygon(int house, out IReadOnlyList<Point> points)
        {
            points = Array.Empty<Point>();
            if (house < 1 || house > 12 || !Anchors.TryGetValue(house, out var fig))
                return false;

            if (!TryGetRimSegment(house, out var rimA, out var rimB))
                return false;

            // Mid-side angular houses: tip at the diamond cardinal point for a clean triangle.
            if (house is 1 or 4 or 7 or 10)
            {
                var tip = house switch
                {
                    10 => new Point(CenterX, 0),
                    4 => new Point(CenterX, 1000),
                    1 => new Point(0, CenterY),
                    _ => new Point(1000, CenterY)
                };
                points = new[] { rimA, rimB, tip };
                return true;
            }

            // Cadent / succedent: trapezoid — rim segment + both endpoints projected to the outer envelope.
            var outerA = ProjectOntoOuterEnvelope(rimA);
            var outerB = ProjectOntoOuterEnvelope(rimB);
            // If the figure sits past the diamond (corner pocket), include its outer tip.
            var figOuter = ProjectOntoOuterEnvelope(fig);
            if (DistanceSq(figOuter, outerA) > 400 && DistanceSq(figOuter, outerB) > 400)
                points = new[] { rimA, rimB, outerB, figOuter, outerA };
            else
                points = new[] { rimA, rimB, outerB, outerA };
            return true;
        }

        /// <summary>Legacy 3-point helper for callers that only need a triangle approximation.</summary>
        public static bool TryGetHouseFillPoints(int house, out Point left, out Point right, out Point outer)
        {
            left = right = outer = default;
            if (!TryGetHouseFillPolygon(house, out var pts) || pts.Count < 3)
                return false;
            left = pts[0];
            right = pts[1];
            outer = pts[pts.Count - 1];
            return true;
        }

        /// <summary>
        /// Inner-square rim segment for a house (exact thirds on the rim stroke).
        /// Top L→R: 11,10,9 · Right T→B: 8,7,6 · Bottom R→L: 5,4,3 · Left B→T: 2,1,12.
        /// </summary>
        private static bool TryGetRimSegment(int house, out Point a, out Point b)
        {
            a = b = default;
            switch (house)
            {
                case 11: a = new(Third0, InnerMin); b = new(Third1, InnerMin); return true;
                case 10: a = new(Third1, InnerMin); b = new(Third2, InnerMin); return true;
                case 9: a = new(Third2, InnerMin); b = new(Third3, InnerMin); return true;
                case 8: a = new(InnerMax, Third0); b = new(InnerMax, Third1); return true;
                case 7: a = new(InnerMax, Third1); b = new(InnerMax, Third2); return true;
                case 6: a = new(InnerMax, Third2); b = new(InnerMax, Third3); return true;
                case 5: a = new(Third3, InnerMax); b = new(Third2, InnerMax); return true;
                case 4: a = new(Third2, InnerMax); b = new(Third1, InnerMax); return true;
                case 3: a = new(Third1, InnerMax); b = new(Third0, InnerMax); return true;
                case 2: a = new(InnerMin, Third3); b = new(InnerMin, Third2); return true;
                case 1: a = new(InnerMin, Third2); b = new(InnerMin, Third1); return true;
                case 12: a = new(InnerMin, Third1); b = new(InnerMin, Third0); return true;
                default: return false;
            }
        }

        private static double DistanceSq(Point p, Point q)
        {
            var dx = p.X - q.X;
            var dy = p.Y - q.Y;
            return dx * dx + dy * dy;
        }

        /// <summary>
        /// Ray from chart center through <paramref name="through"/> onto the outer envelope
        /// (farther of diamond |dx|+|dy|=500 and outer square L∞=500).
        /// </summary>
        private static Point ProjectOntoOuterEnvelope(Point through)
        {
            var dx = through.X - CenterX;
            var dy = through.Y - CenterY;
            if (Math.Abs(dx) < 1e-6 && Math.Abs(dy) < 1e-6)
                return new Point(CenterX, 0);

            var l1 = Math.Abs(dx) + Math.Abs(dy);
            var diamond = new Point(CenterX + dx * (500.0 / l1), CenterY + dy * (500.0 / l1));

            var linf = Math.Max(Math.Abs(dx), Math.Abs(dy));
            var square = new Point(CenterX + dx * (500.0 / linf), CenterY + dy * (500.0 / linf));

            // Prefer the farther hit so corner houses fill out to the outer square.
            var eD = (diamond.X - CenterX) * (diamond.X - CenterX) + (diamond.Y - CenterY) * (diamond.Y - CenterY);
            var eS = (square.X - CenterX) * (square.X - CenterX) + (square.Y - CenterY) * (square.Y - CenterY);
            return eS >= eD ? square : diamond;
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
