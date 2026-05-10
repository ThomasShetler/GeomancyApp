namespace GeomancyWebUI.Services;

public class ThemeService
{
    private bool _isDarkMode = false;

    public bool IsDarkMode
    {
        get => _isDarkMode;
        private set
        {
            if (_isDarkMode != value)
            {
                _isDarkMode = value;
                ThemeChanged?.Invoke(value);
            }
        }
    }

    public event Action<bool>? ThemeChanged;

    public void Initialize(bool isDarkMode)
    {
        _isDarkMode = isDarkMode;
        ThemeChanged?.Invoke(isDarkMode);
    }

    public void ToggleTheme()
    {
        IsDarkMode = !IsDarkMode;
    }

    public void SetTheme(bool isDarkMode)
    {
        IsDarkMode = isDarkMode;
    }

    /// <summary>
    /// Sync C# state from the DOM after theme.js has applied the real theme
    /// (initial load or after an atomic JS toggle). Avoids double-click bugs where
    /// C# thought light while data-theme was already dark.
    /// </summary>
    public void SetFromJs(bool isDarkMode)
    {
        if (_isDarkMode == isDarkMode)
            return;

        _isDarkMode = isDarkMode;
        ThemeChanged?.Invoke(isDarkMode);
    }
}

