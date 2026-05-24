using System;
using System.Collections.Generic;
using System.Linq;
using GeomancyWebUI.Client.Models;

namespace GeomancyWebUI.Client.Helpers
{
    public static class WikiFigureSlug
    {
        public static string ToSlug(string figureName)
        {
            if (string.IsNullOrWhiteSpace(figureName)) return string.Empty;
            return figureName.Trim().ToLowerInvariant().Replace(" ", "-", StringComparison.Ordinal);
        }

        public static FigureModel? Resolve(IReadOnlyList<FigureModel> figures, string? slug)
        {
            if (figures == null || string.IsNullOrWhiteSpace(slug)) return null;
            var normalized = slug.Trim().ToLowerInvariant();
            return figures.FirstOrDefault(f =>
                string.Equals(ToSlug(f.Name), normalized, StringComparison.Ordinal)
                || string.Equals(f.Name, slug, StringComparison.OrdinalIgnoreCase));
        }
    }
}
