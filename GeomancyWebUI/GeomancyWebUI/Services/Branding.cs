using GeomancyApp;

namespace GeomancyWebUI.Services;

/// <summary>Static asset URLs for Geofancy branding (cache-busted on version bump).</summary>
public static class Branding
{
    public static string LogoUrl => $"/img/logo.png?v={GeofancyVersion.Display}";
    public static string LogoAppleUrl => $"/img/logo-apple.png?v={GeofancyVersion.Display}";
    public static string FaviconUrl => $"/favicon.png?v={GeofancyVersion.Display}";
}
