using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GeomancyWebUI.Client.Models
{
    public class TraditionalSourceModel
    {
        [JsonPropertyName("author")]
        public string Author { get; set; } = string.Empty;

        [JsonPropertyName("work")]
        public string Work { get; set; } = string.Empty;

        [JsonPropertyName("section")]
        public string Section { get; set; } = string.Empty;

        [JsonPropertyName("year")]
        public int? Year { get; set; }
    }

    public enum FigureInHouseStrength
    {
        StrongInHouse = 1,
        WeakInHouse = 2,
        Neutral = 3
    }

    public class FigureModel
    {
        public string Name { get; set; } = string.Empty;
        public string OtherNames { get; set; } = string.Empty;
        public string Quality { get; set; } = string.Empty;
        public string Keyword { get; set; } = string.Empty;
        public string Imagery { get; set; } = string.Empty;
        public string StrongHouse { get; set; } = string.Empty;
        public string WeakHouse { get; set; } = string.Empty;
        public string Planet { get; set; } = string.Empty;
        public string Sign { get; set; } = string.Empty;
        public string InnerEl { get; set; } = string.Empty;
        public string OuterEl { get; set; } = string.Empty;
        public string FireElement { get; set; } = string.Empty;
        public string AirElement { get; set; } = string.Empty;
        public string WaterElement { get; set; } = string.Empty;
        public string EarthElement { get; set; } = string.Empty;
        public string Anatomy { get; set; } = string.Empty;
        public string BodyType { get; set; } = string.Empty;
        public string CharacterType { get; set; } = string.Empty;
        public string Colors { get; set; } = string.Empty;
        public string Commentary { get; set; } = string.Empty;
        public string DivinatoryMeaning { get; set; } = string.Empty;
        public string ElementalPattern { get; set; } = string.Empty;

        [JsonPropertyName("tagline")]
        public string Tagline { get; set; } = string.Empty;

        [JsonPropertyName("core_meaning")]
        public List<string> CoreMeaning { get; set; } = new List<string>();

        [JsonPropertyName("favorable_for")]
        public List<string> FavorableFor { get; set; } = new List<string>();

        [JsonPropertyName("unfavorable_for")]
        public List<string> UnfavorableFor { get; set; } = new List<string>();

        [JsonPropertyName("elemental_synthesis")]
        public string ElementalSynthesis { get; set; } = string.Empty;

        [JsonPropertyName("traditional_imagery")]
        public List<string> TraditionalImagery { get; set; } = new List<string>();

        [JsonPropertyName("interpretation")]
        public List<string> Interpretation { get; set; } = new List<string>();

        [JsonPropertyName("in_houses")]
        public Dictionary<string, string> InHouses { get; set; } = new Dictionary<string, string>();

        [JsonPropertyName("modern_examples")]
        public List<string> ModernExamples { get; set; } = new List<string>();

        [JsonPropertyName("traditional_sources")]
        public List<TraditionalSourceModel> TraditionalSources { get; set; } = new List<TraditionalSourceModel>();

        public int HeadLine { get; set; }
        public int NeckLine { get; set; }
        public int BodyLine { get; set; }
        public int FootLine { get; set; }
        public FigureInHouseStrength HouseStrength { get; set; }
    }
}

