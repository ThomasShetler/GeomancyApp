using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace GeomancyApp
{
    /// <summary>
    /// Loads and caches licensed Greer figure reference data from
    /// databank/FigureCorpus/GreersFigures.json.
    /// </summary>
    public static class GreerFigureCorpusLoader
    {
        private static readonly object Gate = new object();
        private static List<GreerFigureData> _figures;

        private const string DatabankRoot = "databank";
        private const string CorpusFolderName = "FigureCorpus";
        private const string FiguresFileName = "GreersFigures.json";

        public static IReadOnlyList<GreerFigureData> LoadFigures()
        {
            if (_figures != null)
                return _figures;

            lock (Gate)
            {
                if (_figures != null)
                    return _figures;

                var path = ResolveDataFile();
                var json = File.ReadAllText(path);
                var file = JsonConvert.DeserializeObject<GreerFigureCorpusFileDto>(json);
                var entries = file?.GreerFigureCorpus?.Figures;

                if (entries == null || entries.Count == 0)
                    throw new InvalidDataException("Greer figure corpus file contains no figures.");

                var figures = entries.Select(MapFigure).ToList();
                ValidateFigures(figures);
                _figures = figures;
                return _figures;
            }
        }

        public static GreerFigureData GetFigureByName(string figureName)
        {
            if (string.IsNullOrWhiteSpace(figureName))
                return null;

            var key = NormalizeFigureKey(figureName);
            return LoadFigures().FirstOrDefault(f => NormalizeFigureKey(f.Name) == key);
        }

        public static string NormalizeFigureKey(string figureName)
        {
            if (string.IsNullOrWhiteSpace(figureName))
                return string.Empty;

            var trimmed = figureName.Trim();
            var paren = trimmed.IndexOf('(');
            if (paren > 0)
                trimmed = trimmed.Substring(0, paren).Trim();

            return trimmed.ToLowerInvariant();
        }

        private static GreerFigureData MapFigure(GreerFigureJsonDto dto) =>
            new GreerFigureData
            {
                FigureId = dto.FigureId,
                Name = dto.Name,
                EnglishName = dto.EnglishName,
                OtherNames = dto.OtherNames,
                Keyword = dto.Keyword,
                Quality = dto.Quality,
                Planet = dto.Planet,
                Sign = dto.Sign,
                Imagery = dto.Imagery,
                StrongHouse = dto.StrongHouse,
                StrongHouseId = dto.StrongHouseId,
                WeakHouse = dto.WeakHouse,
                WeakHouseId = dto.WeakHouseId,
                OuterEl = dto.OuterEl,
                InnerEl = dto.InnerEl,
                FireElement = dto.FireElement,
                AirElement = dto.AirElement,
                WaterElement = dto.WaterElement,
                EarthElement = dto.EarthElement,
                Anatomy = dto.Anatomy,
                BodyType = dto.BodyType,
                CharacterType = dto.CharacterType,
                Colors = dto.Colors,
                Commentary = dto.Commentary,
                DivinatoryMeaning = dto.DivinatoryMeaning,
                Source = dto.Source == null
                    ? null
                    : new GreerSourceEntry
                    {
                        Work = dto.Source.Work,
                        Chapter = dto.Source.Chapter,
                        Pages = dto.Source.Pages,
                        Attribution = dto.Source.Attribution
                    }
            };

        private static void ValidateFigures(IReadOnlyList<GreerFigureData> figures)
        {
            if (figures.Count != 16)
                throw new InvalidDataException(
                    $"Greer figure corpus must contain 16 figures; found {figures.Count}.");

            var names = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (var figure in figures)
            {
                if (string.IsNullOrWhiteSpace(figure.Name))
                    throw new InvalidDataException("Greer figure corpus contains an entry with a missing name.");

                if (!names.Add(figure.Name))
                    throw new InvalidDataException($"Duplicate Greer figure name in corpus: '{figure.Name}'.");
            }
        }

        private static string ResolveDataFile()
        {
            var baseDir = AppContext.BaseDirectory;

            var beside = Path.Combine(baseDir, DatabankRoot, CorpusFolderName, FiguresFileName);
            if (File.Exists(beside))
                return beside;

            var dir = new DirectoryInfo(baseDir);
            while (dir != null)
            {
                var candidate = Path.Combine(dir.FullName, DatabankRoot, CorpusFolderName, FiguresFileName);
                if (File.Exists(candidate))
                    return candidate;
                dir = dir.Parent;
            }

            throw new FileNotFoundException(
                $"Could not locate {FiguresFileName}. Expected '{beside}' (copied via csproj <Content>) " +
                $"or a parent folder named '{DatabankRoot}/{CorpusFolderName}'.");
        }

        private sealed class GreerFigureCorpusFileDto
        {
            [JsonProperty("GreerFigureCorpus")]
            public GreerFigureCorpusRootDto GreerFigureCorpus { get; set; }
        }

        private sealed class GreerFigureCorpusRootDto
        {
            [JsonProperty("figures")]
            public List<GreerFigureJsonDto> Figures { get; set; }
        }

        private sealed class GreerFigureJsonDto
        {
            [JsonProperty("figure_id")] public string FigureId { get; set; }
            [JsonProperty("name")] public string Name { get; set; }
            [JsonProperty("english_name")] public string EnglishName { get; set; }
            [JsonProperty("other_names")] public string OtherNames { get; set; }
            [JsonProperty("keyword")] public string Keyword { get; set; }
            [JsonProperty("quality")] public string Quality { get; set; }
            [JsonProperty("planet")] public string Planet { get; set; }
            [JsonProperty("sign")] public string Sign { get; set; }
            [JsonProperty("imagery")] public string Imagery { get; set; }
            [JsonProperty("strong_house")] public string StrongHouse { get; set; }
            [JsonProperty("strong_house_id")] public int StrongHouseId { get; set; }
            [JsonProperty("weak_house")] public string WeakHouse { get; set; }
            [JsonProperty("weak_house_id")] public int WeakHouseId { get; set; }
            [JsonProperty("outer_el")] public string OuterEl { get; set; }
            [JsonProperty("inner_el")] public string InnerEl { get; set; }
            [JsonProperty("fire_element")] public string FireElement { get; set; }
            [JsonProperty("air_element")] public string AirElement { get; set; }
            [JsonProperty("water_element")] public string WaterElement { get; set; }
            [JsonProperty("earth_element")] public string EarthElement { get; set; }
            [JsonProperty("anatomy")] public string Anatomy { get; set; }
            [JsonProperty("body_type")] public string BodyType { get; set; }
            [JsonProperty("character_type")] public string CharacterType { get; set; }
            [JsonProperty("colors")] public string Colors { get; set; }
            [JsonProperty("commentary")] public string Commentary { get; set; }
            [JsonProperty("divinatory_meaning")] public string DivinatoryMeaning { get; set; }
            [JsonProperty("source")] public GreerSourceJsonDto Source { get; set; }
        }

        private sealed class GreerSourceJsonDto
        {
            [JsonProperty("work")] public string Work { get; set; }
            [JsonProperty("chapter")] public string Chapter { get; set; }
            [JsonProperty("pages")] public string Pages { get; set; }
            [JsonProperty("attribution")] public string Attribution { get; set; }
        }
    }

    public sealed class GreerFigureData
    {
        public string FigureId { get; set; }
        public string Name { get; set; }
        public string EnglishName { get; set; }
        public string OtherNames { get; set; }
        public string Keyword { get; set; }
        public string Quality { get; set; }
        public string Planet { get; set; }
        public string Sign { get; set; }
        public string Imagery { get; set; }
        public string StrongHouse { get; set; }
        public int StrongHouseId { get; set; }
        public string WeakHouse { get; set; }
        public int WeakHouseId { get; set; }
        public string OuterEl { get; set; }
        public string InnerEl { get; set; }
        public string FireElement { get; set; }
        public string AirElement { get; set; }
        public string WaterElement { get; set; }
        public string EarthElement { get; set; }
        public string Anatomy { get; set; }
        public string BodyType { get; set; }
        public string CharacterType { get; set; }
        public string Colors { get; set; }
        public string Commentary { get; set; }
        public string DivinatoryMeaning { get; set; }
        public GreerSourceEntry Source { get; set; }
    }

    public sealed class GreerSourceEntry
    {
        public string Work { get; set; }
        public string Chapter { get; set; }
        public string Pages { get; set; }
        public string Attribution { get; set; }
    }
}
