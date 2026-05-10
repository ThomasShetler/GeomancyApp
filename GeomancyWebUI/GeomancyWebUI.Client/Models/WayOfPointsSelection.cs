namespace GeomancyWebUI.Client.Models
{
    /// <summary>
    /// Identifies the entry the user picked in the Way Of Points list panel
    /// so the right-side detail panel can render the corresponding view.
    /// </summary>
    /// <param name="ElementId">The element name: "Fire" | "Air" | "Water" | "Earth".</param>
    /// <param name="Way">The full WayOfPointsResultModel for the selected element.</param>
    /// <param name="Path">
    /// The specific path inside that Way that was clicked, or <c>null</c> when the
    /// user picked the element-summary header row (no specific path selected).
    /// </param>
    /// <param name="PathIndex">
    /// Zero-based index of <see cref="Path"/> within <see cref="WayOfPointsResultModel.AllPaths"/>,
    /// or -1 when <see cref="Path"/> is <c>null</c>.
    /// </param>
    public record WayOfPointsSelection(
        string ElementId,
        WayOfPointsResultModel Way,
        WayOfPointsPathModel? Path,
        int PathIndex);
}
