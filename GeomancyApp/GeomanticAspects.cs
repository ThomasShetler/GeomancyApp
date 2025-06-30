using System;
using System.Collections.Generic;

namespace GeomancyApp
{
    public enum AspectType
    {
        None = 0,
        Sextile = 1,
        Square = 2,
        Trine = 3,
        Opposition = 4,
        Conjunction = 5
    }

    public static class GeomanticAspects
    {
        /*  Dexter aspect table – Greer, Table 6-1  */
        private static readonly Dictionary<int, int[]> Dexter =
            new Dictionary<int, int[]>
        {
            { 1,  new[]{11,10, 9, 7, 5, 4, 3}}, { 2,  new[]{12,11,10, 8, 6, 5, 4}},
            { 3,  new[]{ 1,12,11, 9, 7, 6, 5}}, { 4,  new[]{ 2, 1,12,10, 8, 7, 6}},
            { 5,  new[]{ 3, 2, 1,11, 9, 8, 7}}, { 6,  new[]{ 4, 3, 2,12,10, 9, 8}},
            { 7,  new[]{ 5, 4, 3, 1,11,10, 9}}, { 8,  new[]{ 6, 5, 4, 2,12,11,10}},
            { 9,  new[]{ 7, 6, 5, 3, 1,12,11}}, {10,  new[]{ 8, 7, 6, 4, 2, 1,12}},
            {11,  new[]{ 9, 8, 7, 5, 3, 2, 1}}, {12,  new[]{10, 9, 8, 6, 4, 3, 2}}
        };

        public static AspectType GetAspect(int from, int to)
        {
            if (from == to) return AspectType.Conjunction;

            int[] row;
            if (!Dexter.TryGetValue(from, out row)) return AspectType.None;

            int idx = Array.IndexOf(row, to);
            if (idx == -1) return AspectType.None;

            switch (idx)
            {
                case 0:
                case 6: return AspectType.Sextile;

                case 1:
                case 5: return AspectType.Square;

                case 2:
                case 4: return AspectType.Trine;

                case 3: return AspectType.Opposition;

                default: return AspectType.None;
            }
        }

        /*  Enumerate every pair once (i < j) and yield aspects >= min  */
        public static IEnumerable<(int from, int to, AspectType aspect)>
            AllAspects(HouseChart chart, AspectType min = AspectType.Sextile)
        {
            for (int i = 1; i <= 12; i++)
            {
                for (int j = i + 1; j <= 12; j++)
                {
                    var asp = GetAspect(i, j);
                    if (asp != AspectType.None && (int)asp >= (int)min)
                        yield return (i, j, asp);
                }
            }
        }
    }
}
