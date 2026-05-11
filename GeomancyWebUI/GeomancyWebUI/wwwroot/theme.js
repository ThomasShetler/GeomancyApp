(function () {
    window.geofancyTheme = {
        get: function () {
            return document.documentElement.getAttribute('data-theme') === 'dark';
        },
        set: function (isDark) {
            var theme = isDark ? 'dark' : 'light';
            document.documentElement.setAttribute('data-theme', theme);
            try {
                localStorage.setItem('theme', theme);
            } catch (e) { /* ignore */ }
        },
        toggle: function () {
            var next = !this.get();
            this.set(next);
            return next;
        },
        applyInitial: function () {
            var stored = null;
            try {
                stored = localStorage.getItem('theme');
            } catch (e) { /* ignore */ }
            var prefersDark = window.matchMedia &&
                window.matchMedia('(prefers-color-scheme: dark)').matches;
            var theme = stored || (prefersDark ? 'dark' : 'light');
            document.documentElement.setAttribute('data-theme', theme);
        }
    };

    // Viewport cap for "phone-style" routing only. 900px was too wide: tiled
    // desktop windows and many laptops read as mobile and hit /mobile + Cast flow.
    var MOBILE_MAX_WIDTH_PX = 768;
    window.geofancyDevice = {
        isMobile: function () {
            return window.matchMedia &&
                window.matchMedia('(max-width: ' + MOBILE_MAX_WIDTH_PX + 'px)').matches;
        }
    };

    // Flat entry points for Blazor JS interop (InvokeAsync<string>(identifier)).
    window.geofancyGetThemeIsDark = function () {
        return window.geofancyTheme.get();
    };
    window.geofancyToggleTheme = function () {
        return window.geofancyTheme.toggle();
    };
    window.geofancyDeviceIsMobile = function () {
        return window.geofancyDevice.isMobile();
    };
})();
