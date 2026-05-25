namespace GeomancyWebUI.Client.Models
{
    public sealed class MothersPreviewChangedEventArgs
    {
        public FigureModel? House1 { get; init; }
        public FigureModel? House2 { get; init; }
        public FigureModel? House3 { get; init; }
        public FigureModel? House4 { get; init; }
        public IReadOnlySet<int>? HighlightedHouseNumbers { get; init; }
        public int? JustCompletedHouseNumber { get; init; }
        public bool AllMothersComplete { get; init; }
        public string? MotherStatusTitle { get; init; }
        public string? MotherStatusDetail { get; init; }
        public string? MotherFigureInsight { get; init; }
        public string? MotherActionLabel { get; init; }
    }
}
