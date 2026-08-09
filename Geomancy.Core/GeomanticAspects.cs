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

        /// <summary>
        /// Aspect type plus dexter/sinister direction from forward distance on the 12-house wheel.
        /// Distance 2/3/4 forward = Sinister; 10/9/8 backward = Dexter; 6 = Opposition.
        /// </summary>
        public static (AspectType aspect, string direction) GetAspectWithDirection(int fromHouse, int toHouse)
        {
            if (fromHouse == toHouse)
                return (AspectType.Conjunction, "Conjunction");

            int distance = (toHouse - fromHouse + 12) % 12;

            return distance switch
            {
                2 => (AspectType.Sextile, "Sinister"),
                10 => (AspectType.Sextile, "Dexter"),
                3 => (AspectType.Square, "Sinister"),
                9 => (AspectType.Square, "Dexter"),
                4 => (AspectType.Trine, "Sinister"),
                8 => (AspectType.Trine, "Dexter"),
                6 => (AspectType.Opposition, "Opposition"),
                _ => (AspectType.None, "None")
            };
        }

        /// <summary>
        /// Greer "houses between" count (not wheel steps): Sextile 1, Square 2, Trine 3, Opposition 5.
        /// </summary>
        public static int HousesBetween(AspectType aspect) => aspect switch
        {
            AspectType.Sextile => 1,
            AspectType.Square => 2,
            AspectType.Trine => 3,
            AspectType.Opposition => 5,
            _ => 0
        };

        public static int HousesBetween(string aspectType)
        {
            if (Enum.TryParse<AspectType>(aspectType?.Trim(), true, out var a))
                return HousesBetween(a);
            return 0;
        }

        /// <summary>
        /// Houses strictly between cast endpoints along the aspect's cast direction
        /// (sinister = forward order; dexter = backward; opposition = forward arc of 6).
        /// </summary>
        public static IReadOnlyList<int> IntermediateHouses(int fromHouse, int toHouse)
        {
            if (fromHouse < 1 || fromHouse > 12 || toHouse < 1 || toHouse > 12 || fromHouse == toHouse)
                return Array.Empty<int>();

            var (_, direction) = GetAspectWithDirection(fromHouse, toHouse);
            bool forward = direction.IndexOf("Sinister", StringComparison.OrdinalIgnoreCase) >= 0
                || direction.Equals("Opposition", StringComparison.OrdinalIgnoreCase)
                || direction.Equals("Conjunction", StringComparison.OrdinalIgnoreCase);

            int step = forward ? 1 : -1;
            var list = new List<int>();
            int h = fromHouse;
            while (true)
            {
                h = ((h - 1 + step + 12) % 12) + 1;
                if (h == toHouse) break;
                list.Add(h);
                if (list.Count > 12) break;
            }
            return list;
        }

        /// <summary>Compact chart label: "Dex Sq", "Sin Tr", "Opp".</summary>
        public static string ShortLabel(AspectType aspect, string direction)
        {
            var asp = aspect switch
            {
                AspectType.Sextile => "Sx",
                AspectType.Square => "Sq",
                AspectType.Trine => "Tr",
                AspectType.Opposition => "Opp",
                AspectType.Conjunction => "Cj",
                _ => string.Empty
            };
            if (string.IsNullOrEmpty(asp)) return string.Empty;
            if (aspect == AspectType.Opposition || aspect == AspectType.Conjunction)
                return asp;

            if (!string.IsNullOrEmpty(direction))
            {
                if (direction.IndexOf("Dexter", StringComparison.OrdinalIgnoreCase) >= 0)
                    return "Dex " + asp;
                if (direction.IndexOf("Sinister", StringComparison.OrdinalIgnoreCase) >= 0)
                    return "Sin " + asp;
            }
            return asp;
        }

        public static string ShortLabel(string aspectType, string direction)
        {
            if (!Enum.TryParse<AspectType>(aspectType?.Trim(), true, out var a))
                return string.Empty;
            return ShortLabel(a, direction ?? string.Empty);
        }

        /// <summary>
        /// Greer-style reader sentence for an aspect cast.
        /// </summary>
        public static string DescribeAspect(int fromHouse, int toHouse, AspectType aspect, string direction)
        {
            if (aspect == AspectType.None || fromHouse <= 0 || toHouse <= 0)
                return string.Empty;

            if (aspect == AspectType.Conjunction)
                return $"Conjunction — the same house seat (H{fromHouse}).";

            int between = HousesBetween(aspect);
            string betweenWord = between switch
            {
                1 => "one house",
                2 => "two houses",
                3 => "three houses",
                5 => "five houses",
                _ => $"{between} houses"
            };

            if (aspect == AspectType.Opposition)
            {
                return $"Opposition — {betweenWord} between H{fromHouse} and H{toHouse} (opposite seats on the chart; not dexter or sinister).";
            }

            bool dexter = !string.IsNullOrEmpty(direction)
                && direction.IndexOf("Dexter", StringComparison.OrdinalIgnoreCase) >= 0;
            string dirPhrase = dexter
                ? "cast against house order (dexter — the stronger mode)"
                : "cast with house order (sinister)";

            string dirPrefix = dexter
                ? "Dexter "
                : (direction != null && direction.IndexOf("Sinister", StringComparison.OrdinalIgnoreCase) >= 0
                    ? "Sinister "
                    : string.Empty);
            return $"{dirPrefix}{aspect} — {betweenWord} between H{fromHouse} and H{toHouse} ({dirPhrase}).";
        }

        public static string DescribeAspect(int fromHouse, int toHouse, string aspectType, string direction)
        {
            if (!Enum.TryParse<AspectType>(aspectType?.Trim(), true, out var a))
                return string.Empty;
            return DescribeAspect(fromHouse, toHouse, a, direction ?? string.Empty);
        }

        /// <summary>Glossary line without specific house numbers (for headers).</summary>
        public static string GlossaryLine(AspectType aspect, string direction)
        {
            int between = HousesBetween(aspect);
            string betweenWord = between switch
            {
                1 => "one house between them",
                2 => "two houses between them",
                3 => "three houses between them",
                5 => "five houses between them (opposite seats)",
                _ => "a recognized house relationship"
            };

            if (aspect == AspectType.Opposition)
                return $"Opposition — {betweenWord}; not dexter or sinister. Greer treats this as an unfavorable aspect of confrontation or denial.";

            if (aspect == AspectType.None || aspect == AspectType.Conjunction)
                return string.Empty;

            bool dexter = !string.IsNullOrEmpty(direction)
                && direction.IndexOf("Dexter", StringComparison.OrdinalIgnoreCase) >= 0;
            bool sinister = !string.IsNullOrEmpty(direction)
                && direction.IndexOf("Sinister", StringComparison.OrdinalIgnoreCase) >= 0;

            string dirNote = dexter
                ? " Dexter casts against house order and is the stronger mode of perfection."
                : sinister
                    ? " Sinister casts with house order and tends to unfold more gradually."
                    : string.Empty;

            string quality = aspect == AspectType.Square
                ? "a challenging aspect of friction or tests"
                : aspect == AspectType.Sextile
                    ? "a mild favorable aspect"
                    : aspect == AspectType.Trine
                        ? "a strong favorable aspect of easy agreement"
                        : "a classical aspect";

            return $"{aspect} — {betweenWord}; {quality}.{dirNote}";
        }

        public static string GlossaryLine(string aspectType, string direction)
        {
            if (!Enum.TryParse<AspectType>(aspectType?.Trim(), true, out var a))
                return string.Empty;
            return GlossaryLine(a, direction ?? string.Empty);
        }

        /*  Enumerate every pair once (i < j) and yield aspects >= min  */
        /*  Only count aspects when the figures in the two houses are different  */
        public static IEnumerable<(int from, int to, AspectType aspect)>
            AllAspects(HouseChart chart, AspectType min = AspectType.Sextile)
        {
            for (int i = 1; i <= 12; i++)
            {
                for (int j = i + 1; j <= 12; j++)
                {
                    var asp = GetAspect(i, j);
                    if (asp != AspectType.None && (int)asp >= (int)min)
                    {
                        var figure1 = chart?.GetHouseFigure(i);
                        var figure2 = chart?.GetHouseFigure(j);

                        if (figure1 != null && figure2 != null)
                        {
                            if (!FigureNameHelper.Root(figure1.Name)
                                    .Equals(FigureNameHelper.Root(figure2.Name), StringComparison.OrdinalIgnoreCase))
                            {
                                yield return (i, j, asp);
                            }
                        }
                    }
                }
            }
        }
    }
}
