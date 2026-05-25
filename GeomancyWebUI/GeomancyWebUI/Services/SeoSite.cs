namespace GeomancyWebUI.Services;

/// <summary>
/// Central SEO constants and URL helpers for Geofancy public pages.
/// </summary>
public static class SeoSite
{
    public const string SiteName = "Geofancy";
    public const string SiteUrl = "https://geofancy.up.railway.app";
    public const string Locale = "en_US";
    public const string Author = "Thomas Shetler";
    public const string TwitterCard = "summary_large_image";
    public const string DefaultOgImagePath = "/img/logo.png";

    public const string DefaultDescription =
        "Free geomancy software for shield and house charts, perfections, Way of Points, a live wiki with sixteen figures, and interactive casting walkthroughs.";

    public const string DefaultKeywords =
        "geomancy, geofancy, geomantic chart, shield chart, house chart, perfections, way of points, via puncti, four mothers, divination, John Michael Greer, Sam Block";

    public static string AbsoluteUrl(string? relativePath = null)
    {
        if (string.IsNullOrWhiteSpace(relativePath))
            return SiteUrl;

        if (relativePath.StartsWith("http://", StringComparison.OrdinalIgnoreCase)
            || relativePath.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
            return relativePath;

        var path = relativePath.StartsWith('/') ? relativePath : "/" + relativePath;
        return SiteUrl.TrimEnd('/') + path;
    }

    public static string FormatTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            return SiteName;

        if (title.Contains(SiteName, StringComparison.OrdinalIgnoreCase))
            return title.Trim();

        return $"{title.Trim()} | {SiteName}";
    }

    public static string Truncate(string? text, int maxLength = 160)
    {
        if (string.IsNullOrWhiteSpace(text))
            return DefaultDescription;

        var trimmed = text.Trim();
        if (trimmed.Length <= maxLength)
            return trimmed;

        return trimmed[..(maxLength - 1)].TrimEnd() + "…";
    }
}
