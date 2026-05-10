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
    }
}
