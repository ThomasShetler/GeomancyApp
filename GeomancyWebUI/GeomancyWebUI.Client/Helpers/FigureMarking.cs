namespace GeomancyWebUI.Client.Helpers
{
    /// <summary>
    /// Classical geomantic dot-marking: pair dots two at a time from the top of each row/column
    /// until 0, 1, or 2 remain. Odd remainder → single/active line (1); even → double/passive (2).
    /// </summary>
    public static class FigureMarking
    {
        public const int MinDotsPerColumn = 3;
        public const int MaxDotsPerColumn = 16;

        /// <summary>
        /// Resolves a geomantic line value (1 or 2) from the number of dots marked in one row/column.
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
        /// Returns ordered index pairs (top-down) for pairing animation within one column.
        /// Indices are 0-based from the top of the stack.
        /// </summary>
        public static IReadOnlyList<(int a, int b)> GetPairingSteps(int dotCount)
        {
            if (dotCount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(dotCount));
            }

            var steps = new List<(int, int)>();
            var paired = new bool[dotCount];
            var pairedCount = 0;

            while (pairedCount < dotCount)
            {
                var first = -1;
                for (var i = 0; i < dotCount; i++)
                {
                    if (!paired[i])
                    {
                        first = i;
                        break;
                    }
                }

                if (first < 0)
                {
                    break;
                }

                var second = -1;
                for (var i = first + 1; i < dotCount; i++)
                {
                    if (!paired[i])
                    {
                        second = i;
                        break;
                    }
                }

                if (second < 0)
                {
                    break;
                }

                steps.Add((first, second));
                paired[first] = true;
                paired[second] = true;
                pairedCount += 2;
            }

            return steps;
        }

        public static bool AllColumnsReady(IReadOnlyList<int> columnDotCounts, int minDots = MinDotsPerColumn)
        {
            if (columnDotCounts == null || columnDotCounts.Count != 4)
            {
                return false;
            }

            return columnDotCounts.All(c => c >= minDots);
        }
    }

}
