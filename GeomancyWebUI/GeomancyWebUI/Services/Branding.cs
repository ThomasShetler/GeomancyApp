using GeomancyApp;

namespace GeomancyWebUI.Services;

/// <summary>Static asset URLs for Geofancy branding (cache-busted on version bump).</summary>
public static class Branding
{
    /// <summary>Bump when replacing logo.png without a semver release.</summary>
    private const string LogoRevision = "square";

    public static string LogoUrl => $"/img/logo.png?v={GeofancyVersion.Display}-{LogoRevision}";
    public static string LogoAppleUrl => $"/img/logo-apple.png?v={GeofancyVersion.Display}-{LogoRevision}";
    public static string FaviconUrl => $"/favicon.png?v={GeofancyVersion.Display}-{LogoRevision}";
}
