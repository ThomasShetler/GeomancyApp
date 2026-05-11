namespace GeomancyApp
{
    /// <summary>
    /// Single runtime source of truth for the Geofancy product version
    /// surfaced in any UI (home page version chip, future About box,
    /// release-notes section heading, footer credits, etc).
    ///
    /// Kept as plain consts so consumers - both the .NET 8 Blazor app and
    /// the .NET Framework 4.8 WinForms client - can use the values without
    /// reflection or runtime cost.
    ///
    /// *** When bumping the version, edit ALL THREE places: ***
    ///   1) Geomancy.Core/GeofancyVersion.cs   (this file: Display, ReleaseDate)
    ///   2) Directory.Build.props              (.&lt;Version&gt;, &lt;AssemblyVersion&gt;, ...)
    ///   3) GeomancyApp\Properties\AssemblyInfo.cs +
    ///      GeomancyAPI\Properties\AssemblyInfo.cs +
    ///      GeomancyUnitTesting\Properties\AssemblyInfo.cs
    /// </summary>
    public static class GeofancyVersion
    {
        /// <summary>
        /// User-facing semver string (e.g. "0.1.0-beta", "1.0.0", "1.2.0-rc.1").
        /// Use this for any UI label that should read like marketing copy.
        /// </summary>
        public const string Display = "0.2.2";

        /// <summary>
        /// Human-readable release date for the current Display version.
        /// Used by the release-notes section on the landing page.
        /// </summary>
        public const string ReleaseDate = "May 2026";

        /// <summary>
        /// SemVer release "channel" derived from the suffix on Display.
        /// Useful for pre-release UI affordances (badges, banners).
        /// </summary>
        public const string Channel = "Stable";
    }
}
