namespace GeomancyWebUI.Client.Models
{
    /// <summary>
    /// One visual link to draw on the 12-house chart (aspect cast or formation path).
    /// </summary>
    public sealed class ChartAspectLink
    {
        public int FromHouse { get; set; }
        public int ToHouse { get; set; }
        public string AspectType { get; set; } = string.Empty;
        public string Direction { get; set; } = string.Empty;
        /// <summary>aspect | path | company-pair</summary>
        public string Kind { get; set; } = "aspect";
        public string? Label { get; set; }
    }

    /// <summary>
    /// Relationship geometry projected onto the house chart for the selected perfection/aspect.
    /// </summary>
    public sealed class ChartRelationshipOverlay
    {
        public IReadOnlyList<ChartAspectLink> Links { get; set; } = Array.Empty<ChartAspectLink>();
        public int QuerentHouse { get; set; }
        public int QuesitedHouse { get; set; }
    }
}
