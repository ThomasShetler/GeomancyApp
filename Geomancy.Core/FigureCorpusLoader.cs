using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace GeomancyApp
{
    /// <summary>
    /// Loads and caches the static figure corpus from databank/FigureCorpus/Figures.json.
    /// </summary>
    internal static class FigureCorpusLoader
    {
        private static readonly object Gate = new object();
        private static List<FigureData> _figures;

        private const string DatabankRoot = "databank";
        private const string CorpusFolderName = "FigureCorpus";
        private const string FiguresFileName = "Figures.json";

        private static readonly string[] CourtRoleKeys =
        {
            "RightWitness",
            "LeftWitness",
            "Judge",
            "Reconciler"
        };

        public static List<FigureData> LoadFigures()
        {
            if (_figures != null)
                return _figures;

            lock (Gate)
            {
                if (_figures != null)
                    return _figures;

                var path = ResolveDataFile();
                var json = File.ReadAllText(path);
                var file = JsonConvert.DeserializeObject<FigureCorpusFileDto>(json);
                var entries = file?.FigureCorpus?.Figures;

                if (entries == null || entries.Count == 0)
                    throw new InvalidDataException("Figure corpus file contains no figures.");

                var figures = entries.Select(MapFigure).ToList();
                ValidateFigures(figures);
                _figures = figures;
                return _figures;
            }
        }

        private static FigureData MapFigure(FigureJsonDto dto)
        {
            return new FigureData
            {
                FigureID = dto.FigureId,
                Name = dto.Name,
                EnglishName = dto.EnglishName,
                OtherNames = dto.OtherNames,
                Quality = dto.Quality,
                Keyword = dto.Keyword,
                Imagery = dto.Imagery,
                StrongHouse = dto.StrongHouse,
                StrongHouseID = dto.StrongHouseId,
                WeakHouse = dto.WeakHouse,
                WeakHouseID = dto.WeakHouseId,
                Planet = dto.Planet,
                Sign = dto.Sign,
                InnerEl = dto.InnerEl,
                OuterEl = dto.OuterEl,
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
                Tagline = dto.Tagline,
                CoreMeaning = dto.CoreMeaning ?? new List<string>(),
                FavorableFor = dto.FavorableFor ?? new List<string>(),
                UnfavorableFor = dto.UnfavorableFor ?? new List<string>(),
                ElementalSynthesis = dto.ElementalSynthesis,
                TraditionalImagery = dto.TraditionalImagery ?? new List<string>(),
                Interpretation = dto.Interpretation ?? new List<string>(),
                InHouses = dto.InHouses != null
                    ? new Dictionary<string, string>(dto.InHouses, StringComparer.Ordinal)
                    : new Dictionary<string, string>(StringComparer.Ordinal),
                InCourtRoles = dto.InCourtRoles != null
                    ? new Dictionary<string, string>(dto.InCourtRoles, StringComparer.Ordinal)
                    : new Dictionary<string, string>(StringComparer.Ordinal),
                ModernExamples = dto.ModernExamples ?? new List<string>(),
                TraditionalSources = (dto.TraditionalSources ?? new List<TraditionalSourceJsonDto>())
                    .Select(s => new TraditionalSourceEntry
                    {
                        Author = s.Author,
                        Work = s.Work,
                        Section = s.Section ?? string.Empty,
                        Year = s.Year
                    }).ToList()
            };
        }

        private static void ValidateFigures(IReadOnlyList<FigureData> figures)
        {
            if (figures.Count != 16)
                throw new InvalidDataException($"Figure corpus must contain 16 figures; found {figures.Count}.");

            var names = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (var figure in figures)
            {
                if (string.IsNullOrWhiteSpace(figure.Name))
                    throw new InvalidDataException("Figure corpus contains an entry with a missing name.");

                if (!names.Add(figure.Name))
                    throw new InvalidDataException($"Duplicate figure name in corpus: '{figure.Name}'.");

                ValidateElementValue(figure.Name, "fire", figure.FireElement);
                ValidateElementValue(figure.Name, "air", figure.AirElement);
                ValidateElementValue(figure.Name, "water", figure.WaterElement);
                ValidateElementValue(figure.Name, "earth", figure.EarthElement);

                for (var house = 1; house <= 12; house++)
                {
                    var key = house.ToString();
                    if (figure.InHouses == null || !figure.InHouses.ContainsKey(key))
                        throw new InvalidDataException($"Figure '{figure.Name}' is missing in_houses['{key}'].");
                }

                foreach (var courtKey in CourtRoleKeys)
                {
                    if (figure.InCourtRoles == null || !figure.InCourtRoles.ContainsKey(courtKey))
                        throw new InvalidDataException($"Figure '{figure.Name}' is missing in_court_roles['{courtKey}'].");
                }
            }
        }

        private static void ValidateElementValue(string figureName, string elementName, string value)
        {
            if (!string.Equals(value, "Active", StringComparison.OrdinalIgnoreCase)
                && !string.Equals(value, "Passive", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidDataException(
                    $"Figure '{figureName}' has invalid {elementName}_element value '{value}'. Expected Active or Passive.");
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

        private sealed class FigureCorpusFileDto
        {
            [JsonProperty("FigureCorpus")]
            public FigureCorpusRootDto FigureCorpus { get; set; }
        }

        private sealed class FigureCorpusRootDto
        {
            [JsonProperty("schema_version")]
            public int SchemaVersion { get; set; }

            [JsonProperty("figures")]
            public List<FigureJsonDto> Figures { get; set; }
        }

        private sealed class FigureJsonDto
        {
            [JsonProperty("figure_id")] public string FigureId { get; set; }
            [JsonProperty("name")] public string Name { get; set; }
            [JsonProperty("english_name")] public string EnglishName { get; set; }
            [JsonProperty("other_names")] public string OtherNames { get; set; }
            [JsonProperty("quality")] public string Quality { get; set; }
            [JsonProperty("keyword")] public string Keyword { get; set; }
            [JsonProperty("imagery")] public string Imagery { get; set; }
            [JsonProperty("strong_house")] public string StrongHouse { get; set; }
            [JsonProperty("strong_house_id")] public int StrongHouseId { get; set; }
            [JsonProperty("weak_house")] public string WeakHouse { get; set; }
            [JsonProperty("weak_house_id")] public int WeakHouseId { get; set; }
            [JsonProperty("planet")] public string Planet { get; set; }
            [JsonProperty("sign")] public string Sign { get; set; }
            [JsonProperty("inner_el")] public string InnerEl { get; set; }
            [JsonProperty("outer_el")] public string OuterEl { get; set; }
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
            [JsonProperty("tagline")] public string Tagline { get; set; }
            [JsonProperty("core_meaning")] public List<string> CoreMeaning { get; set; }
            [JsonProperty("favorable_for")] public List<string> FavorableFor { get; set; }
            [JsonProperty("unfavorable_for")] public List<string> UnfavorableFor { get; set; }
            [JsonProperty("elemental_synthesis")] public string ElementalSynthesis { get; set; }
            [JsonProperty("traditional_imagery")] public List<string> TraditionalImagery { get; set; }
            [JsonProperty("interpretation")] public List<string> Interpretation { get; set; }
            [JsonProperty("in_houses")] public Dictionary<string, string> InHouses { get; set; }
            [JsonProperty("in_court_roles")] public Dictionary<string, string> InCourtRoles { get; set; }
            [JsonProperty("modern_examples")] public List<string> ModernExamples { get; set; }
            [JsonProperty("traditional_sources")] public List<TraditionalSourceJsonDto> TraditionalSources { get; set; }
        }

        private sealed class TraditionalSourceJsonDto
        {
            [JsonProperty("author")] public string Author { get; set; }
            [JsonProperty("work")] public string Work { get; set; }
            [JsonProperty("section")] public string Section { get; set; }
            [JsonProperty("year")] public int? Year { get; set; }
        }
    }
}
