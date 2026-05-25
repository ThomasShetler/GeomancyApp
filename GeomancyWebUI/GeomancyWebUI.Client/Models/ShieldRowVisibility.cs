namespace GeomancyWebUI.Client.Models
{
    /// <summary>
    /// Which shield chart rows ChartSurface renders (wiki walkthrough).
    /// </summary>
    public enum ShieldRowVisibility
    {
        /// <summary>Top row only — Daughters 5–8 and Mothers 1–4.</summary>
        MothersRowOnly,

        /// <summary>Full shield: Mothers/Daughters, Nieces, and Court.</summary>
        All
    }
}
