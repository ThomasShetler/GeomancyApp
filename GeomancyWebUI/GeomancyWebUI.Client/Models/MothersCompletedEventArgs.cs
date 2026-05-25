namespace GeomancyWebUI.Client.Models
{
    public sealed class MothersCompletedEventArgs
    {
        public FigureModel House1 { get; init; } = new();
        public FigureModel House2 { get; init; } = new();
        public FigureModel House3 { get; init; } = new();
        public FigureModel House4 { get; init; } = new();
    }
}
