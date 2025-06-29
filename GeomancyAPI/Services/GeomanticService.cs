using GeomancyApp;
using GeomancyAPI.Models;

namespace GeomancyAPI.Services
{
    /// <summary>
    /// Service for handling geomantic calculations and figure generation
    /// </summary>
    public class GeomanticService
    {
        /// <summary>
        /// Generates a single geomantic figure from elemental pattern
        /// </summary>
        public FigureResponse GenerateFigure(GenerateFigureRequest request)
        {
            var figure = new GeomanticFigure(
                request.FireElement, 
                request.AirElement, 
                request.WaterElement, 
                request.EarthElement);

            return MapToFigureResponse(figure);
        }

        /// <summary>
        /// Generates four geomantic figures
        /// </summary>
        public FourFiguresResponse GenerateFourFigures(GenerateFourFiguresRequest request)
        {
            return new FourFiguresResponse
            {
                Figure1 = GenerateFigure(request.Figure1),
                Figure2 = GenerateFigure(request.Figure2),
                Figure3 = GenerateFigure(request.Figure3),
                Figure4 = GenerateFigure(request.Figure4)
            };
        }

        /// <summary>
        /// Generates a complete house chart from the first four houses
        /// </summary>
        public HouseChartResponse GenerateHouseChart(GenerateHouseChartRequest request)
        {
            var houseChart = new HouseChart();
            
            // Set the first four houses and calculate the rest
            houseChart.SetFirstFourHousesAndCalculate(
                request.House1.FireElement, request.House1.AirElement, request.House1.WaterElement, request.House1.EarthElement,
                request.House2.FireElement, request.House2.AirElement, request.House2.WaterElement, request.House2.EarthElement,
                request.House3.FireElement, request.House3.AirElement, request.House3.WaterElement, request.House3.EarthElement,
                request.House4.FireElement, request.House4.AirElement, request.House4.WaterElement, request.House4.EarthElement);

            return MapToHouseChartResponse(houseChart);
        }

        /// <summary>
        /// Gets all available geomantic figures
        /// </summary>
        public List<FigureResponse> GetAllFigures()
        {
            var figures = FigureData.GetAllFigures();
            return figures.Select(MapToFigureResponse).ToList();
        }

        /// <summary>
        /// Gets a figure by name
        /// </summary>
        public FigureResponse? GetFigureByName(string name)
        {
            var figure = FigureData.GetFigureByName(name);
            return figure != null ? MapToFigureResponse(figure) : null;
        }

        /// <summary>
        /// Maps a GeomanticFigure to FigureResponse
        /// </summary>
        private FigureResponse MapToFigureResponse(GeomanticFigure figure)
        {
            return new FigureResponse
            {
                Name = figure.Name,
                OtherNames = figure.OtherNames,
                Quality = figure.Quality,
                Keyword = figure.Keyword,
                Imagery = figure.Imagery,
                StrongHouse = figure.StrongHouse,
                WeakHouse = figure.WeakHouse,
                Planet = figure.Planet,
                Sign = figure.Sign,
                InnerElement = figure.InnerEl,
                OuterElement = figure.OuterEl,
                FireElement = figure.FireElement,
                AirElement = figure.AirElement,
                WaterElement = figure.WaterElement,
                EarthElement = figure.EarthElement,
                Anatomy = figure.Anatomy,
                BodyType = figure.BodyType,
                CharacterType = figure.CharacterType,
                Colors = figure.Colors,
                Commentary = figure.Commentary,
                DivinatoryMeaning = figure.DivinatoryMeaning,
                ElementalPattern = figure.GetElementalPattern(),
                HouseNumber = figure.HouseNumber
            };
        }

        /// <summary>
        /// Maps a HouseChart to HouseChartResponse
        /// </summary>
        private HouseChartResponse MapToHouseChartResponse(HouseChart houseChart)
        {
            var response = new HouseChartResponse
            {
                Houses = new Dictionary<string, FigureResponse>(),
                IsComplete = houseChart.IsChartComplete(),
                ChartSummary = houseChart.GetChartSummary()
            };

            // Add all houses
            for (int i = 1; i <= 12; i++)
            {
                var house = houseChart.GetHouseFigure(i);
                if (house != null)
                {
                    response.Houses[$"House{i}"] = MapToFigureResponse(house);
                }
            }

            // Add witnesses, judge, and sentence
            if (houseChart.RightWitness != null)
                response.RightWitness = MapToFigureResponse(houseChart.RightWitness);
            
            if (houseChart.LeftWitness != null)
                response.LeftWitness = MapToFigureResponse(houseChart.LeftWitness);
            
            if (houseChart.Judge != null)
                response.Judge = MapToFigureResponse(houseChart.Judge);
            
            if (houseChart.Sentence != null)
                response.Sentence = MapToFigureResponse(houseChart.Sentence);

            return response;
        }
    }
} 