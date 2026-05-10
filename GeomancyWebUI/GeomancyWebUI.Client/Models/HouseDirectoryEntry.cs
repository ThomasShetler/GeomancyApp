using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GeomancyWebUI.Client.Models
{
    /// <summary>
    /// Reference data for a single house (1-12) sourced from the
    /// HouseAndCourtDirectory/HouseData.json file via the API.
    /// </summary>
    public class HouseDirectoryEntry
    {
        public int Id { get; set; }
        public string House { get; set; } = string.Empty;
        public string TraditionalName { get; set; } = string.Empty;
        public string AstrologicalCorrespondence { get; set; } = string.Empty;
        public List<string> Governs { get; set; } = new List<string>();
        public string SignificatorOfQuesitedWhen { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;

        [JsonPropertyName("interpretive_essence")]
        public string InterpretiveEssence { get; set; } = string.Empty;

        [JsonPropertyName("key_significators")]
        public List<string> KeySignificators { get; set; } = new List<string>();

        [JsonPropertyName("common_misreadings")]
        public List<string> CommonMisreadings { get; set; } = new List<string>();

        [JsonPropertyName("figure_combinations_to_watch")]
        public string FigureCombinationsToWatch { get; set; } = string.Empty;

        public List<string> ExampleQuestions { get; set; } = new List<string>();
    }
}
