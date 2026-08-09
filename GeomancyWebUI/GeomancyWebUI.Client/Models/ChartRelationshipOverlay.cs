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
        /// <summary>Greer-style description for tooltips.</summary>
        public string? Description { get; set; }
        /// <summary>Houses strictly between endpoints along the cast arc.</summary>
        public IReadOnlyList<int> IntermediateHouses { get; set; } = Array.Empty<int>();
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
