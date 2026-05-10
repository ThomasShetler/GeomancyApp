using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GeomancyWebUI.Client.Models
{
    /// <summary>
    /// Reference data for a single court placement (Right Witness, Left Witness,
    /// Judge, Reconciler) sourced from HouseAndCourtDirectory/CourtData.json via the API.
    /// </summary>
    public class CourtDirectoryEntry
    {
        public int Id { get; set; }
        public string Placement { get; set; } = string.Empty;
        public string TraditionalName { get; set; } = string.Empty;
        public string GeneratedBy { get; set; } = string.Empty;
        public List<string> Meaning { get; set; } = new List<string>();
        public string UtilityInReading { get; set; } = string.Empty;

        [JsonPropertyName("essence")]
        public string Essence { get; set; } = string.Empty;

        [JsonPropertyName("read_when")]
        public List<string> ReadWhen { get; set; } = new List<string>();

        [JsonPropertyName("pitfalls")]
        public List<string> Pitfalls { get; set; } = new List<string>();

        [JsonPropertyName("examples")]
        public List<string> Examples { get; set; } = new List<string>();
    }
}
