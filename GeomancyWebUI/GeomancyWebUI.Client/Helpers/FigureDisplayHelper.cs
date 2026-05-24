using System;
using System.Collections.Generic;
using System.Linq;
using GeomancyWebUI.Client.Models;

namespace GeomancyWebUI.Client.Helpers
{
    public static class FigureDisplayHelper
    {
        public record ElementInfo(string Name, string Symbol, string Value, bool IsActive, string CssClass);
        public record HousePlacementInfo(int HouseNumber, string HouseName, string Text);
        public record CourtRoleInfo(string Key, string Label, string Text);

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

        public static IEnumerable<HousePlacementInfo> BuildHousePlacements(FigureModel fig)
        {
            if (fig.InHouses == null || fig.InHouses.Count == 0) yield break;

            foreach (var kv in fig.InHouses
                .Select(entry => (HouseNumber: ParseHouseNumber(entry.Key), Text: entry.Value))
                .Where(entry => entry.HouseNumber >= 1 && entry.HouseNumber <= 12 && !string.IsNullOrWhiteSpace(entry.Text))
                .OrderBy(entry => entry.HouseNumber))
            {
                yield return new HousePlacementInfo(kv.HouseNumber, GetHouseName(kv.HouseNumber), kv.Text.Trim());
            }
        }

        public static IEnumerable<CourtRoleInfo> BuildCourtRoles(FigureModel fig)
        {
            if (fig.InCourtRoles == null || fig.InCourtRoles.Count == 0) yield break;

            var ordered = new[]
            {
                ("RightWitness", "Right Witness"),
                ("LeftWitness", "Left Witness"),
                ("Judge", "Judge"),
                ("Reconciler", "Reconciler")
            };

            foreach (var (key, label) in ordered)
            {
                if (fig.InCourtRoles.TryGetValue(key, out var text) && !string.IsNullOrWhiteSpace(text))
                {
                    yield return new CourtRoleInfo(key, label, text.Trim());
                }
            }
        }

        public static int? TryResolveHouseNumber(string? houseName)
        {
            if (string.IsNullOrWhiteSpace(houseName)) return null;

            return houseName.Trim().ToLowerInvariant() switch
            {
                "first" => 1,
                "second" => 2,
                "third" => 3,
                "fourth" => 4,
                "fifth" => 5,
                "sixth" => 6,
                "seventh" => 7,
                "eighth" => 8,
                "ninth" => 9,
                "tenth" => 10,
                "eleventh" => 11,
                "twelfth" => 12,
                _ => null
            };
        }

        public static string GetHouseName(int houseNumber) => houseNumber switch
        {
            1 => "First",
            2 => "Second",
            3 => "Third",
            4 => "Fourth",
            5 => "Fifth",
            6 => "Sixth",
            7 => "Seventh",
            8 => "Eighth",
            9 => "Ninth",
            10 => "Tenth",
            11 => "Eleventh",
            12 => "Twelfth",
            _ => $"House {houseNumber}"
        };

        public static string GetLinePatternSummary(FigureModel fig) =>
            $"Head {fig.HeadLine} · Neck {fig.NeckLine} · Body {fig.BodyLine} · Foot {fig.FootLine}";

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

        private static int ParseHouseNumber(string? key)
        {
            if (int.TryParse(key, out var n)) return n;
            return -1;
        }
    }
}
