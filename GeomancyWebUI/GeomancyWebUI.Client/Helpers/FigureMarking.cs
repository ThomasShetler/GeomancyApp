namespace GeomancyWebUI.Client.Helpers
{
    /// <summary>
    /// Classical geomantic dot-marking (stick-and-surface): four horizontal rows of dots;
    /// pair two at a time left-to-right in each row until 0, 1, or 2 remain.
    /// Odd remainder → single/active line (1); even → double/passive (2).
    /// </summary>
    public static class FigureMarking
    {
        public const int MinDotsPerRow = 3;

        /// <summary>Alias for <see cref="MinDotsPerRow"/>.</summary>
        public const int MinDotsPerColumn = MinDotsPerRow;

        /// <summary>
        /// Resolves a geomantic line value (1 or 2) from the number of dots marked in one horizontal row.
        /// </summary>
        public static int ResolveLineFromDotCount(int dotCount)
        {
            if (dotCount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(dotCount));
            }

            var remainder = dotCount;
            while (remainder >= 2)
            {
                remainder -= 2;
            }

            return remainder == 1 ? 1 : 2;
        }

        /// <summary>
        /// Returns ordered index pairs (left-to-right) for pairing animation within one row.
        /// Pairs are drawn until only one or two marks remain (classical stick-and-surface rule).
        /// Indices are 0-based from the left end of the row.
        /// </summary>
        public static IReadOnlyList<(int a, int b)> GetPairingSteps(int dotCount)
        {
            if (dotCount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(dotCount));
            }

            var steps = new List<(int, int)>();
            var paired = new bool[dotCount];

            while (true)
            {
                var unpaired = new List<int>();
                for (var i = 0; i < dotCount; i++)
                {
                    if (!paired[i])
                    {
                        unpaired.Add(i);
                    }
                }

                if (unpaired.Count <= 2)
                {
                    break;
                }

                steps.Add((unpaired[0], unpaired[1]));
                paired[unpaired[0]] = true;
                paired[unpaired[1]] = true;
            }

            return steps;
        }

        /// <summary>
        /// Pairing steps using mark indices sorted left-to-right by canvas X position.
        /// </summary>
        public static IReadOnlyList<(int a, int b)> GetPairingStepsByHorizontalOrder(
            IReadOnlyList<double> xPercents)
        {
            if (xPercents == null || xPercents.Count == 0)
            {
                return Array.Empty<(int, int)>();
            }

            var order = Enumerable.Range(0, xPercents.Count)
                .OrderBy(i => xPercents[i])
                .ThenBy(i => i)
                .ToArray();

            var steps = GetPairingSteps(xPercents.Count);
            return steps.Select(p => (order[p.a], order[p.b])).ToList();
        }

        public static bool AllRowsReady(IReadOnlyList<int> rowDotCounts, int minDots = MinDotsPerRow)
        {
            if (rowDotCounts == null || rowDotCounts.Count != 4)
            {
                return false;
            }

            return rowDotCounts.All(c => c >= minDots);
        }

        /// <summary>Alias for <see cref="AllRowsReady"/>.</summary>
        public static bool AllColumnsReady(IReadOnlyList<int> columnDotCounts, int minDots = MinDotsPerRow)
            => AllRowsReady(columnDotCounts, minDots);
    }

}
