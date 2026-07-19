using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GeomancyWebUI.Client.Models
{
    public class GreerSourceModel
    {
        [JsonPropertyName("work")]
        public string Work { get; set; } = string.Empty;

        [JsonPropertyName("chapter")]
        public string Chapter { get; set; } = string.Empty;

        [JsonPropertyName("pages")]
        public string Pages { get; set; } = string.Empty;

        [JsonPropertyName("attribution")]
        public string Attribution { get; set; } = string.Empty;
    }

    public class GreerFigureModel
    {
        [JsonPropertyName("figure_id")]
        public string FigureId { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("english_name")]
        public string EnglishName { get; set; } = string.Empty;

        [JsonPropertyName("other_names")]
        public string OtherNames { get; set; } = string.Empty;

        [JsonPropertyName("keyword")]
        public string Keyword { get; set; } = string.Empty;

        [JsonPropertyName("quality")]
        public string Quality { get; set; } = string.Empty;

        [JsonPropertyName("planet")]
        public string Planet { get; set; } = string.Empty;

        [JsonPropertyName("sign")]
        public string Sign { get; set; } = string.Empty;

        [JsonPropertyName("imagery")]
        public string Imagery { get; set; } = string.Empty;

        [JsonPropertyName("strong_house")]
        public string StrongHouse { get; set; } = string.Empty;

        [JsonPropertyName("strong_house_id")]
        public int StrongHouseId { get; set; }

        [JsonPropertyName("weak_house")]
        public string WeakHouse { get; set; } = string.Empty;

        [JsonPropertyName("weak_house_id")]
        public int WeakHouseId { get; set; }

        [JsonPropertyName("outer_el")]
        public string OuterEl { get; set; } = string.Empty;

        [JsonPropertyName("inner_el")]
        public string InnerEl { get; set; } = string.Empty;

        [JsonPropertyName("fire_element")]
        public string FireElement { get; set; } = string.Empty;

        [JsonPropertyName("air_element")]
        public string AirElement { get; set; } = string.Empty;

        [JsonPropertyName("water_element")]
        public string WaterElement { get; set; } = string.Empty;

        [JsonPropertyName("earth_element")]
        public string EarthElement { get; set; } = string.Empty;

        [JsonPropertyName("anatomy")]
        public string Anatomy { get; set; } = string.Empty;

        [JsonPropertyName("body_type")]
        public string BodyType { get; set; } = string.Empty;

        [JsonPropertyName("character_type")]
        public string CharacterType { get; set; } = string.Empty;

        [JsonPropertyName("colors")]
        public string Colors { get; set; } = string.Empty;

        [JsonPropertyName("commentary")]
        public string Commentary { get; set; } = string.Empty;

        [JsonPropertyName("divinatory_meaning")]
        public string DivinatoryMeaning { get; set; } = string.Empty;

        [JsonPropertyName("source")]
        public GreerSourceModel? Source { get; set; }
    }

    public class GreerHouseEntry
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("ordinal")]
        public string Ordinal { get; set; } = string.Empty;

        [JsonPropertyName("governs")]
        public List<string> Governs { get; set; } = new List<string>();

        [JsonPropertyName("question_involves")]
        public List<string> QuestionInvolves { get; set; } = new List<string>();

        [JsonPropertyName("additional_details")]
        public List<string> AdditionalDetails { get; set; } = new List<string>();

        [JsonPropertyName("example_questions")]
        public List<string> ExampleQuestions { get; set; } = new List<string>();

        [JsonPropertyName("source")]
        public GreerSourceModel? Source { get; set; }
    }

    public class GreerHouseDirectory
    {
        [JsonPropertyName("houses")]
        public List<GreerHouseEntry> Houses { get; set; } = new List<GreerHouseEntry>();
    }
}
