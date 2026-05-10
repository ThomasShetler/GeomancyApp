namespace GeomancyWebUI.Client.Models
{
    /// <summary>
    /// Reference data for a single Way Of Points element (Fire/Air/Water/Earth)
    /// sourced from the WayOfPointsDirectory/ElementData.json file via the API.
    /// </summary>
    public class WayOfPointsElementEntry
    {
        public int Id { get; set; }
        public string Element { get; set; } = string.Empty;
        public string LatinName { get; set; } = string.Empty;
        public string LineName { get; set; } = string.Empty;
        public int LineIndex { get; set; }
        public string Glyph { get; set; } = string.Empty;
        public string ColorHint { get; set; } = string.Empty;
        public string Quality { get; set; } = string.Empty;
        public string Polarity { get; set; } = string.Empty;
        public string Tagline { get; set; } = string.Empty;
        public string Domain { get; set; } = string.Empty;
        public string WhenEstablished { get; set; } = string.Empty;
        public string WhenNotEstablished { get; set; } = string.Empty;
        public string EndpointHouseEmphasis { get; set; } = string.Empty;
        public List<string> InterpretationParagraphs { get; set; } = new List<string>();
    }
}
