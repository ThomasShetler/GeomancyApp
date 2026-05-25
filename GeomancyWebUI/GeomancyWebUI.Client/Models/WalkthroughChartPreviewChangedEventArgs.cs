namespace GeomancyWebUI.Client.Models
{
    public sealed class WalkthroughChartPreviewChangedEventArgs
    {
        public WalkthroughMobileDrawerPhase DrawerPhase { get; init; }
        public bool IsMothersStage { get; init; }
        public FigureModel? House1 { get; init; }
        public FigureModel? House2 { get; init; }
        public FigureModel? House3 { get; init; }
        public FigureModel? House4 { get; init; }
        public HouseChartModel? Chart { get; init; }
        public bool ShowCourtFigures { get; init; }
        public IReadOnlySet<int>? VisibleHouseNumbers { get; init; }
        public IReadOnlySet<int>? HighlightedHouseNumbers { get; init; }
        public IReadOnlySet<ChartHighlightCourt>? HighlightedCourt { get; init; }
        public IReadOnlyDictionary<int, int>? HouseHighlightedLines { get; init; }
        public IReadOnlyDictionary<int, int>? HouseRevealLineCounts { get; init; }
        public IReadOnlyDictionary<int, string>? HouseLineHighlightThemes { get; init; }
        public IReadOnlyDictionary<ChartHighlightCourt, int>? CourtHighlightedLines { get; init; }
        public IReadOnlyDictionary<ChartHighlightCourt, int>? CourtRevealLineCounts { get; init; }
        public IReadOnlyDictionary<ChartHighlightCourt, string>? CourtLineHighlightThemes { get; init; }
        public int? JustCompletedHouseNumber { get; init; }
        public bool AllMothersComplete { get; init; }
        public string? StepIndicator { get; init; }
        public string? StepTitle { get; init; }
        public string? StepLead { get; init; }
        public string? StepCallout { get; init; }
        public bool CanStepPrevious { get; init; }
        public bool CanStepNext { get; init; }
        public bool CollapseDrawer { get; init; }
        public bool AwaitingSummaryAck { get; init; }
        public string? NextButtonLabel { get; init; }
        public string? MotherStatusTitle { get; init; }
        public string? MotherStatusDetail { get; init; }
        public string? MotherFigureInsight { get; init; }
        public string? MotherActionLabel { get; init; }
    }
}
