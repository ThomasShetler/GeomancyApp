namespace GeomancyWebUI.Client.Models
{
    /// <summary>
    /// Reference data for a single Way Of Points path-type classification
    /// (Strong / StrongPassive / WeakerPassive / Passive) sourced from the
    /// WayOfPointsDirectory/PathTypeData.json file via the API.
    /// </summary>
    public class WayOfPointsPathTypeEntry
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Glyph { get; set; } = string.Empty;
        public string ColorHint { get; set; } = string.Empty;
        public string BadgeClass { get; set; } = string.Empty;
        public string Tagline { get; set; } = string.Empty;
        public string MechanismSummary { get; set; } = string.Empty;
        public string CoReads { get; set; } = string.Empty;
        public List<string> InterpretationParagraphs { get; set; } = new List<string>();
    }
}
