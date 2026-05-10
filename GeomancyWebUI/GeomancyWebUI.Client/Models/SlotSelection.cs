namespace GeomancyWebUI.Client.Models
{
    /// <summary>
    /// Identifies the kind of slot the user clicked in the Court & Houses table.
    /// </summary>
    public enum SlotKind
    {
        House,
        RightWitness,
        LeftWitness,
        Judge,
        Sentence
    }

    /// <summary>
    /// Snapshot of a user's row click on the Court & Houses tab. Carries the
    /// occupant figure so consumers (e.g. FigureDetailPanel) don't need to
    /// re-query the chart model.
    /// </summary>
    public record SlotSelection(SlotKind Kind, int? HouseNumber, string SlotLabel, FigureModel? Figure);
}
