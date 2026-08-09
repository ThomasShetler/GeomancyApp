using System;

namespace GeomancyApp
{
    /// <summary>
    /// Formats relationship-path fields for perfection list rows (mechanism geometry, not Q/X alone).
    /// </summary>
    public static class PerfectionPathDisplay
    {
        public sealed class ListRowPath
        {
            public int FromHouse { get; set; }
            public int ToHouse { get; set; }
            public string FromFigure { get; set; } = string.Empty;
            public string ToFigure { get; set; } = string.Empty;
            public string ActorPrefix { get; set; } = string.Empty;
            public bool HasPath => FromHouse > 0 && ToHouse > 0;
        }

        public static ListRowPath ForListRow(PerfectionResult result)
        {
            if (result == null)
                return new ListRowPath();

            if (result.PathFromHouse > 0 && result.PathToHouse > 0)
            {
                return new ListRowPath
                {
                    FromHouse = result.PathFromHouse,
                    ToHouse = result.PathToHouse,
                    FromFigure = result.PathFigure ?? string.Empty,
                    ToFigure = result.PathSecondaryFigure ?? string.Empty,
                    ActorPrefix = result.PathActor ?? string.Empty
                };
            }

            // Aspect fallback when path not set but cast houses exist
            if (result.AspectFromHouse > 0 && result.AspectToHouse > 0)
            {
                return new ListRowPath
                {
                    FromHouse = result.AspectFromHouse,
                    ToHouse = result.AspectToHouse,
                    FromFigure = result.PathFigure ?? string.Empty,
                    ToFigure = string.Empty,
                    ActorPrefix = string.Empty
                };
            }

            return new ListRowPath();
        }

        public static ListRowPath ForListRow(
            int pathFromHouse,
            int pathToHouse,
            string pathFigure,
            string pathSecondaryFigure,
            string pathActor,
            int aspectFromHouse = 0,
            int aspectToHouse = 0)
        {
            if (pathFromHouse > 0 && pathToHouse > 0)
            {
                return new ListRowPath
                {
                    FromHouse = pathFromHouse,
                    ToHouse = pathToHouse,
                    FromFigure = pathFigure ?? string.Empty,
                    ToFigure = pathSecondaryFigure ?? string.Empty,
                    ActorPrefix = pathActor ?? string.Empty
                };
            }

            if (aspectFromHouse > 0 && aspectToHouse > 0)
            {
                return new ListRowPath
                {
                    FromHouse = aspectFromHouse,
                    ToHouse = aspectToHouse,
                    FromFigure = pathFigure ?? string.Empty,
                    ToFigure = string.Empty,
                    ActorPrefix = string.Empty
                };
            }

            return new ListRowPath();
        }

        /// <summary>
        /// Human-readable flow string for tests / debugging, e.g. "Qst. H5 → H12 · Laetitia".
        /// </summary>
        public static string FormatFlow(ListRowPath path)
        {
            if (path == null || !path.HasPath)
                return string.Empty;

            var prefix = string.IsNullOrEmpty(path.ActorPrefix) ? string.Empty : path.ActorPrefix + " ";
            var flow = $"{prefix}H{path.FromHouse} → H{path.ToHouse}";

            if (!string.IsNullOrEmpty(path.FromFigure) && !string.IsNullOrEmpty(path.ToFigure))
                return $"{flow} · {path.FromFigure} · {path.ToFigure}";
            if (!string.IsNullOrEmpty(path.FromFigure))
                return $"{flow} · {path.FromFigure}";
            if (!string.IsNullOrEmpty(path.ToFigure))
                return $"{flow} · {path.ToFigure}";
            return flow;
        }

        public static string FormatFlow(PerfectionResult result) => FormatFlow(ForListRow(result));
    }
}
