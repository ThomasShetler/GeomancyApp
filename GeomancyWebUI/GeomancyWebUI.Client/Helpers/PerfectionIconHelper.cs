using System;

namespace GeomancyWebUI.Client.Helpers
{
    public enum PerfectionIconKind
    {
        Aspect,
        Mode,
        Company,
        Denial
    }

    /// <summary>
    /// Maps perfection modes, aspects, and company types to icon variants and CSS tint classes.
    /// </summary>
    public static class PerfectionIconHelper
    {
        public static PerfectionIconKind ResolveKind(string? mode, string? aspectType)
        {
            if (!string.IsNullOrEmpty(mode))
            {
                if (mode.Equals("Company", StringComparison.OrdinalIgnoreCase))
                    return PerfectionIconKind.Company;
                if (mode.Equals("None", StringComparison.OrdinalIgnoreCase)
                    || mode.Equals("Impedition", StringComparison.OrdinalIgnoreCase))
                    return PerfectionIconKind.Denial;
                if (mode.Equals("Aspect", StringComparison.OrdinalIgnoreCase))
                    return PerfectionIconKind.Aspect;
            }

            if (!string.IsNullOrEmpty(aspectType)
                && !aspectType.Equals("None", StringComparison.OrdinalIgnoreCase))
                return PerfectionIconKind.Aspect;

            if (!string.IsNullOrEmpty(mode))
                return PerfectionIconKind.Mode;

            return PerfectionIconKind.Mode;
        }

        public static string ResolveVariant(string? mode, string? aspectType, string? companyType)
        {
            if (!string.IsNullOrEmpty(mode))
            {
                if (mode.Equals("Company", StringComparison.OrdinalIgnoreCase))
                    return NormalizeCompanyType(companyType);

                if (mode.Equals("Aspect", StringComparison.OrdinalIgnoreCase)
                    && !string.IsNullOrEmpty(aspectType)
                    && !aspectType.Equals("None", StringComparison.OrdinalIgnoreCase))
                    return aspectType.Trim().ToLowerInvariant();

                if (mode.Equals("None", StringComparison.OrdinalIgnoreCase)
                    || mode.Equals("Impedition", StringComparison.OrdinalIgnoreCase))
                    return "impedition";

                return mode.Trim().ToLowerInvariant();
            }

            if (!string.IsNullOrEmpty(aspectType) && !aspectType.Equals("None", StringComparison.OrdinalIgnoreCase))
                return aspectType.Trim().ToLowerInvariant();

            return "generic";
        }

        public static string CssClass(string? mode, string? aspectType, string? companyType)
        {
            var kind = ResolveKind(mode, aspectType);
            var variant = ResolveVariant(mode, aspectType, companyType);

            return kind switch
            {
                PerfectionIconKind.Aspect => AspectDirectionHelper.AspectTypeClass(variant),
                PerfectionIconKind.Company => $"icon-company icon-company-{variant}",
                PerfectionIconKind.Denial => "icon-denial",
                _ => $"icon-mode icon-mode-{variant}"
            };
        }

        public static string AspectCssClass(string? aspectType) =>
            AspectDirectionHelper.AspectTypeClass(aspectType);

        private static string NormalizeCompanyType(string? companyType) => companyType?.Trim().ToLowerInvariant() switch
        {
            "simple" => "simple",
            "demisimple" => "demisimple",
            "compound" => "compound",
            "capitular" => "capitular",
            _ => "generic"
        };
    }
}
