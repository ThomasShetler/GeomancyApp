using System;

namespace GeomancyWebUI.Client.Helpers
{
    /// <summary>
    /// SVG path markup for house-chart connector badges (mirrors PerfectionIcon glyphs).
    /// Returns inner elements only — caller wraps in a 16×16 viewBox svg.
    /// </summary>
    public static class ChartLinkIconGlyphs
    {
        public static string? TryGetInnerMarkup(string? iconKind, string? iconVariant)
        {
            if (string.IsNullOrWhiteSpace(iconKind))
                return null;

            var kind = iconKind.Trim().ToLowerInvariant();
            var variant = string.IsNullOrWhiteSpace(iconVariant)
                ? "generic"
                : iconVariant.Trim().ToLowerInvariant();

            return kind switch
            {
                "aspect" => AspectInner(variant),
                "mode" => ModeInner(variant),
                "company" => CompanyInner(variant),
                _ => null
            };
        }

        private static string AspectInner(string variant) => variant switch
        {
            "sextile" =>
                "<path d=\"M8 2L13.2 5V11L8 14L2.8 11V5L8 2Z\" stroke=\"currentColor\" stroke-width=\"1.4\" stroke-linejoin=\"round\" fill=\"none\"/>" +
                "<path d=\"M8 2V14M2.8 5L13.2 11M13.2 5L2.8 11\" stroke=\"currentColor\" stroke-width=\"1.1\" opacity=\"0.85\" fill=\"none\"/>",
            "trine" =>
                "<path d=\"M8 2.5L13.5 12H2.5L8 2.5Z\" stroke=\"currentColor\" stroke-width=\"1.4\" stroke-linejoin=\"round\" fill=\"none\"/>" +
                "<circle cx=\"8\" cy=\"8.5\" r=\"1.1\" fill=\"currentColor\"/>",
            "square" =>
                "<rect x=\"3.5\" y=\"3.5\" width=\"9\" height=\"9\" stroke=\"currentColor\" stroke-width=\"1.4\" rx=\"0.5\" fill=\"none\"/>" +
                "<path d=\"M3.5 3.5L12.5 12.5M12.5 3.5L3.5 12.5\" stroke=\"currentColor\" stroke-width=\"1.1\" opacity=\"0.85\" fill=\"none\"/>",
            "opposition" =>
                "<circle cx=\"4.5\" cy=\"8\" r=\"2.2\" stroke=\"currentColor\" stroke-width=\"1.3\" fill=\"none\"/>" +
                "<circle cx=\"11.5\" cy=\"8\" r=\"2.2\" stroke=\"currentColor\" stroke-width=\"1.3\" fill=\"none\"/>" +
                "<path d=\"M6.7 8H9.3\" stroke=\"currentColor\" stroke-width=\"1.4\" stroke-linecap=\"round\" fill=\"none\"/>",
            "conjunction" =>
                "<circle cx=\"8\" cy=\"8\" r=\"4.5\" stroke=\"currentColor\" stroke-width=\"1.4\" fill=\"none\"/>" +
                "<circle cx=\"8\" cy=\"8\" r=\"1.4\" fill=\"currentColor\"/>",
            _ =>
                "<circle cx=\"8\" cy=\"8\" r=\"4.5\" stroke=\"currentColor\" stroke-width=\"1.3\" stroke-dasharray=\"2 1.5\" fill=\"none\"/>"
        };

        private static string ModeInner(string variant) => variant switch
        {
            "occupation" =>
                "<path d=\"M3 7.5V13H13V7.5L8 4L3 7.5Z\" stroke=\"currentColor\" stroke-width=\"1.3\" stroke-linejoin=\"round\" fill=\"none\"/>" +
                "<path d=\"M6 13V9.5H10V13\" stroke=\"currentColor\" stroke-width=\"1.2\" fill=\"none\"/>" +
                "<circle cx=\"8\" cy=\"8.2\" r=\"1.1\" fill=\"currentColor\"/>",
            "conjunction" =>
                "<circle cx=\"6.2\" cy=\"8\" r=\"2.8\" stroke=\"currentColor\" stroke-width=\"1.3\" fill=\"none\"/>" +
                "<circle cx=\"9.8\" cy=\"8\" r=\"2.8\" stroke=\"currentColor\" stroke-width=\"1.3\" fill=\"none\"/>" +
                "<path d=\"M7.5 8H8.5\" stroke=\"currentColor\" stroke-width=\"1.4\" stroke-linecap=\"round\" fill=\"none\"/>",
            "translation" =>
                "<circle cx=\"3.5\" cy=\"8\" r=\"1.6\" stroke=\"currentColor\" stroke-width=\"1.2\" fill=\"none\"/>" +
                "<circle cx=\"8\" cy=\"8\" r=\"2.2\" fill=\"currentColor\" fill-opacity=\"0.25\" stroke=\"currentColor\" stroke-width=\"1.3\"/>" +
                "<circle cx=\"12.5\" cy=\"8\" r=\"1.6\" stroke=\"currentColor\" stroke-width=\"1.2\" fill=\"none\"/>" +
                "<path d=\"M5.1 8H5.8M10.2 8H10.9\" stroke=\"currentColor\" stroke-width=\"1.3\" stroke-linecap=\"round\" fill=\"none\"/>",
            "mutation" =>
                "<path d=\"M4 5.5L7 8.5L4 11.5\" stroke=\"currentColor\" stroke-width=\"1.3\" stroke-linecap=\"round\" stroke-linejoin=\"round\" fill=\"none\"/>" +
                "<path d=\"M12 5.5L9 8.5L12 11.5\" stroke=\"currentColor\" stroke-width=\"1.3\" stroke-linecap=\"round\" stroke-linejoin=\"round\" fill=\"none\"/>" +
                "<path d=\"M7.5 4.5L8.5 11.5\" stroke=\"currentColor\" stroke-width=\"1.2\" stroke-linecap=\"round\" opacity=\"0.7\" fill=\"none\"/>",
            _ =>
                "<rect x=\"3\" y=\"3\" width=\"10\" height=\"10\" rx=\"1.5\" stroke=\"currentColor\" stroke-width=\"1.3\" fill=\"none\"/>"
        };

        private static string CompanyInner(string variant)
        {
            var core =
                "<rect x=\"2.5\" y=\"5\" width=\"4.5\" height=\"6\" rx=\"0.8\" stroke=\"currentColor\" stroke-width=\"1.2\" fill=\"none\"/>" +
                "<rect x=\"9\" y=\"5\" width=\"4.5\" height=\"6\" rx=\"0.8\" stroke=\"currentColor\" stroke-width=\"1.2\" fill=\"none\"/>" +
                "<path d=\"M7 8H9\" stroke=\"currentColor\" stroke-width=\"1.3\" stroke-linecap=\"round\" fill=\"none\"/>";

            return variant switch
            {
                "simple" => core + "<circle cx=\"8\" cy=\"8\" r=\"1\" fill=\"currentColor\"/>",
                "demisimple" => core +
                    "<path d=\"M7.2 7.2L8.8 8.8M8.8 7.2L7.2 8.8\" stroke=\"currentColor\" stroke-width=\"1.1\" stroke-linecap=\"round\" fill=\"none\"/>",
                "compound" => core +
                    "<path d=\"M7 6.5L9 9.5M9 6.5L7 9.5\" stroke=\"currentColor\" stroke-width=\"1.1\" stroke-linecap=\"round\" fill=\"none\"/>",
                "capitular" => core +
                    "<path d=\"M6.5 8H9.5\" stroke=\"currentColor\" stroke-width=\"1.4\" stroke-linecap=\"round\" fill=\"none\"/>" +
                    "<path d=\"M8 6.5V9.5\" stroke=\"currentColor\" stroke-width=\"1.2\" stroke-linecap=\"round\" opacity=\"0.7\" fill=\"none\"/>",
                _ => core
            };
        }

        public static string NormalizeCompanyVariant(string? companyType) =>
            companyType?.Trim().ToLowerInvariant() switch
            {
                "simple" => "simple",
                "demisimple" => "demisimple",
                "compound" => "compound",
                "capitular" => "capitular",
                _ => "generic"
            };
    }
}
