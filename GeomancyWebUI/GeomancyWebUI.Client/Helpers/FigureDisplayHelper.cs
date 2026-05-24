using System;
using System.Collections.Generic;
using System.Linq;
using GeomancyWebUI.Client.Models;

namespace GeomancyWebUI.Client.Helpers
{
    public static class FigureDisplayHelper
    {
        public record ElementInfo(string Name, string Symbol, string Value, bool IsActive, string CssClass);

        public static IEnumerable<ElementInfo> BuildElements(FigureModel fig)
        {
            yield return MakeElement("Fire", "🜂", fig.FireElement, "fire");
            yield return MakeElement("Air", "🜁", fig.AirElement, "air");
            yield return MakeElement("Water", "🜄", fig.WaterElement, "water");
            yield return MakeElement("Earth", "🜃", fig.EarthElement, "earth");
        }

        public static string GetQualityChipClass(string? quality) => quality?.Trim().ToLowerInvariant() switch
        {
            "mobile" => "chip-quality chip-quality-mobile",
            "stable" => "chip-quality chip-quality-stable",
            _ => "chip-quality"
        };

        public static bool HasInterpretationContent(FigureModel fig) =>
            !string.IsNullOrWhiteSpace(fig.Imagery)
            || fig.TraditionalImagery?.Any() == true
            || !string.IsNullOrWhiteSpace(fig.ElementalSynthesis)
            || fig.Interpretation?.Any() == true
            || !string.IsNullOrWhiteSpace(fig.Commentary)
            || fig.ModernExamples?.Any() == true;

        public static bool HasPersonContent(FigureModel fig) =>
            !string.IsNullOrWhiteSpace(fig.BodyType)
            || !string.IsNullOrWhiteSpace(fig.TraditionalBodyType)
            || !string.IsNullOrWhiteSpace(fig.CharacterType)
            || !string.IsNullOrWhiteSpace(fig.TraditionalCharacterType)
            || !string.IsNullOrWhiteSpace(fig.Anatomy)
            || !string.IsNullOrWhiteSpace(fig.Colors);

        public static bool HasPlanetaryContent(FigureModel fig) =>
            !string.IsNullOrWhiteSpace(fig.PlanetaryIntelligence)
            || !string.IsNullOrWhiteSpace(fig.PlanetarySpirit)
            || !string.IsNullOrWhiteSpace(fig.PlanetaryAngel);

        public static bool HasReadingCraftContent(HouseDirectoryEntry houseEntry) =>
            houseEntry.KeySignificators?.Any() == true
            || houseEntry.CommonMisreadings?.Any() == true
            || !string.IsNullOrWhiteSpace(houseEntry.FigureCombinationsToWatch);

        public static string GetPlanetSymbol(string? planet) => planet?.Trim().ToLowerInvariant() switch
        {
            "sun" => "☉",
            "moon" => "☽",
            "mercury" => "☿",
            "venus" => "♀",
            "mars" => "♂",
            "jupiter" => "♃",
            "saturn" => "♄",
            "uranus" => "♅",
            "neptune" => "♆",
            "pluto" => "♇",
            "north node" => "☊",
            "ascending node" => "☊",
            "south node" => "☋",
            "descending node" => "☋",
            "dragon's head" => "☊",
            "dragon's tail" => "☋",
            _ => "✦"
        };

        public static string GetZodiacSymbol(string? sign) => sign?.Trim().ToLowerInvariant() switch
        {
            "aries" => "♈",
            "taurus" => "♉",
            "gemini" => "♊",
            "cancer" => "♋",
            "leo" => "♌",
            "virgo" => "♍",
            "libra" => "♎",
            "scorpio" => "♏",
            "sagittarius" => "♐",
            "capricorn" => "♑",
            "aquarius" => "♒",
            "pisces" => "♓",
            _ => "✧"
        };

        public static string GetElementSymbol(string? element) => element?.Trim().ToLowerInvariant() switch
        {
            "fire" => "🜂",
            "air" => "🜁",
            "water" => "🜄",
            "earth" => "🜃",
            _ => "✦"
        };

        public static string GetElementCssClass(string? element) => element?.Trim().ToLowerInvariant() switch
        {
            "fire" => "fire",
            "air" => "air",
            "water" => "water",
            "earth" => "earth",
            _ => string.Empty
        };

        private static ElementInfo MakeElement(string name, string symbol, string? raw, string cssClass)
        {
            var trimmed = (raw ?? string.Empty).Trim();
            var dormant = string.IsNullOrEmpty(trimmed)
                || trimmed.Equals("0", StringComparison.OrdinalIgnoreCase)
                || trimmed.Equals("none", StringComparison.OrdinalIgnoreCase)
                || trimmed.Equals("inactive", StringComparison.OrdinalIgnoreCase)
                || trimmed.Equals("passive", StringComparison.OrdinalIgnoreCase)
                || trimmed.Equals("false", StringComparison.OrdinalIgnoreCase);
            return new ElementInfo(name, symbol, string.IsNullOrEmpty(trimmed) ? "—" : trimmed, !dormant, cssClass);
        }
    }
}
