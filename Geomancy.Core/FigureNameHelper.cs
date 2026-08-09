using System;

namespace GeomancyApp
{
    /// <summary>
    /// Normalizes geomantic figure names for identity comparisons (occupation, company, translation).
    /// Multi-word figures must stay intact — taking only the first token collapses Fortuna Major/Minor.
    /// </summary>
    public static class FigureNameHelper
    {
        public static string Root(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return string.Empty;

            var cleaned = name.Trim().Trim('\u200b', '\u200c', '\uFEFF');

            if (cleaned.Contains("("))
                cleaned = cleaned.Substring(0, cleaned.IndexOf('(')).Trim();

            if (cleaned.StartsWith("Fortuna Major", StringComparison.OrdinalIgnoreCase)) return "Fortuna Major";
            if (cleaned.StartsWith("Fortuna Minor", StringComparison.OrdinalIgnoreCase)) return "Fortuna Minor";
            if (cleaned.StartsWith("Caput Draconis", StringComparison.OrdinalIgnoreCase)) return "Caput Draconis";
            if (cleaned.StartsWith("Cauda Draconis", StringComparison.OrdinalIgnoreCase)) return "Cauda Draconis";

            return cleaned.Split(' ')[0];
        }
    }
}
