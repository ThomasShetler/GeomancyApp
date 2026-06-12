using System;
using System.Collections.Generic;
using GeomancyWebUI.Client.Models;

namespace GeomancyWebUI.Client.Helpers
{
    public sealed class FigureReferenceBundle
    {
        public FigureModel DisplayFigure { get; init; } = new FigureModel();
        public GreerFigureModel? GreerFigure { get; init; }
        public bool ShowGreerAlongside { get; init; }
        public bool ShowGreerAttribution { get; init; }
        public bool HideGeofancyOnlySections { get; init; }
    }

    public sealed class HouseReferenceBundle
    {
        public string Description { get; init; } = string.Empty;
        public List<string> ExampleQuestions { get; init; } = new List<string>();
        public GreerHouseEntry? GreerHouse { get; init; }
        public bool ShowGreerAlongside { get; init; }
        public bool ShowGreerAttribution { get; init; }
        public bool UseGreerPrimary { get; init; }
    }

    public static class GreerReferenceMergeHelper
    {
        public static FigureReferenceBundle MergeFigure(
            FigureModel? baseFigure,
            GreerFigureModel? greer,
            GreerReferenceMode mode)
        {
            if (baseFigure == null)
                baseFigure = new FigureModel();

            if (mode == GreerReferenceMode.Off || greer == null)
            {
                return new FigureReferenceBundle
                {
                    DisplayFigure = baseFigure,
                    ShowGreerAttribution = false,
                    HideGeofancyOnlySections = false
                };
            }

            return mode switch
            {
                GreerReferenceMode.Alongside => new FigureReferenceBundle
                {
                    DisplayFigure = baseFigure,
                    GreerFigure = greer,
                    ShowGreerAlongside = true,
                    ShowGreerAttribution = true,
                    HideGeofancyOnlySections = false
                },
                GreerReferenceMode.Override => new FigureReferenceBundle
                {
                    DisplayFigure = ApplyGreerOverrides(baseFigure, greer),
                    GreerFigure = greer,
                    ShowGreerAttribution = true,
                    HideGeofancyOnlySections = false
                },
                GreerReferenceMode.GreerOnly => new FigureReferenceBundle
                {
                    DisplayFigure = BuildGreerOnlyFigure(baseFigure, greer),
                    GreerFigure = greer,
                    ShowGreerAttribution = true,
                    HideGeofancyOnlySections = true
                },
                _ => new FigureReferenceBundle { DisplayFigure = baseFigure }
            };
        }

        public static HouseReferenceBundle MergeHouse(
            HouseDirectoryEntry? baseHouse,
            GreerHouseEntry? greer,
            GreerReferenceMode mode)
        {
            if (mode == GreerReferenceMode.Off || greer == null)
            {
                return new HouseReferenceBundle
                {
                    Description = baseHouse?.InterpretiveEssence ?? string.Empty,
                    ExampleQuestions = baseHouse?.ExampleQuestions ?? new List<string>(),
                    ShowGreerAttribution = false
                };
            }

            return mode switch
            {
                GreerReferenceMode.Alongside => new HouseReferenceBundle
                {
                    Description = baseHouse?.InterpretiveEssence ?? string.Empty,
                    ExampleQuestions = baseHouse?.ExampleQuestions ?? new List<string>(),
                    GreerHouse = greer,
                    ShowGreerAlongside = true,
                    ShowGreerAttribution = true
                },
                GreerReferenceMode.Override => new HouseReferenceBundle
                {
                    Description = FirstNonEmpty(greer.Description, baseHouse?.InterpretiveEssence),
                    ExampleQuestions = greer.ExampleQuestions?.Count > 0
                        ? greer.ExampleQuestions
                        : baseHouse?.ExampleQuestions ?? new List<string>(),
                    GreerHouse = greer,
                    ShowGreerAttribution = true,
                    UseGreerPrimary = true
                },
                GreerReferenceMode.GreerOnly => new HouseReferenceBundle
                {
                    Description = greer.Description ?? string.Empty,
                    ExampleQuestions = greer.ExampleQuestions ?? new List<string>(),
                    GreerHouse = greer,
                    ShowGreerAttribution = true,
                    UseGreerPrimary = true
                },
                _ => new HouseReferenceBundle
                {
                    Description = baseHouse?.InterpretiveEssence ?? string.Empty,
                    ExampleQuestions = baseHouse?.ExampleQuestions ?? new List<string>()
                }
            };
        }

        public static bool HasGreerInterpretationContent(GreerFigureModel? greer) =>
            greer != null && (
                !string.IsNullOrWhiteSpace(greer.Imagery)
                || !string.IsNullOrWhiteSpace(greer.Commentary)
                || !string.IsNullOrWhiteSpace(greer.DivinatoryMeaning));

        public static bool HasGreerPersonContent(GreerFigureModel? greer) =>
            greer != null && (
                !string.IsNullOrWhiteSpace(greer.BodyType)
                || !string.IsNullOrWhiteSpace(greer.CharacterType)
                || !string.IsNullOrWhiteSpace(greer.Anatomy)
                || !string.IsNullOrWhiteSpace(greer.Colors));

        public static bool HasGreerCorrespondenceContent(GreerFigureModel? greer) =>
            greer != null && (
                HasGreerPersonContent(greer)
                || !string.IsNullOrWhiteSpace(greer.StrongHouse)
                || !string.IsNullOrWhiteSpace(greer.WeakHouse)
                || !string.IsNullOrWhiteSpace(greer.FireElement)
                || !string.IsNullOrWhiteSpace(greer.OuterEl));

        public static bool HasGreerContextContent(GreerFigureModel? greer) =>
            greer != null && !string.IsNullOrWhiteSpace(greer.DivinatoryMeaning);

        public static bool GreerFieldDiffers(string? geofancy, string? greer) =>
            !string.IsNullOrWhiteSpace(greer)
            && !string.Equals(NormalizeText(geofancy), NormalizeText(greer), StringComparison.Ordinal);

        public static string NormalizeFigureKey(string? figureName)
        {
            if (string.IsNullOrWhiteSpace(figureName))
                return string.Empty;

            var trimmed = figureName.Trim();
            var paren = trimmed.IndexOf('(', StringComparison.Ordinal);
            if (paren > 0)
                trimmed = trimmed[..paren].Trim();

            return trimmed.ToLowerInvariant();
        }

        private static string NormalizeText(string? value) =>
            string.IsNullOrWhiteSpace(value) ? string.Empty : value.Trim();

        private static FigureModel ApplyGreerOverrides(FigureModel baseFigure, GreerFigureModel greer)
        {
            var copy = CloneFigure(baseFigure);
            copy.EnglishName = FirstNonEmpty(greer.EnglishName, copy.EnglishName);
            copy.OtherNames = FirstNonEmpty(greer.OtherNames, copy.OtherNames);
            copy.Keyword = FirstNonEmpty(greer.Keyword, copy.Keyword);
            copy.Quality = FirstNonEmpty(greer.Quality, copy.Quality);
            copy.Planet = FirstNonEmpty(greer.Planet, copy.Planet);
            copy.Sign = FirstNonEmpty(greer.Sign, copy.Sign);
            copy.Imagery = FirstNonEmpty(greer.Imagery, copy.Imagery);
            copy.StrongHouse = FirstNonEmpty(greer.StrongHouse, copy.StrongHouse);
            copy.WeakHouse = FirstNonEmpty(greer.WeakHouse, copy.WeakHouse);
            copy.InnerEl = FirstNonEmpty(greer.InnerEl, copy.InnerEl);
            copy.OuterEl = FirstNonEmpty(greer.OuterEl, copy.OuterEl);
            copy.FireElement = FirstNonEmpty(greer.FireElement, copy.FireElement);
            copy.AirElement = FirstNonEmpty(greer.AirElement, copy.AirElement);
            copy.WaterElement = FirstNonEmpty(greer.WaterElement, copy.WaterElement);
            copy.EarthElement = FirstNonEmpty(greer.EarthElement, copy.EarthElement);
            copy.Anatomy = FirstNonEmpty(greer.Anatomy, copy.Anatomy);
            copy.BodyType = FirstNonEmpty(greer.BodyType, copy.BodyType);
            copy.CharacterType = FirstNonEmpty(greer.CharacterType, copy.CharacterType);
            copy.Colors = FirstNonEmpty(greer.Colors, copy.Colors);
            copy.Commentary = FirstNonEmpty(greer.Commentary, copy.Commentary);
            copy.DivinatoryMeaning = FirstNonEmpty(greer.DivinatoryMeaning, copy.DivinatoryMeaning);

            if (!string.IsNullOrWhiteSpace(greer.BodyType))
                copy.TraditionalBodyType = string.Empty;
            if (!string.IsNullOrWhiteSpace(greer.CharacterType))
                copy.TraditionalCharacterType = string.Empty;

            return copy;
        }

        private static FigureModel BuildGreerOnlyFigure(FigureModel baseFigure, GreerFigureModel greer) =>
            new FigureModel
            {
                Name = FirstNonEmpty(greer.Name, baseFigure.Name),
                EnglishName = greer.EnglishName ?? string.Empty,
                OtherNames = greer.OtherNames ?? string.Empty,
                Quality = greer.Quality ?? string.Empty,
                Keyword = greer.Keyword ?? string.Empty,
                Imagery = greer.Imagery ?? string.Empty,
                StrongHouse = greer.StrongHouse ?? string.Empty,
                WeakHouse = greer.WeakHouse ?? string.Empty,
                Planet = greer.Planet ?? string.Empty,
                Sign = greer.Sign ?? string.Empty,
                InnerEl = greer.InnerEl ?? string.Empty,
                OuterEl = greer.OuterEl ?? string.Empty,
                FireElement = greer.FireElement ?? string.Empty,
                AirElement = greer.AirElement ?? string.Empty,
                WaterElement = greer.WaterElement ?? string.Empty,
                EarthElement = greer.EarthElement ?? string.Empty,
                Anatomy = greer.Anatomy ?? string.Empty,
                BodyType = greer.BodyType ?? string.Empty,
                CharacterType = greer.CharacterType ?? string.Empty,
                Colors = greer.Colors ?? string.Empty,
                Commentary = greer.Commentary ?? string.Empty,
                DivinatoryMeaning = greer.DivinatoryMeaning ?? string.Empty,
                HeadLine = baseFigure.HeadLine,
                NeckLine = baseFigure.NeckLine,
                BodyLine = baseFigure.BodyLine,
                FootLine = baseFigure.FootLine,
                HouseStrength = baseFigure.HouseStrength
            };

        private static FigureModel CloneFigure(FigureModel source) =>
            new FigureModel
            {
                Name = source.Name,
                EnglishName = source.EnglishName,
                OtherNames = source.OtherNames,
                Quality = source.Quality,
                Keyword = source.Keyword,
                Imagery = source.Imagery,
                StrongHouse = source.StrongHouse,
                WeakHouse = source.WeakHouse,
                Planet = source.Planet,
                Sign = source.Sign,
                Humor = source.Humor,
                PlanetaryIntelligence = source.PlanetaryIntelligence,
                PlanetarySpirit = source.PlanetarySpirit,
                PlanetaryAngel = source.PlanetaryAngel,
                InnerEl = source.InnerEl,
                OuterEl = source.OuterEl,
                FireElement = source.FireElement,
                AirElement = source.AirElement,
                WaterElement = source.WaterElement,
                EarthElement = source.EarthElement,
                Anatomy = source.Anatomy,
                BodyType = source.BodyType,
                TraditionalBodyType = source.TraditionalBodyType,
                CharacterType = source.CharacterType,
                TraditionalCharacterType = source.TraditionalCharacterType,
                Colors = source.Colors,
                Commentary = source.Commentary,
                DivinatoryMeaning = source.DivinatoryMeaning,
                ElementalPattern = source.ElementalPattern,
                HeadLine = source.HeadLine,
                NeckLine = source.NeckLine,
                BodyLine = source.BodyLine,
                FootLine = source.FootLine,
                HouseStrength = source.HouseStrength,
                Tagline = source.Tagline,
                CoreMeaning = new List<string>(source.CoreMeaning),
                FavorableFor = new List<string>(source.FavorableFor),
                UnfavorableFor = new List<string>(source.UnfavorableFor),
                ElementalSynthesis = source.ElementalSynthesis,
                TraditionalImagery = new List<string>(source.TraditionalImagery),
                Interpretation = new List<string>(source.Interpretation),
                InHouses = new Dictionary<string, string>(source.InHouses),
                InCourtRoles = new Dictionary<string, string>(source.InCourtRoles),
                ModernExamples = new List<string>(source.ModernExamples),
                TraditionalSources = new List<TraditionalSourceModel>(source.TraditionalSources)
            };

        private static string FirstNonEmpty(string? preferred, string? fallback) =>
            !string.IsNullOrWhiteSpace(preferred) ? preferred! :
            !string.IsNullOrWhiteSpace(fallback) ? fallback! : string.Empty;
    }
}
