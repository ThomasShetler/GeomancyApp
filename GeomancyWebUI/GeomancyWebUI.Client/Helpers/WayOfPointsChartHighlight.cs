using System.Collections.Generic;
using GeomancyWebUI.Client.Models;

namespace GeomancyWebUI.Client.Helpers
{
    /// <summary>
    /// Maps a Way Of Points list selection onto shield-chart highlight seats
    /// (houses 1–12 plus Judge / Witnesses).
    /// </summary>
    public static class WayOfPointsChartHighlight
    {
        /// <summary>
        /// Path houses use 0 = Judge, 13 = Right Witness, 14 = Left Witness, 1–12 = houses.
        /// Judge is always included when any path is highlighted (paths start there even when
        /// the API list omits house 0).
        /// </summary>
        public static void Apply(
            WayOfPointsSelection? selection,
            out HashSet<int> houses,
            out HashSet<ChartHighlightCourt> court)
        {
            houses = new HashSet<int>();
            court = new HashSet<ChartHighlightCourt>();
            if (selection == null)
                return;

            IEnumerable<WayOfPointsPathModel> paths;
            if (selection.Path != null)
                paths = new[] { selection.Path };
            else if (selection.Way?.AllPaths is { Count: > 0 } all)
                paths = all;
            else
                return;

            foreach (var path in paths)
                AddPath(path, houses, court);
        }

        private static void AddPath(
            WayOfPointsPathModel path,
            HashSet<int> houses,
            HashSet<ChartHighlightCourt> court)
        {
            court.Add(ChartHighlightCourt.Judge);

            if (path.Houses != null)
            {
                foreach (var h in path.Houses)
                    AddSeat(h, houses, court);
            }

            if (path.EndpointHouse is >= 1 and <= 12)
                houses.Add(path.EndpointHouse);
        }

        private static void AddSeat(int seat, HashSet<int> houses, HashSet<ChartHighlightCourt> court)
        {
            if (seat is >= 1 and <= 12)
            {
                houses.Add(seat);
                return;
            }

            switch (seat)
            {
                case 0:
                    court.Add(ChartHighlightCourt.Judge);
                    break;
                case 13:
                    court.Add(ChartHighlightCourt.RightWitness);
                    break;
                case 14:
                    court.Add(ChartHighlightCourt.LeftWitness);
                    break;
            }
        }
    }
}
