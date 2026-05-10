namespace GeomancyWebUI.Client.Models
{
    /// <summary>
    /// Identifies which list a row belongs to in the revamped Perfections tab.
    /// </summary>
    public enum PerfectionEntryKind
    {
        Perfection,
        Denial,
        PositiveAspect,
        NegativeAspect
    }

    /// <summary>
    /// Snapshot of a row click in the Perfections tab. Either <see cref="Perfection"/>
    /// or <see cref="Aspect"/> is populated depending on <see cref="Kind"/>.
    /// <see cref="Index"/> is the position within its source list (so two perfections
    /// with the same mode/houses can still be distinguished for selection state).
    /// </summary>
    public record PerfectionSelection(
        PerfectionEntryKind Kind,
        int Index,
        PerfectionModel? Perfection,
        AspectRecordModel? Aspect);
}
