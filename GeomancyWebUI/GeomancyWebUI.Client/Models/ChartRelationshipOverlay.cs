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
        /// <summary>aspect | path | company-pair | company-pass</summary>
        public string Kind { get; set; } = "aspect";
        public string? Label { get; set; }
        /// <summary>Greer-style description for tooltips.</summary>
        public string? Description { get; set; }
        /// <summary>Houses strictly between endpoints along the cast arc.</summary>
        public IReadOnlyList<int> IntermediateHouses { get; set; } = Array.Empty<int>();

        /// <summary>querent | quesited | neutral — colors path strokes for Qrt/Qst.</summary>
        public string Role { get; set; } = "neutral";

        /// <summary>aspect | mode | company | empty</summary>
        public string IconKind { get; set; } = string.Empty;

        /// <summary>Matches PerfectionIconHelper variants (sextile, translation, demisimple, …).</summary>
        public string IconVariant { get; set; } = string.Empty;
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
