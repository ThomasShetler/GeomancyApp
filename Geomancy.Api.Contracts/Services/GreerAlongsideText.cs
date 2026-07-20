using System;

namespace GeomancyAPI.Services
{
    /// <summary>
    /// Alongside-mode text comparison: when Greer and corpus values match, UI keeps Greer only.
    /// </summary>
    public static class GreerAlongsideText
    {
        /// <summary>
        /// True when both sides have text and match after trim + whitespace collapse (case-insensitive).
        /// </summary>
        public static bool TextMatches(string a, string b)
        {
            var left = CollapseWhitespace(a);
            var right = CollapseWhitespace(b);
            return left.Length > 0
                && right.Length > 0
                && string.Equals(left, right, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>True when Greer has a distinct non-empty value compared to the corpus field.</summary>
        public static bool GreerFieldDiffers(string geofancy, string greer) =>
            !string.IsNullOrWhiteSpace(greer) && !TextMatches(geofancy, greer);

        private static string CollapseWhitespace(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return string.Empty;

            return string.Join(" ", value.Split((char[])null, StringSplitOptions.RemoveEmptyEntries));
        }
    }
}
