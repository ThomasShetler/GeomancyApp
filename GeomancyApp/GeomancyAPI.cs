using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeomancyApp
{
    /// <summary>
    /// Main API class for geomantic calculations and figure management.
    /// Designed to be easily convertible to a DLL for external use.
    /// </summary>
    public static class GeomancyAPI
    {
        /// <summary>
        /// Gets a figure by its name (case-insensitive)
        /// </summary>
        /// <param name="figureName">The name of the figure to retrieve</param>
        /// <returns>FigureData object or null if not found</returns>
        public static FigureData GetFigureByName(string figureName)
        {
            return FigureData.GetFigureByName(figureName);
        }

        /// <summary>
        /// Gets a figure by its elemental pattern using boolean values
        /// </summary>
        /// <param name="fireActive">True if fire element is active (1 dot), false if passive (2 dots)</param>
        /// <param name="airActive">True if air element is active (1 dot), false if passive (2 dots)</param>
        /// <param name="waterActive">True if water element is active (1 dot), false if passive (2 dots)</param>
        /// <param name="earthActive">True if earth element is active (1 dot), false if passive (2 dots)</param>
        /// <returns>FigureData object or null if not found</returns>
        public static FigureData GetFigureByElementalPattern(bool fireActive, bool airActive, bool waterActive, bool earthActive)
        {
            return FigureData.GetFigureByElementalPattern(fireActive, airActive, waterActive, earthActive);
        }

        /// <summary>
        /// Gets a figure by its elemental pattern using integer values
        /// </summary>
        /// <param name="fireValue">1 for active (1 dot), 0 or 2 for passive (2 dots)</param>
        /// <param name="airValue">1 for active (1 dot), 0 or 2 for passive (2 dots)</param>
        /// <param name="waterValue">1 for active (1 dot), 0 or 2 for passive (2 dots)</param>
        /// <param name="earthValue">1 for active (1 dot), 0 or 2 for passive (2 dots)</param>
        /// <returns>FigureData object or null if not found</returns>
        public static FigureData GetFigureByElementalPattern(int fireValue, int airValue, int waterValue, int earthValue)
        {
            return FigureData.GetFigureByElementalPattern(fireValue, airValue, waterValue, earthValue);
        }

        /// <summary>
        /// Gets all available geomantic figures
        /// </summary>
        /// <returns>List of all FigureData objects</returns>
        public static List<FigureData> GetAllFigures()
        {
            return FigureData.GetAllFigures();
        }

        /// <summary>
        /// Creates a new geomantic figure from elemental pattern
        /// </summary>
        /// <param name="headLine">Fire element value (1=active, 2=passive)</param>
        /// <param name="neckLine">Air element value (1=active, 2=passive)</param>
        /// <param name="bodyLine">Water element value (1=active, 2=passive)</param>
        /// <param name="footLine">Earth element value (1=active, 2=passive)</param>
        /// <param name="houseNumber">Optional house number assignment</param>
        /// <returns>GeomanticFigure object</returns>
        public static GeomanticFigure CreateFigure(int headLine, int neckLine, int bodyLine, int footLine, int houseNumber = 0)
        {
            return new GeomanticFigure(headLine, neckLine, bodyLine, footLine, houseNumber);
        }

        /// <summary>
        /// Creates a new geomantic figure from name
        /// </summary>
        /// <param name="figureName">Name of the figure</param>
        /// <param name="houseNumber">Optional house number assignment</param>
        /// <returns>GeomanticFigure object</returns>
        public static GeomanticFigure CreateFigure(string figureName, int houseNumber = 0)
        {
            return new GeomanticFigure(figureName, houseNumber);
        }

        /// <summary>
        /// Creates a new house chart
        /// </summary>
        /// <returns>Empty HouseChart object</returns>
        public static HouseChart CreateChart()
        {
            return new HouseChart();
        }

        /// <summary>
        /// Creates a complete chart from the first four houses
        /// </summary>
        /// <param name="house1Pattern">Elemental pattern for house 1 (fire,air,water,earth)</param>
        /// <param name="house2Pattern">Elemental pattern for house 2 (fire,air,water,earth)</param>
        /// <param name="house3Pattern">Elemental pattern for house 3 (fire,air,water,earth)</param>
        /// <param name="house4Pattern">Elemental pattern for house 4 (fire,air,water,earth)</param>
        /// <returns>Complete HouseChart with all calculations</returns>
        public static HouseChart CreateCompleteChart(int[] house1Pattern, int[] house2Pattern, int[] house3Pattern, int[] house4Pattern)
        {
            var chart = new HouseChart();
            
            chart.SetHouseFigure(1, house1Pattern[0], house1Pattern[1], house1Pattern[2], house1Pattern[3]);
            chart.SetHouseFigure(2, house2Pattern[0], house2Pattern[1], house2Pattern[2], house2Pattern[3]);
            chart.SetHouseFigure(3, house3Pattern[0], house3Pattern[1], house3Pattern[2], house3Pattern[3]);
            chart.SetHouseFigure(4, house4Pattern[0], house4Pattern[1], house4Pattern[2], house4Pattern[3]);
            
            chart.CalculateCompleteChart();
            return chart;
        }

        /// <summary>
        /// Creates a complete chart from figure names
        /// </summary>
        /// <param name="house1Name">Name of figure for house 1</param>
        /// <param name="house2Name">Name of figure for house 2</param>
        /// <param name="house3Name">Name of figure for house 3</param>
        /// <param name="house4Name">Name of figure for house 4</param>
        /// <returns>Complete HouseChart with all calculations</returns>
        public static HouseChart CreateCompleteChart(string house1Name, string house2Name, string house3Name, string house4Name)
        {
            var chart = new HouseChart();
            
            chart.SetHouseFigure(1, house1Name);
            chart.SetHouseFigure(2, house2Name);
            chart.SetHouseFigure(3, house3Name);
            chart.SetHouseFigure(4, house4Name);
            
            chart.CalculateCompleteChart();
            return chart;
        }

        /// <summary>
        /// Gets a simple chart summary as a string
        /// </summary>
        /// <param name="chart">The HouseChart to summarize</param>
        /// <returns>Formatted string summary of the chart</returns>
        public static string GetChartSummary(HouseChart chart)
        {
            return chart?.GetChartSummary() ?? "No chart provided";
        }

        /// <summary>
        /// Validates if a chart is complete (has all required figures)
        /// </summary>
        /// <param name="chart">The HouseChart to validate</param>
        /// <returns>True if chart is complete, false otherwise</returns>
        public static bool IsChartComplete(HouseChart chart)
        {
            return chart?.IsChartComplete() ?? false;
        }

        /// <summary>
        /// Gets the elemental pattern as a string for a given figure
        /// </summary>
        /// <param name="figure">The GeomanticFigure to get pattern for</param>
        /// <returns>String representation of the elemental pattern (e.g., "1-1-2-1")</returns>
        public static string GetElementalPattern(GeomanticFigure figure)
        {
            return figure?.GetElementalPattern() ?? "0-0-0-0";
        }

        /// <summary>
        /// Gets the elemental pattern as boolean array for a given figure
        /// </summary>
        /// <param name="figure">The GeomanticFigure to get pattern for</param>
        /// <returns>Boolean array [fire, air, water, earth] where true = active, false = passive</returns>
        public static bool[] GetElementalPatternAsBools(GeomanticFigure figure)
        {
            return figure?.GetElementalPatternAsBools() ?? new bool[4];
        }

        /// <summary>
        /// Converts a figure name to its elemental pattern
        /// </summary>
        /// <param name="figureName">Name of the figure</param>
        /// <returns>Integer array [fire, air, water, earth] where 1 = active, 2 = passive</returns>
        public static int[] GetElementalPatternFromName(string figureName)
        {
            var figure = GetFigureByName(figureName);
            if (figure == null) return new int[] { 0, 0, 0, 0 };

            return new int[]
            {
                figure.FireElement.Equals("Active", StringComparison.OrdinalIgnoreCase) ? 1 : 2,
                figure.AirElement.Equals("Active", StringComparison.OrdinalIgnoreCase) ? 1 : 2,
                figure.WaterElement.Equals("Active", StringComparison.OrdinalIgnoreCase) ? 1 : 2,
                figure.EarthElement.Equals("Active", StringComparison.OrdinalIgnoreCase) ? 1 : 2
            };
        }

        /// <summary>
        /// Gets a list of all figure names
        /// </summary>
        /// <returns>Array of all figure names</returns>
        public static string[] GetAllFigureNames()
        {
            var figures = GetAllFigures();
            return figures.Select(f => f.Name).ToArray();
        }

        /// <summary>
        /// Gets a list of all figure keywords
        /// </summary>
        /// <returns>Array of all figure keywords</returns>
        public static string[] GetAllFigureKeywords()
        {
            var figures = GetAllFigures();
            return figures.Select(f => f.Keyword).ToArray();
        }

        /// <summary>
        /// Searches for figures by keyword
        /// </summary>
        /// <param name="keyword">Keyword to search for</param>
        /// <returns>List of figures matching the keyword</returns>
        public static List<FigureData> SearchFiguresByKeyword(string keyword)
        {
            var figures = GetAllFigures();
            return figures.Where(f => f.Keyword.Equals(keyword, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        /// <summary>
        /// Gets figures by quality (Mobile or Stable)
        /// </summary>
        /// <param name="quality">Quality to filter by ("Mobile" or "Stable")</param>
        /// <returns>List of figures with the specified quality</returns>
        public static List<FigureData> GetFiguresByQuality(string quality)
        {
            var figures = GetAllFigures();
            return figures.Where(f => f.Quality.Equals(quality, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        /// <summary>
        /// Gets figures by planet
        /// </summary>
        /// <param name="planet">Planet to filter by</param>
        /// <returns>List of figures associated with the specified planet</returns>
        public static List<FigureData> GetFiguresByPlanet(string planet)
        {
            var figures = GetAllFigures();
            return figures.Where(f => f.Planet.Equals(planet, StringComparison.OrdinalIgnoreCase)).ToList();
        }
    }
} 