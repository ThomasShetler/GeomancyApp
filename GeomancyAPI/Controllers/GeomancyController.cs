using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using GeomancyAPI.Models;
using GeomancyAPI.Services;
using GeomancyApp;

namespace GeomancyAPI.Controllers
{
    /// <summary>
    /// Geomancy API Controller for generating geomantic figures and house charts
    /// </summary>
    [RoutePrefix("api/geomancy")]
    public class GeomancyController : ApiController
    {
        /// <summary>
        /// Simple test endpoint to verify API is working
        /// </summary>
        /// <returns>Simple test response</returns>
        [HttpGet]
        [Route("test")]
        public HttpResponseMessage Test()
        {
            return Request.CreateResponse(HttpStatusCode.OK, new { message = "API is working!", timestamp = DateTime.UtcNow });
        }

        /// <summary>
        /// Generate a single geomantic figure from elemental lines
        /// </summary>
        /// <param name="request">The elemental line values (1 or 2 for each line)</param>
        /// <returns>The generated figure with all its properties</returns>
        [HttpPost]
        [Route("figure")]
        [ResponseType(typeof(GenerateFigureResponse))]
        public HttpResponseMessage GenerateFigure([FromBody] GenerateFigureRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }

                // Create a GeomanticFigure from the request
                var figure = new GeomanticFigure(request.HeadLine, request.NeckLine, request.BodyLine, request.FootLine);

                // Convert to response model
                var response = new GenerateFigureResponse
                {
                    Figure = ConvertToFigureResponse(figure),
                    Success = true,
                    Message = "Figure generated successfully"
                };

                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                var errorResponse = new GenerateFigureResponse
                {
                    Success = false,
                    Message = $"Error generating figure: {ex.Message}"
                };
                return Request.CreateResponse(HttpStatusCode.InternalServerError, errorResponse);
            }
        }

        /// <summary>
        /// Generate 4 geomantic figures for the first 4 houses
        /// </summary>
        /// <param name="request">The 4 figure requests for houses 1-4</param>
        /// <returns>The 4 generated figures</returns>
        [HttpPost]
        [Route("figures")]
        [ResponseType(typeof(GenerateFourFiguresResponse))]
        public HttpResponseMessage GenerateFourFigures([FromBody] GenerateFourFiguresRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }

                var figures = new List<FigureResponse>();

                // Generate figures for each house
                var figure1 = new GeomanticFigure(request.House1.HeadLine, request.House1.NeckLine, request.House1.BodyLine, request.House1.FootLine, 1);
                var figure2 = new GeomanticFigure(request.House2.HeadLine, request.House2.NeckLine, request.House2.BodyLine, request.House2.FootLine, 2);
                var figure3 = new GeomanticFigure(request.House3.HeadLine, request.House3.NeckLine, request.House3.BodyLine, request.House3.FootLine, 3);
                var figure4 = new GeomanticFigure(request.House4.HeadLine, request.House4.NeckLine, request.House4.BodyLine, request.House4.FootLine, 4);

                figures.Add(ConvertToFigureResponse(figure1));
                figures.Add(ConvertToFigureResponse(figure2));
                figures.Add(ConvertToFigureResponse(figure3));
                figures.Add(ConvertToFigureResponse(figure4));

                var response = new GenerateFourFiguresResponse
                {
                    Figures = figures,
                    Success = true,
                    Message = "Four figures generated successfully"
                };

                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                var errorResponse = new GenerateFourFiguresResponse
                {
                    Success = false,
                    Message = $"Error generating figures: {ex.Message}"
                };
                return Request.CreateResponse(HttpStatusCode.InternalServerError, errorResponse);
            }
        }

        /// <summary>
        /// Generate a complete house chart from 4 figures and return all calculated houses, witnesses, judge, and sentence
        /// </summary>
        /// <param name="request">The 4 figure requests for houses 1-4</param>
        /// <returns>The complete house chart with all 12 houses, witnesses, judge, and sentence</returns>
        [HttpPost]
        [Route("chart")]
        [ResponseType(typeof(HouseChartResponse))]
        public HttpResponseMessage GenerateHouseChart([FromBody] GenerateFourFiguresRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }

                // Create the house chart
                var houseChart = new HouseChart();

                // Set the first 4 houses and calculate the complete chart
                houseChart.SetFirstFourHousesAndCalculate(
                    request.House1.HeadLine, request.House1.NeckLine, request.House1.BodyLine, request.House1.FootLine,
                    request.House2.HeadLine, request.House2.NeckLine, request.House2.BodyLine, request.House2.FootLine,
                    request.House3.HeadLine, request.House3.NeckLine, request.House3.BodyLine, request.House3.FootLine,
                    request.House4.HeadLine, request.House4.NeckLine, request.House4.BodyLine, request.House4.FootLine
                );

                // Convert to response model
                var response = ConvertToHouseChartResponse(houseChart);

                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                var errorResponse = new ErrorResponse
                {
                    Error = "Chart Generation Error",
                    Message = $"Error generating house chart: {ex.Message}",
                    Timestamp = DateTime.UtcNow
                };
                return Request.CreateResponse(HttpStatusCode.InternalServerError, errorResponse);
            }
        }

        /// <summary>
        /// Get information about a specific geomantic figure by name
        /// </summary>
        /// <param name="figureName">The name of the figure (e.g., "Via", "Populus", "Conjunctio")</param>
        /// <returns>The figure information</returns>
        [HttpGet]
        [Route("figure/{figureName}")]
        [ResponseType(typeof(GenerateFigureResponse))]
        public HttpResponseMessage GetFigureByName(string figureName)
        {
            try
            {
                var figure = new GeomanticFigure(figureName);

                var response = new GenerateFigureResponse
                {
                    Figure = ConvertToFigureResponse(figure),
                    Success = true,
                    Message = $"Figure '{figureName}' retrieved successfully"
                };

                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                var errorResponse = new GenerateFigureResponse
                {
                    Success = false,
                    Message = $"Error retrieving figure '{figureName}': {ex.Message}"
                };
                return Request.CreateResponse(HttpStatusCode.NotFound, errorResponse);
            }
        }

        /// <summary>
        /// Get all available geomantic figures
        /// </summary>
        /// <returns>List of all 16 geomantic figures</returns>
        [HttpGet]
        [Route("figures/all")]
        [ResponseType(typeof(List<FigureResponse>))]
        public HttpResponseMessage GetAllFigures()
        {
            try
            {
                var allFigures = FigureData.GetAllFigures();
                var response = allFigures.Select(f => ConvertToFigureResponse(new GeomanticFigure(f.Name))).ToList();

                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                var errorResponse = new ErrorResponse
                {
                    Error = "Retrieval Error",
                    Message = $"Error retrieving all figures: {ex.Message}",
                    Timestamp = DateTime.UtcNow
                };
                return Request.CreateResponse(HttpStatusCode.InternalServerError, errorResponse);
            }
        }

        /// <summary>
        /// Get a figure by its elemental pattern
        /// </summary>
        /// <param name="headLine">Head line value (1 or 2)</param>
        /// <param name="neckLine">Neck line value (1 or 2)</param>
        /// <param name="bodyLine">Body line value (1 or 2)</param>
        /// <param name="footLine">Foot line value (1 or 2)</param>
        /// <returns>The figure matching the elemental pattern</returns>
        [HttpGet]
        [Route("figure/pattern")]
        [ResponseType(typeof(GenerateFigureResponse))]
        public HttpResponseMessage GetFigureByPattern(int headLine, int neckLine, int bodyLine, int footLine)
        {
            try
            {
                if (headLine < 1 || headLine > 2 || neckLine < 1 || neckLine > 2 || 
                    bodyLine < 1 || bodyLine > 2 || footLine < 1 || footLine > 2)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "All line values must be 1 or 2");
                }

                var figure = new GeomanticFigure(headLine, neckLine, bodyLine, footLine);

                var response = new GenerateFigureResponse
                {
                    Figure = ConvertToFigureResponse(figure),
                    Success = true,
                    Message = "Figure retrieved by pattern successfully"
                };

                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                var errorResponse = new GenerateFigureResponse
                {
                    Success = false,
                    Message = $"Error retrieving figure by pattern: {ex.Message}"
                };
                return Request.CreateResponse(HttpStatusCode.NotFound, errorResponse);
            }
        }

        /// <summary>
        /// Generate a complete house chart from 4 figure names (individual parameters)
        /// </summary>
        /// <param name="firstMother">Name of the first mother figure</param>
        /// <param name="secondMother">Name of the second mother figure</param>
        /// <param name="thirdMother">Name of the third mother figure</param>
        /// <param name="fourthMother">Name of the fourth mother figure</param>
        /// <returns>The complete house chart with all 12 houses, witnesses, judge, and sentence</returns>
        [HttpGet]
        [Route("chart/figures")]
        [ResponseType(typeof(HouseChartResponse))]
        public HttpResponseMessage GenerateChartFromFigureNames(string firstMother, string secondMother, string thirdMother, string fourthMother)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(firstMother) || string.IsNullOrWhiteSpace(secondMother) || 
                    string.IsNullOrWhiteSpace(thirdMother) || string.IsNullOrWhiteSpace(fourthMother))
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "All four mother figure names are required");
                }

                // Create figures from names
                var figure1 = new GeomanticFigure(firstMother);
                var figure2 = new GeomanticFigure(secondMother);
                var figure3 = new GeomanticFigure(thirdMother);
                var figure4 = new GeomanticFigure(fourthMother);

                // Create the house chart
                var houseChart = new HouseChart();

                // Set the first 4 houses and calculate the complete chart
                houseChart.SetFirstFourHousesAndCalculate(
                    figure1.HeadLine, figure1.NeckLine, figure1.BodyLine, figure1.FootLine,
                    figure2.HeadLine, figure2.NeckLine, figure2.BodyLine, figure2.FootLine,
                    figure3.HeadLine, figure3.NeckLine, figure3.BodyLine, figure3.FootLine,
                    figure4.HeadLine, figure4.NeckLine, figure4.BodyLine, figure4.FootLine
                );

                // Convert to response model
                var response = ConvertToHouseChartResponse(houseChart);

                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                var errorResponse = new ErrorResponse
                {
                    Error = "Chart Generation Error",
                    Message = $"Error generating house chart from figure names: {ex.Message}",
                    Timestamp = DateTime.UtcNow
                };
                return Request.CreateResponse(HttpStatusCode.InternalServerError, errorResponse);
            }
        }

        /// <summary>
        /// Generate a complete house chart from 4 figure names (comma-separated)
        /// </summary>
        /// <param name="mothers">Comma-separated list of 4 mother figure names</param>
        /// <returns>The complete house chart with all 12 houses, witnesses, judge, and sentence</returns>
        [HttpGet]
        [Route("chart/figures/comma")]
        [ResponseType(typeof(HouseChartResponse))]
        public HttpResponseMessage GenerateChartFromCommaSeparatedFigures(string mothers)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(mothers))
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Mothers parameter is required (comma-separated figure names)");
                }

                var figureNames = mothers.Split(',').Select(f => f.Trim()).ToArray();
                
                if (figureNames.Length != 4)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Exactly 4 figure names are required, separated by commas");
                }

                // Create figures from names
                var figure1 = new GeomanticFigure(figureNames[0]);
                var figure2 = new GeomanticFigure(figureNames[1]);
                var figure3 = new GeomanticFigure(figureNames[2]);
                var figure4 = new GeomanticFigure(figureNames[3]);

                // Create the house chart
                var houseChart = new HouseChart();

                // Set the first 4 houses and calculate the complete chart
                houseChart.SetFirstFourHousesAndCalculate(
                    figure1.HeadLine, figure1.NeckLine, figure1.BodyLine, figure1.FootLine,
                    figure2.HeadLine, figure2.NeckLine, figure2.BodyLine, figure2.FootLine,
                    figure3.HeadLine, figure3.NeckLine, figure3.BodyLine, figure3.FootLine,
                    figure4.HeadLine, figure4.NeckLine, figure4.BodyLine, figure4.FootLine
                );

                // Convert to response model
                var response = ConvertToHouseChartResponse(houseChart);

                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                var errorResponse = new ErrorResponse
                {
                    Error = "Chart Generation Error",
                    Message = $"Error generating house chart from comma-separated figures: {ex.Message}",
                    Timestamp = DateTime.UtcNow
                };
                return Request.CreateResponse(HttpStatusCode.InternalServerError, errorResponse);
            }
        }

        /// <summary>
        /// Get all aspects in a chart above a minimum aspect type
        /// </summary>
        /// <param name="request">Chart data</param>
        /// <param name="minAspect">Minimum aspect type to include (default: Sextile)</param>
        /// <returns>List of all aspects in the chart</returns>
        [HttpPost]
        [Route("aspects")]
        [ResponseType(typeof(AspectsResponse))]
        public HttpResponseMessage GetChartAspects([FromBody] GenerateFourFiguresRequest request, string minAspect = "Sextile")
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }

                // Create the house chart
                var houseChart = new HouseChart();
                houseChart.SetFirstFourHousesAndCalculate(
                    request.House1.HeadLine, request.House1.NeckLine, request.House1.BodyLine, request.House1.FootLine,
                    request.House2.HeadLine, request.House2.NeckLine, request.House2.BodyLine, request.House2.FootLine,
                    request.House3.HeadLine, request.House3.NeckLine, request.House3.BodyLine, request.House3.FootLine,
                    request.House4.HeadLine, request.House4.NeckLine, request.House4.BodyLine, request.House4.FootLine
                );

                // Parse minimum aspect type
                AspectType minAspectType = AspectType.Sextile;
                if (Enum.TryParse<AspectType>(minAspect, true, out var parsed))
                {
                    minAspectType = parsed;
                }

                // Get all aspects
                var aspects = GeomanticAspects.AllAspects(houseChart, minAspectType).ToList();

                // Convert to response model
                var aspectList = aspects.Select(a => new AspectInfo
                {
                    FromHouse = a.from,
                    ToHouse = a.to,
                    AspectType = a.aspect.ToString(),
                    FromFigure = houseChart.GetHouseFigure(a.from)?.Name,
                    ToFigure = houseChart.GetHouseFigure(a.to)?.Name
                }).ToList();

                var response = new AspectsResponse
                {
                    Aspects = aspectList,
                    TotalAspects = aspectList.Count,
                    MinimumAspectType = minAspectType.ToString(),
                    Success = true,
                    Message = $"Found {aspectList.Count} aspects of type {minAspectType} or higher"
                };

                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                var errorResponse = new AspectsResponse
                {
                    Success = false,
                    Message = $"Error calculating aspects: {ex.Message}"
                };
                return Request.CreateResponse(HttpStatusCode.InternalServerError, errorResponse);
            }
        }

        /// <summary>
        /// Calculate perfection between two significators in a chart
        /// </summary>
        /// <param name="request">Chart data and significator houses</param>
        /// <returns>Perfection analysis result (may contain multiple perfections)</returns>
        [HttpPost]
        [Route("perfection")]
        [ResponseType(typeof(MultiplePerfectionsResponse))]
        public HttpResponseMessage CalculatePerfection([FromBody] PerfectionRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }

                // Create the house chart from the provided mothers
                var houseChart = new HouseChart();
                houseChart.SetFirstFourHousesAndCalculate(
                    request.Mothers.House1.HeadLine, request.Mothers.House1.NeckLine, request.Mothers.House1.BodyLine, request.Mothers.House1.FootLine,
                    request.Mothers.House2.HeadLine, request.Mothers.House2.NeckLine, request.Mothers.House2.BodyLine, request.Mothers.House2.FootLine,
                    request.Mothers.House3.HeadLine, request.Mothers.House3.NeckLine, request.Mothers.House3.BodyLine, request.Mothers.House3.FootLine,
                    request.Mothers.House4.HeadLine, request.Mothers.House4.NeckLine, request.Mothers.House4.BodyLine, request.Mothers.House4.FootLine
                );

                // Calculate all perfections for this pair
                var allPerfections = PerfectionCalculator.Find(houseChart, request.QuerentHouse, request.QuesitedHouse, returnAllModes: true);

                // Filter out None perfections
                var validPerfections = allPerfections.Where(p => p.Mode != PerfectionType.None).ToList();

                // Convert all perfections to response models (without individual scores)
                var perfectionResponses = new List<PerfectionResponse>();
                foreach (var perfection in validPerfections)
                {
                    var perfectionResponse = ToPerfectionResponseWithoutScoring(perfection, houseChart, request.QuerentHouse, request.QuesitedHouse);
                    perfectionResponses.Add(perfectionResponse);
                }

                // Calculate aggregate scores after all perfections are found
                int totalFavorableScore = 0;
                
                // Sum favorable scores from all perfections
                foreach (var perfection in validPerfections)
                {
                    totalFavorableScore += PerfectionCalculator.CalculateScore(perfection);
                }
                
                // Calculate total unfavorable score once (handles impedition and unfavorable aspects)
                // This method checks impedition only if no perfections exist, and checks unfavorable aspects
                int totalUnfavorableScore = PerfectionCalculator.CalculateTotalUnfavorableScore(houseChart, request.QuerentHouse, request.QuesitedHouse);
                
                int aggregateNetScore = totalFavorableScore + totalUnfavorableScore; // unfavorableScore is already negative

                // Apply aggregate scores to all responses
                foreach (var perfectionResponse in perfectionResponses)
                {
                    perfectionResponse.FavorableScore = totalFavorableScore;
                    perfectionResponse.UnfavorableScore = totalUnfavorableScore;
                    perfectionResponse.NetScore = aggregateNetScore;
                }

                var response = new MultiplePerfectionsResponse
                {
                    Success = true,
                    Message = perfectionResponses.Count > 0 
                        ? $"Found {perfectionResponses.Count} perfection(s)." 
                        : "No perfections found.",
                    Perfections = perfectionResponses,
                    TotalPerfections = perfectionResponses.Count
                };

                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                var errorResponse = new MultiplePerfectionsResponse
                {
                    Success = false,
                    Message = $"Error calculating perfection: {ex.Message}",
                    Perfections = new List<PerfectionResponse>(),
                    TotalPerfections = 0
                };
                return Request.CreateResponse(HttpStatusCode.InternalServerError, errorResponse);
            }
        }

        /// <summary>
        /// Calculate perfection using figure names as query parameters (simple URL testing)
        /// </summary>
        /// <param name="firstMother">Name of the first mother figure</param>
        /// <param name="secondMother">Name of the second mother figure</param>
        /// <param name="thirdMother">Name of the third mother figure</param>
        /// <param name="fourthMother">Name of the fourth mother figure</param>
        /// <param name="querentHouse">House number for the querent (1-12)</param>
        /// <param name="quesitedHouse">House number for the quesited (1-12)</param>
        /// <returns>Perfection analysis result</returns>
        [HttpGet]
        [Route("perfection/figures")]
        [ResponseType(typeof(PerfectionResponse))]
        public HttpResponseMessage CalculatePerfectionFromFigures(string firstMother, string secondMother, string thirdMother, string fourthMother, int querentHouse, int quesitedHouse)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(firstMother) || string.IsNullOrWhiteSpace(secondMother) || 
                    string.IsNullOrWhiteSpace(thirdMother) || string.IsNullOrWhiteSpace(fourthMother))
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "All four mother figure names are required");
                }

                if (querentHouse < 1 || querentHouse > 12 || quesitedHouse < 1 || quesitedHouse > 12)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "House numbers must be between 1 and 12");
                }

                // Create figures from names
                var figure1 = new GeomanticFigure(firstMother);
                var figure2 = new GeomanticFigure(secondMother);
                var figure3 = new GeomanticFigure(thirdMother);
                var figure4 = new GeomanticFigure(fourthMother);

                // Create the house chart
                var houseChart = new HouseChart();
                houseChart.SetFirstFourHousesAndCalculate(
                    figure1.HeadLine, figure1.NeckLine, figure1.BodyLine, figure1.FootLine,
                    figure2.HeadLine, figure2.NeckLine, figure2.BodyLine, figure2.FootLine,
                    figure3.HeadLine, figure3.NeckLine, figure3.BodyLine, figure3.FootLine,
                    figure4.HeadLine, figure4.NeckLine, figure4.BodyLine, figure4.FootLine
                );

                // Calculate perfection
                var perfection = PerfectionCalculator.Find(houseChart, querentHouse, quesitedHouse);

                // Convert to response model with scoring
                var response = ToPerfectionResponse(perfection, houseChart, querentHouse, quesitedHouse);

                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                var errorResponse = new PerfectionResponse
                {
                    Success = false,
                    Message = $"Error calculating perfection: {ex.Message}"
                };
                return Request.CreateResponse(HttpStatusCode.InternalServerError, errorResponse);
            }
        }

        /// <summary>
        /// Calculate perfection using elemental patterns as query parameters (simple URL testing)
        /// </summary>
        /// <param name="h1Head">House 1 Head line (1 or 2)</param>
        /// <param name="h1Neck">House 1 Neck line (1 or 2)</param>
        /// <param name="h1Body">House 1 Body line (1 or 2)</param>
        /// <param name="h1Foot">House 1 Foot line (1 or 2)</param>
        /// <param name="h2Head">House 2 Head line (1 or 2)</param>
        /// <param name="h2Neck">House 2 Neck line (1 or 2)</param>
        /// <param name="h2Body">House 2 Body line (1 or 2)</param>
        /// <param name="h2Foot">House 2 Foot line (1 or 2)</param>
        /// <param name="h3Head">House 3 Head line (1 or 2)</param>
        /// <param name="h3Neck">House 3 Neck line (1 or 2)</param>
        /// <param name="h3Body">House 3 Body line (1 or 2)</param>
        /// <param name="h3Foot">House 3 Foot line (1 or 2)</param>
        /// <param name="h4Head">House 4 Head line (1 or 2)</param>
        /// <param name="h4Neck">House 4 Neck line (1 or 2)</param>
        /// <param name="h4Body">House 4 Body line (1 or 2)</param>
        /// <param name="h4Foot">House 4 Foot line (1 or 2)</param>
        /// <param name="querentHouse">House number for the querent (1-12)</param>
        /// <param name="quesitedHouse">House number for the quesited (1-12)</param>
        /// <returns>Perfection analysis result</returns>
        [HttpGet]
        [Route("perfection/pattern")]
        [ResponseType(typeof(PerfectionResponse))]
        public HttpResponseMessage CalculatePerfectionFromPattern(int h1Head, int h1Neck, int h1Body, int h1Foot,
                                                                 int h2Head, int h2Neck, int h2Body, int h2Foot,
                                                                 int h3Head, int h3Neck, int h3Body, int h3Foot,
                                                                 int h4Head, int h4Neck, int h4Body, int h4Foot,
                                                                 int querentHouse, int quesitedHouse)
        {
            try
            {
                // Validate elemental patterns
                if (h1Head < 1 || h1Head > 2 || h1Neck < 1 || h1Neck > 2 || h1Body < 1 || h1Body > 2 || h1Foot < 1 || h1Foot > 2 ||
                    h2Head < 1 || h2Head > 2 || h2Neck < 1 || h2Neck > 2 || h2Body < 1 || h2Body > 2 || h2Foot < 1 || h2Foot > 2 ||
                    h3Head < 1 || h3Head > 2 || h3Neck < 1 || h3Neck > 2 || h3Body < 1 || h3Body > 2 || h3Foot < 1 || h3Foot > 2 ||
                    h4Head < 1 || h4Head > 2 || h4Neck < 1 || h4Neck > 2 || h4Body < 1 || h4Body > 2 || h4Foot < 1 || h4Foot > 2)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "All elemental line values must be 1 or 2");
                }

                if (querentHouse < 1 || querentHouse > 12 || quesitedHouse < 1 || quesitedHouse > 12)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "House numbers must be between 1 and 12");
                }

                // Create the house chart
                var houseChart = new HouseChart();
                houseChart.SetFirstFourHousesAndCalculate(h1Head, h1Neck, h1Body, h1Foot,
                                                         h2Head, h2Neck, h2Body, h2Foot,
                                                         h3Head, h3Neck, h3Body, h3Foot,
                                                         h4Head, h4Neck, h4Body, h4Foot);

                // Calculate perfection
                var perfection = PerfectionCalculator.Find(houseChart, querentHouse, quesitedHouse);

                // Convert to response model with scoring
                var response = ToPerfectionResponse(perfection, houseChart, querentHouse, quesitedHouse);

                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                var errorResponse = new PerfectionResponse
                {
                    Success = false,
                    Message = $"Error calculating perfection: {ex.Message}"
                };
                return Request.CreateResponse(HttpStatusCode.InternalServerError, errorResponse);
            }
        }

        /// <summary>
        /// Get aspects using figure names as query parameters (simple URL testing)
        /// </summary>
        /// <param name="firstMother">Name of the first mother figure</param>
        /// <param name="secondMother">Name of the second mother figure</param>
        /// <param name="thirdMother">Name of the third mother figure</param>
        /// <param name="fourthMother">Name of the fourth mother figure</param>
        /// <param name="minAspect">Minimum aspect type to include (default: Sextile)</param>
        /// <returns>List of all aspects in the chart</returns>
        [HttpGet]
        [Route("aspects/figures")]
        [ResponseType(typeof(AspectsResponse))]
        public HttpResponseMessage GetAspectsFromFigures(string firstMother, string secondMother, string thirdMother, string fourthMother, string minAspect = "Sextile")
        {
            try
            {
                if (string.IsNullOrWhiteSpace(firstMother) || string.IsNullOrWhiteSpace(secondMother) || 
                    string.IsNullOrWhiteSpace(thirdMother) || string.IsNullOrWhiteSpace(fourthMother))
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "All four mother figure names are required");
                }

                // Create figures from names
                var figure1 = new GeomanticFigure(firstMother);
                var figure2 = new GeomanticFigure(secondMother);
                var figure3 = new GeomanticFigure(thirdMother);
                var figure4 = new GeomanticFigure(fourthMother);

                // Create the house chart
                var houseChart = new HouseChart();
                houseChart.SetFirstFourHousesAndCalculate(
                    figure1.HeadLine, figure1.NeckLine, figure1.BodyLine, figure1.FootLine,
                    figure2.HeadLine, figure2.NeckLine, figure2.BodyLine, figure2.FootLine,
                    figure3.HeadLine, figure3.NeckLine, figure3.BodyLine, figure3.FootLine,
                    figure4.HeadLine, figure4.NeckLine, figure4.BodyLine, figure4.FootLine
                );

                // Parse minimum aspect type
                AspectType minAspectType = AspectType.Sextile;
                if (Enum.TryParse<AspectType>(minAspect, true, out var parsed))
                {
                    minAspectType = parsed;
                }

                // Get all aspects
                var aspects = GeomanticAspects.AllAspects(houseChart, minAspectType).ToList();

                // Convert to response model
                var aspectList = aspects.Select(a => new AspectInfo
                {
                    FromHouse = a.from,
                    ToHouse = a.to,
                    AspectType = a.aspect.ToString(),
                    FromFigure = houseChart.GetHouseFigure(a.from)?.Name,
                    ToFigure = houseChart.GetHouseFigure(a.to)?.Name
                }).ToList();

                var response = new AspectsResponse
                {
                    Aspects = aspectList,
                    TotalAspects = aspectList.Count,
                    MinimumAspectType = minAspectType.ToString(),
                    Success = true,
                    Message = $"Found {aspectList.Count} aspects of type {minAspectType} or higher"
                };

                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                var errorResponse = new AspectsResponse
                {
                    Success = false,
                    Message = $"Error calculating aspects: {ex.Message}"
                };
                return Request.CreateResponse(HttpStatusCode.InternalServerError, errorResponse);
            }
        }

        /// <summary>
        /// Get aspects using elemental patterns as query parameters (simple URL testing)
        /// </summary>
        /// <param name="h1Head">House 1 Head line (1 or 2)</param>
        /// <param name="h1Neck">House 1 Neck line (1 or 2)</param>
        /// <param name="h1Body">House 1 Body line (1 or 2)</param>
        /// <param name="h1Foot">House 1 Foot line (1 or 2)</param>
        /// <param name="h2Head">House 2 Head line (1 or 2)</param>
        /// <param name="h2Neck">House 2 Neck line (1 or 2)</param>
        /// <param name="h2Body">House 2 Body line (1 or 2)</param>
        /// <param name="h2Foot">House 2 Foot line (1 or 2)</param>
        /// <param name="h3Head">House 3 Head line (1 or 2)</param>
        /// <param name="h3Neck">House 3 Neck line (1 or 2)</param>
        /// <param name="h3Body">House 3 Body line (1 or 2)</param>
        /// <param name="h3Foot">House 3 Foot line (1 or 2)</param>
        /// <param name="h4Head">House 4 Head line (1 or 2)</param>
        /// <param name="h4Neck">House 4 Neck line (1 or 2)</param>
        /// <param name="h4Body">House 4 Body line (1 or 2)</param>
        /// <param name="h4Foot">House 4 Foot line (1 or 2)</param>
        /// <param name="minAspect">Minimum aspect type to include (default: Sextile)</param>
        /// <returns>List of all aspects in the chart</returns>
        [HttpGet]
        [Route("aspects/pattern")]
        [ResponseType(typeof(AspectsResponse))]
        public HttpResponseMessage GetAspectsFromPattern(int h1Head, int h1Neck, int h1Body, int h1Foot,
                                                        int h2Head, int h2Neck, int h2Body, int h2Foot,
                                                        int h3Head, int h3Neck, int h3Body, int h3Foot,
                                                        int h4Head, int h4Neck, int h4Body, int h4Foot,
                                                        string minAspect = "Sextile")
        {
            try
            {
                // Validate elemental patterns
                if (h1Head < 1 || h1Head > 2 || h1Neck < 1 || h1Neck > 2 || h1Body < 1 || h1Body > 2 || h1Foot < 1 || h1Foot > 2 ||
                    h2Head < 1 || h2Head > 2 || h2Neck < 1 || h2Neck > 2 || h2Body < 1 || h2Body > 2 || h2Foot < 1 || h2Foot > 2 ||
                    h3Head < 1 || h3Head > 2 || h3Neck < 1 || h3Neck > 2 || h3Body < 1 || h3Body > 2 || h3Foot < 1 || h3Foot > 2 ||
                    h4Head < 1 || h4Head > 2 || h4Neck < 1 || h4Neck > 2 || h4Body < 1 || h4Body > 2 || h4Foot < 1 || h4Foot > 2)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "All elemental line values must be 1 or 2");
                }

                // Create the house chart
                var houseChart = new HouseChart();
                houseChart.SetFirstFourHousesAndCalculate(h1Head, h1Neck, h1Body, h1Foot,
                                                         h2Head, h2Neck, h2Body, h2Foot,
                                                         h3Head, h3Neck, h3Body, h3Foot,
                                                         h4Head, h4Neck, h4Body, h4Foot);

                // Parse minimum aspect type
                AspectType minAspectType = AspectType.Sextile;
                if (Enum.TryParse<AspectType>(minAspect, true, out var parsed))
                {
                    minAspectType = parsed;
                }

                // Get all aspects
                var aspects = GeomanticAspects.AllAspects(houseChart, minAspectType).ToList();

                // Convert to response model
                var aspectList = aspects.Select(a => new AspectInfo
                {
                    FromHouse = a.from,
                    ToHouse = a.to,
                    AspectType = a.aspect.ToString(),
                    FromFigure = houseChart.GetHouseFigure(a.from)?.Name,
                    ToFigure = houseChart.GetHouseFigure(a.to)?.Name
                }).ToList();

                var response = new AspectsResponse
                {
                    Aspects = aspectList,
                    TotalAspects = aspectList.Count,
                    MinimumAspectType = minAspectType.ToString(),
                    Success = true,
                    Message = $"Found {aspectList.Count} aspects of type {minAspectType} or higher"
                };

                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                var errorResponse = new AspectsResponse
                {
                    Success = false,
                    Message = $"Error calculating aspects: {ex.Message}"
                };
                return Request.CreateResponse(HttpStatusCode.InternalServerError, errorResponse);
            }
        }

        /// <summary>
        /// Find all perfections in a chart using figure names
        /// </summary>
        /// <param name="firstMother">Name of the first mother figure</param>
        /// <param name="secondMother">Name of the second mother figure</param>
        /// <param name="thirdMother">Name of the third mother figure</param>
        /// <param name="fourthMother">Name of the fourth mother figure</param>
        /// <returns>All perfections found in the chart</returns>
        [HttpGet]
        [Route("perfections/figures")]
        [ResponseType(typeof(MultiplePerfectionsResponse))]
        public HttpResponseMessage GetAllPerfectionsFromFigures(string firstMother, string secondMother, string thirdMother, string fourthMother)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(firstMother) || string.IsNullOrWhiteSpace(secondMother) || 
                    string.IsNullOrWhiteSpace(thirdMother) || string.IsNullOrWhiteSpace(fourthMother))
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "All four mother figure names are required");
                }

                // Create figures from names
                var figure1 = new GeomanticFigure(firstMother);
                var figure2 = new GeomanticFigure(secondMother);
                var figure3 = new GeomanticFigure(thirdMother);
                var figure4 = new GeomanticFigure(fourthMother);

                // Create the house chart
                var houseChart = new HouseChart();
                houseChart.SetFirstFourHousesAndCalculate(
                    figure1.HeadLine, figure1.NeckLine, figure1.BodyLine, figure1.FootLine,
                    figure2.HeadLine, figure2.NeckLine, figure2.BodyLine, figure2.FootLine,
                    figure3.HeadLine, figure3.NeckLine, figure3.BodyLine, figure3.FootLine,
                    figure4.HeadLine, figure4.NeckLine, figure4.BodyLine, figure4.FootLine
                );

                // Find all perfections
                var perfections = PerfectionCalculator.FindAll(houseChart).ToList();

                // Convert to response model with scoring
                var perfectionList = perfections.Select(p => ToPerfectionResponse(p, houseChart, p.QuerentHouse, p.QuesitedHouse)).ToList();

                var response = new MultiplePerfectionsResponse
                {
                    Perfections = perfectionList,
                    TotalPerfections = perfectionList.Count,
                    Success = true,
                    Message = $"Found {perfectionList.Count} perfections in the chart"
                };

                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                var errorResponse = new MultiplePerfectionsResponse
                {
                    Success = false,
                    Message = $"Error calculating perfections: {ex.Message}"
                };
                return Request.CreateResponse(HttpStatusCode.InternalServerError, errorResponse);
            }
        }

        /// <summary>
        /// Get aspect analysis with scoring using figure names
        /// </summary>
        /// <param name="firstMother">Name of the first mother figure</param>
        /// <param name="secondMother">Name of the second mother figure</param>
        /// <param name="thirdMother">Name of the third mother figure</param>
        /// <param name="fourthMother">Name of the fourth mother figure</param>
        /// <returns>Aspect analysis with scoring</returns>
        [HttpGet]
        [Route("aspect-analysis/figures")]
        [ResponseType(typeof(AspectAnalysisResponse))]
        public HttpResponseMessage GetAspectAnalysisFromFigures(string firstMother, string secondMother, string thirdMother, string fourthMother)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(firstMother) || string.IsNullOrWhiteSpace(secondMother) || 
                    string.IsNullOrWhiteSpace(thirdMother) || string.IsNullOrWhiteSpace(fourthMother))
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "All four mother figure names are required");
                }

                // Create figures from names
                var figure1 = new GeomanticFigure(firstMother);
                var figure2 = new GeomanticFigure(secondMother);
                var figure3 = new GeomanticFigure(thirdMother);
                var figure4 = new GeomanticFigure(fourthMother);

                // Create the house chart
                var houseChart = new HouseChart();
                houseChart.SetFirstFourHousesAndCalculate(
                    figure1.HeadLine, figure1.NeckLine, figure1.BodyLine, figure1.FootLine,
                    figure2.HeadLine, figure2.NeckLine, figure2.BodyLine, figure2.FootLine,
                    figure3.HeadLine, figure3.NeckLine, figure3.BodyLine, figure3.FootLine,
                    figure4.HeadLine, figure4.NeckLine, figure4.BodyLine, figure4.FootLine
                );

                // Get aspect analysis
                var aspectReport = ChartAspectAnalysis.Evaluate(houseChart);

                // Convert to response model
                var aspectList = aspectReport.Details.Select(d => new AspectDetail
                {
                    FromHouse = d.From,
                    ToHouse = d.To,
                    AspectType = d.Aspect.ToString(),
                    Weight = d.Weight,
                    FromFigure = houseChart.GetHouseFigure(d.From)?.Name,
                    ToFigure = houseChart.GetHouseFigure(d.To)?.Name,
                    Justification = d.Justification != null ? new WeightJustificationDetail
                    {
                        BaseWeight = d.Justification.BaseWeight,
                        TotalModifier = d.Justification.TotalModifier,
                        FinalWeight = d.Justification.FinalWeight,
                        Justifications = d.Justification.Justifications
                    } : null
                }).ToList();

                var response = new AspectAnalysisResponse
                {
                    TotalScore = aspectReport.TotalScore,
                    Aspects = aspectList,
                    TotalAspects = aspectList.Count,
                    Polarity = new PolarityReport {
                        Easy = aspectReport.EasyScore,
                        Hard = aspectReport.HardScore,
                        Delta = aspectReport.Delta,
                        Percent = aspectReport.PolarityPercent,
                        Verdict = aspectReport.PolarityVerdict
                    },
                    AspectCounts = aspectReport.AspectCounts,
                    Success = true,
                    Message = $"Chart aspect strength: {aspectReport.TotalScore} points from {aspectList.Count} aspects. Polarity: {aspectReport.PolarityVerdict} ({aspectReport.PolarityPercent:F1}%)"
                };

                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                var errorResponse = new AspectAnalysisResponse
                {
                    Success = false,
                    Message = $"Error analyzing aspects: {ex.Message}"
                };
                return Request.CreateResponse(HttpStatusCode.InternalServerError, errorResponse);
            }
        }

        /// <summary>
        /// Find all perfections for a specific querent and quesited house in a chart using figure names
        /// </summary>
        /// <param name="firstMother">Name of the first mother figure</param>
        /// <param name="secondMother">Name of the second mother figure</param>
        /// <param name="thirdMother">Name of the third mother figure</param>
        /// <param name="fourthMother">Name of the fourth mother figure</param>
        /// <param name="querentHouse">House number for the querent (1-12)</param>
        /// <param name="quesitedHouse">House number for the quesited (1-12)</param>
        /// <returns>All perfections found for the specified pair</returns>
        [HttpGet]
        [Route("perfections/figures/pair")]
        [ResponseType(typeof(MultiplePerfectionsResponse))]
        public HttpResponseMessage GetAllPerfectionsForPair(string firstMother, string secondMother, string thirdMother, string fourthMother, int querentHouse, int quesitedHouse)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(firstMother) || string.IsNullOrWhiteSpace(secondMother) || 
                    string.IsNullOrWhiteSpace(thirdMother) || string.IsNullOrWhiteSpace(fourthMother))
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "All four mother figure names are required");
                }
                if (querentHouse < 1 || querentHouse > 12 || quesitedHouse < 1 || quesitedHouse > 12)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "House numbers must be between 1 and 12");
                }

                // Create figures from names
                var figure1 = new GeomanticFigure(firstMother);
                var figure2 = new GeomanticFigure(secondMother);
                var figure3 = new GeomanticFigure(thirdMother);
                var figure4 = new GeomanticFigure(fourthMother);

                // Create the house chart
                var houseChart = new HouseChart();
                houseChart.SetFirstFourHousesAndCalculate(
                    figure1.HeadLine, figure1.NeckLine, figure1.BodyLine, figure1.FootLine,
                    figure2.HeadLine, figure2.NeckLine, figure2.BodyLine, figure2.FootLine,
                    figure3.HeadLine, figure3.NeckLine, figure3.BodyLine, figure3.FootLine,
                    figure4.HeadLine, figure4.NeckLine, figure4.BodyLine, figure4.FootLine
                );

                // Find all perfections for the specific pair
                var perfections = PerfectionCalculator.FindAll(houseChart, querentHouse, quesitedHouse).ToList();

                // Convert to response model with scoring
                var perfectionList = perfections.Select(p =>
                {
                    var perfectionResponse = ToPerfectionResponse(p, houseChart, querentHouse, quesitedHouse);
                    // Add indentation information
                    perfectionResponse.Indentation = ToIndentScoreResponse(ChartAspectAnalysis.ComputeIndent(houseChart, querentHouse, quesitedHouse));
                    perfectionResponse.TranslatorIndentation = (p.TranslatorHouse > 0)
                        ? ToIndentScoreResponse(ChartAspectAnalysis.ComputeIndent(houseChart, querentHouse, p.TranslatorHouse))
                        : null;
                    return perfectionResponse;
                }).ToList();

                var mainIndent = ChartAspectAnalysis.ComputeIndent(houseChart, querentHouse, quesitedHouse);
                IndentScoreResponse mainIndentResp = ToIndentScoreResponse(mainIndent);
                IndentScoreResponse translatorIndentResp = null;
                if (perfections.Any() && perfections[0].TranslatorHouse > 0)
                {
                    translatorIndentResp = ToIndentScoreResponse(ChartAspectAnalysis.ComputeIndent(houseChart, querentHouse, perfections[0].TranslatorHouse));
                }

                var response = new MultiplePerfectionsResponse
                {
                    Perfections = perfectionList,
                    TotalPerfections = perfectionList.Count,
                    Indentation = mainIndentResp,
                    TranslatorIndentation = translatorIndentResp,
                    Success = true,
                    Message = $"Found {perfectionList.Count} perfections for the specified pair"
                };

                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                var errorResponse = new MultiplePerfectionsResponse
                {
                    Success = false,
                    Message = $"Error calculating perfections: {ex.Message}"
                };
                return Request.CreateResponse(HttpStatusCode.InternalServerError, errorResponse);
            }
        }

        [HttpGet]
        [Route("indentation")]
        [ResponseType(typeof(IndentationResponse))]
        public HttpResponseMessage GetIndentation(string firstMother, string secondMother, string thirdMother, string fourthMother, int querentHouse, int quesitedHouse, int? translatorHouse = null)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(firstMother) || string.IsNullOrWhiteSpace(secondMother) || 
                    string.IsNullOrWhiteSpace(thirdMother) || string.IsNullOrWhiteSpace(fourthMother))
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "All four mother figure names are required");
                }
                if (querentHouse < 1 || querentHouse > 12 || quesitedHouse < 1 || quesitedHouse > 12)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "House numbers must be between 1 and 12");
                }

                // Create figures from names
                var figure1 = new GeomanticFigure(firstMother);
                var figure2 = new GeomanticFigure(secondMother);
                var figure3 = new GeomanticFigure(thirdMother);
                var figure4 = new GeomanticFigure(fourthMother);

                // Create the house chart
                var houseChart = new HouseChart();
                houseChart.SetFirstFourHousesAndCalculate(
                    figure1.HeadLine, figure1.NeckLine, figure1.BodyLine, figure1.FootLine,
                    figure2.HeadLine, figure2.NeckLine, figure2.BodyLine, figure2.FootLine,
                    figure3.HeadLine, figure3.NeckLine, figure3.BodyLine, figure3.FootLine,
                    figure4.HeadLine, figure4.NeckLine, figure4.BodyLine, figure4.FootLine
                );

                var mainIndent = ChartAspectAnalysis.ComputeIndent(houseChart, querentHouse, quesitedHouse);
                IndentScoreResponse mainIndentResp = ToIndentScoreResponse(mainIndent);
                IndentScoreResponse translatorIndentResp = null;
                if (translatorHouse.HasValue && translatorHouse.Value > 0)
                {
                    translatorIndentResp = ToIndentScoreResponse(ChartAspectAnalysis.ComputeIndent(houseChart, querentHouse, translatorHouse.Value));
                }
                var response = new IndentationResponse
                {
                    Indentation = mainIndentResp,
                    TranslatorIndentation = translatorIndentResp
                };
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                var errorResponse = new ErrorResponse
                {
                    Error = "Indentation Calculation Error",
                    Message = $"Error calculating indentation: {ex.Message}",
                    Timestamp = DateTime.UtcNow
                };
                return Request.CreateResponse(HttpStatusCode.InternalServerError, errorResponse);
            }
        }

        #region Helper Methods

        private FigureResponse ConvertToFigureResponse(GeomanticFigure figure)
        {
            return new FigureResponse
            {
                Name = figure.Name,
                HouseStrength = (Models.FigureInHouseStrength)(int)figure.HouseStrength,
                OtherNames = figure.OtherNames,
                Quality = figure.Quality,
                Keyword = figure.Keyword,
                Imagery = figure.Imagery,
                StrongHouse = figure.StrongHouse,
                WeakHouse = figure.WeakHouse,
                Planet = figure.Planet,
                Sign = figure.Sign,
                InnerEl = figure.InnerEl,
                OuterEl = figure.OuterEl,
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
                HeadLine = figure.HeadLine,
                NeckLine = figure.NeckLine,
                BodyLine = figure.BodyLine,
                FootLine = figure.FootLine
            };
        }

        private HouseChartResponse ConvertToHouseChartResponse(HouseChart houseChart)
        {
            var houses = new List<HouseResponse>();

            // Add all 12 houses
            for (int i = 1; i <= 12; i++)
            {
                var house = houseChart.GetHouseFigure(i);
                if (house != null)
                {
                    houses.Add(new HouseResponse
                    {
                        HouseNumber = i,
                        Figure = ConvertToFigureResponse(house)
                    });
                }
            }

            // Convert triplicities
            var triplicities = houseChart.GetTriplicities().Select(t => new TriplicityResponse
            {
                Number = t.Number,
                FirstFigure = ConvertToFigureResponse(t.FirstFigure),
                SecondFigure = ConvertToFigureResponse(t.SecondFigure),
                ThirdFigure = ConvertToFigureResponse(t.ThirdFigure),
                Description = t.Description
            }).ToList();

            return new HouseChartResponse
            {
                Houses = houses,
                RightWitness = houseChart.RightWitness != null ? ConvertToFigureResponse(houseChart.RightWitness) : null,
                LeftWitness = houseChart.LeftWitness != null ? ConvertToFigureResponse(houseChart.LeftWitness) : null,
                Judge = houseChart.Judge != null ? ConvertToFigureResponse(houseChart.Judge) : null,
                Sentence = houseChart.Sentence != null ? ConvertToFigureResponse(houseChart.Sentence) : null,
                Triplicities = triplicities,
                ChartSummary = houseChart.GetChartSummary(),
                IsComplete = houseChart.IsChartComplete(),
                GeneratedAt = DateTime.UtcNow
            };
        }

        // Helper to convert IndentScore to IndentScoreResponse
        private static IndentScoreResponse ToIndentScoreResponse(IndentScore s)
        {
            return new IndentScoreResponse
            {
                QuerentHouse = s.QuerentHouse,
                QuesitedHouse = s.QuesitedHouse,
                Index = s.Index,
                Bonus = s.Bonus
            };
        }

        // Helper to convert PerfectionResult to PerfectionResponse without scoring (for aggregate calculation)
        private static PerfectionResponse ToPerfectionResponseWithoutScoring(PerfectionResult perfection, HouseChart houseChart, int querentHouse, int quesitedHouse)
        {
            string modeString = perfection.Mode.ToString();
            if (perfection.Mode == PerfectionType.None)
            {
                modeString = "Impedition";
            }
            else if (perfection.Mode == PerfectionType.Aspect && !string.IsNullOrEmpty(perfection.AspectDirection))
            {
                modeString = "Aspect";
            }

            return new PerfectionResponse
            {
                // For Impedition (None), Success should be true so it displays as a card
                Success = true, // Always true - we want to display all results as cards
                Message = perfection.Mode == PerfectionType.None ? "No perfection found." : "Perfection found.",
                Mode = modeString,
                AspectBetweenSignificators = perfection.AspectBetweenSignificators.ToString(),
                AspectDirection = perfection.AspectDirection,
                TranslatorHouse = perfection.TranslatorHouse,
                TranslatorFigure = perfection.TranslatorHouse > 0 ? houseChart.GetHouseFigure(perfection.TranslatorHouse)?.Name : string.Empty,
                Notes = perfection.Notes.ToList(),
                QuerentHouse = perfection.QuerentHouse > 0 ? perfection.QuerentHouse : querentHouse,
                QuesitedHouse = perfection.QuesitedHouse > 0 ? perfection.QuesitedHouse : quesitedHouse,
                QuerentFigure = houseChart.GetHouseFigure(perfection.QuerentHouse > 0 ? perfection.QuerentHouse : querentHouse)?.Name ?? string.Empty,
                QuesitedFigure = houseChart.GetHouseFigure(perfection.QuesitedHouse > 0 ? perfection.QuesitedHouse : quesitedHouse)?.Name ?? string.Empty,
                MadeThroughCompany = perfection.MadeThroughCompany,
                BaseMode = perfection.BaseMode != PerfectionType.None ? perfection.BaseMode.ToString() : string.Empty,
                CompanyType = perfection.CompanyType.ToString(),
                CompanyTypeDescription = perfection.CompanyTypeDescription ?? string.Empty,
                FavorableScore = 0,
                UnfavorableScore = 0,
                NetScore = 0
            };
        }

        /// <summary>
        /// Analyze perfections with comprehensive scoring and aspect tracking
        /// </summary>
        /// <param name="request">Chart data and significator houses</param>
        /// <returns>Comprehensive perfection analysis with all aspects</returns>
        [HttpPost]
        [Route("perfection/analyze")]
        [ResponseType(typeof(PerfectionAnalysisResponse))]
        public HttpResponseMessage AnalyzePerfections([FromBody] PerfectionRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }

                // Create the house chart from the provided mothers
                var houseChart = new HouseChart();
                houseChart.SetFirstFourHousesAndCalculate(
                    request.Mothers.House1.HeadLine, request.Mothers.House1.NeckLine, request.Mothers.House1.BodyLine, request.Mothers.House1.FootLine,
                    request.Mothers.House2.HeadLine, request.Mothers.House2.NeckLine, request.Mothers.House2.BodyLine, request.Mothers.House2.FootLine,
                    request.Mothers.House3.HeadLine, request.Mothers.House3.NeckLine, request.Mothers.House3.BodyLine, request.Mothers.House3.FootLine,
                    request.Mothers.House4.HeadLine, request.Mothers.House4.NeckLine, request.Mothers.House4.BodyLine, request.Mothers.House4.FootLine
                );

                // Use the new AnalyzePerfections method
                var analysis = PerfectionCalculator.AnalyzePerfections(houseChart, request.QuerentHouse, request.QuesitedHouse);

                // Convert to response models with individual scores
                var perfectionResponses = new List<PerfectionResponse>();
                foreach (var perfection in analysis.Perfections)
                {
                    var perfectionResponse = ToPerfectionResponseWithoutScoring(perfection, houseChart, request.QuerentHouse, request.QuesitedHouse);
                    // Calculate individual scores for this perfection
                    perfectionResponse.FavorableScore = PerfectionCalculator.CalculateScore(perfection);
                    perfectionResponse.UnfavorableScore = PerfectionCalculator.CalculateUnfavorableScore(perfection);
                    perfectionResponse.NetScore = perfectionResponse.FavorableScore + perfectionResponse.UnfavorableScore;
                    perfectionResponses.Add(perfectionResponse);
                }

                var denialResponses = new List<PerfectionResponse>();
                foreach (var denial in analysis.Denials)
                {
                    var denialResponse = ToPerfectionResponseWithoutScoring(denial, houseChart, request.QuerentHouse, request.QuesitedHouse);
                    // Calculate individual scores for this denial
                    denialResponse.FavorableScore = PerfectionCalculator.CalculateScore(denial);
                    denialResponse.UnfavorableScore = PerfectionCalculator.CalculateUnfavorableScore(denial);
                    // For Impedition (Mode == None), always set -5
                    if (denial.Mode == PerfectionType.None)
                    {
                        denialResponse.UnfavorableScore = -5;
                        denialResponse.Success = true; // Set to true so it displays as a card
                    }
                    denialResponse.NetScore = denialResponse.FavorableScore + denialResponse.UnfavorableScore;
                    denialResponses.Add(denialResponse);
                }

                var positiveAspects = analysis.PositiveAspects.Select(a => 
                {
                    var aspectType = a.AspectType;
                    return new AspectRecordResponse
                    {
                        AspectType = aspectType.ToString(),
                        Direction = a.Direction,
                        FromHouse = a.FromHouse,
                        ToHouse = a.ToHouse,
                        Description = a.Description,
                        MadeThroughCompany = a.MadeThroughCompany,
                        IsMajorAspect = a.IsMajorAspect,
                        FavorableScore = CalculateAspectScore(aspectType, a.MadeThroughCompany),
                        UnfavorableScore = 0
                    };
                }).ToList();

                var negativeAspects = analysis.NegativeAspects.Select(a => 
                {
                    var aspectType = a.AspectType;
                    return new AspectRecordResponse
                    {
                        AspectType = aspectType.ToString(),
                        Direction = a.Direction,
                        FromHouse = a.FromHouse,
                        ToHouse = a.ToHouse,
                        Description = a.Description,
                        MadeThroughCompany = a.MadeThroughCompany,
                        IsMajorAspect = a.IsMajorAspect,
                        FavorableScore = 0,
                        UnfavorableScore = CalculateAspectUnfavorableScore(aspectType, a.MadeThroughCompany)
                    };
                }).ToList();

                var response = new PerfectionAnalysisResponse
                {
                    Success = true,
                    Message = $"Found {analysis.Perfections.Count} perfection(s), {analysis.Denials.Count} denial(s), {analysis.PositiveAspects.Count} positive aspect(s), and {analysis.NegativeAspects.Count} negative aspect(s).",
                    Perfections = perfectionResponses,
                    Denials = denialResponses,
                    PositiveAspects = positiveAspects,
                    NegativeAspects = negativeAspects,
                    TotalFavorableScore = analysis.TotalFavorableScore,
                    TotalUnfavorableScore = analysis.TotalUnfavorableScore,
                    NetScore = analysis.NetScore,
                    QuerentHouse = analysis.QuerentHouse,
                    QuesitedHouse = analysis.QuesitedHouse
                };

                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                var errorResponse = new PerfectionAnalysisResponse
                {
                    Success = false,
                    Message = $"Error analyzing perfections: {ex.Message}",
                    Perfections = new List<PerfectionResponse>(),
                    Denials = new List<PerfectionResponse>(),
                    PositiveAspects = new List<AspectRecordResponse>(),
                    NegativeAspects = new List<AspectRecordResponse>()
                };
                return Request.CreateResponse(HttpStatusCode.InternalServerError, errorResponse);
            }
        }

        // Helper to convert PerfectionResult to PerfectionResponse with scoring
        private static PerfectionResponse ToPerfectionResponse(PerfectionResult perfection, HouseChart houseChart, int querentHouse, int quesitedHouse)
        {
            // Calculate scores
            int favorableScore = PerfectionCalculator.CalculateScore(perfection);
            int unfavorableScore = PerfectionCalculator.CalculateTotalUnfavorableScore(houseChart, querentHouse, quesitedHouse);
            int netScore = favorableScore + unfavorableScore; // unfavorableScore is already negative

            // Build mode string with aspect direction if applicable
            string modeString = perfection.Mode.ToString();
            if (perfection.Mode == PerfectionType.None)
            {
                modeString = "Impedition";
            }
            else if (perfection.Mode == PerfectionType.Aspect && !string.IsNullOrEmpty(perfection.AspectDirection))
            {
                modeString = $"Aspect ({perfection.AspectDirection})";
            }

            return new PerfectionResponse
            {
                Mode = modeString,
                AspectBetweenSignificators = perfection.AspectBetweenSignificators.ToString(),
                AspectDirection = perfection.AspectDirection ?? string.Empty,
                TranslatorHouse = perfection.TranslatorHouse,
                TranslatorFigure = perfection.TranslatorHouse > 0 ? houseChart.GetHouseFigure(perfection.TranslatorHouse)?.Name : null,
                Notes = perfection.Notes,
                QuerentHouse = querentHouse,
                QuesitedHouse = quesitedHouse,
                QuerentFigure = houseChart.GetHouseFigure(querentHouse)?.Name,
                QuesitedFigure = houseChart.GetHouseFigure(quesitedHouse)?.Name,
                MadeThroughCompany = perfection.MadeThroughCompany,
                BaseMode = perfection.BaseMode != PerfectionType.None ? perfection.BaseMode.ToString() : null,
                CompanyType = perfection.CompanyType.ToString(),
                CompanyTypeDescription = perfection.CompanyTypeDescription ?? string.Empty,
                FavorableScore = favorableScore,
                UnfavorableScore = unfavorableScore,
                NetScore = netScore,
                Success = true,
                Message = "Perfection calculation completed successfully"
            };
        }

        // Helper method to calculate favorable score for an aspect (Table 6-3)
        private static int CalculateAspectScore(AspectType aspectType, bool madeThroughCompany)
        {
            int baseScore = 0;
            
            // Favorable aspects
            if (aspectType == AspectType.Trine)
                baseScore = 3;
            else if (aspectType == AspectType.Sextile)
                baseScore = 3;
            else
                return 0; // Not a favorable aspect
            
            // If made through company, subtract 1 (Table 6-3 rule)
            if (madeThroughCompany)
            {
                baseScore = Math.Max(0, baseScore - 1);
            }
            
            return baseScore;
        }

        // Helper method to calculate unfavorable score for an aspect (Table 6-3)
        private static int CalculateAspectUnfavorableScore(AspectType aspectType, bool madeThroughCompany)
        {
            int baseScore = 0;
            
            // Unfavorable aspects
            if (aspectType == AspectType.Opposition)
                baseScore = -4;
            else if (aspectType == AspectType.Square)
                baseScore = -3;
            else
                return 0; // Not an unfavorable aspect
            
            // If made through company, subtract 1 more (Table 6-3 rule)
            if (madeThroughCompany)
            {
                baseScore -= 1; // e.g., -3 becomes -4
            }
            
            return baseScore;
        }

        /// <summary>
        /// Calculate way of points for a chart
        /// </summary>
        /// <param name="request">Chart data (four mothers)</param>
        /// <returns>Way of points analysis for all four ways (Fire, Air, Water, Earth)</returns>
        [HttpPost]
        [Route("way-of-points")]
        [ResponseType(typeof(WayOfPointsAnalysisResponse))]
        public HttpResponseMessage CalculateWayOfPoints([FromBody] GenerateFourFiguresRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }

                // Create the house chart from the provided mothers
                var houseChart = new HouseChart();
                houseChart.SetFirstFourHousesAndCalculate(
                    request.House1.HeadLine, request.House1.NeckLine, request.House1.BodyLine, request.House1.FootLine,
                    request.House2.HeadLine, request.House2.NeckLine, request.House2.BodyLine, request.House2.FootLine,
                    request.House3.HeadLine, request.House3.NeckLine, request.House3.BodyLine, request.House3.FootLine,
                    request.House4.HeadLine, request.House4.NeckLine, request.House4.BodyLine, request.House4.FootLine
                );

                // Calculate all ways of points
                var analysis = WayOfPoints.CalculateAllWays(houseChart);

                // Convert to response models
                var response = new WayOfPointsAnalysisResponse
                {
                    Success = true,
                    Message = "Way of points calculation completed successfully",
                    FireWay = ToWayOfPointsResultResponse(analysis.FireWay),
                    AirWay = ToWayOfPointsResultResponse(analysis.AirWay),
                    WaterWay = ToWayOfPointsResultResponse(analysis.WaterWay),
                    EarthWay = ToWayOfPointsResultResponse(analysis.EarthWay)
                };

                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                var errorResponse = new WayOfPointsAnalysisResponse
                {
                    Success = false,
                    Message = $"Error calculating way of points: {ex.Message}",
                    FireWay = new WayOfPointsResultResponse(),
                    AirWay = new WayOfPointsResultResponse(),
                    WaterWay = new WayOfPointsResultResponse(),
                    EarthWay = new WayOfPointsResultResponse()
                };
                return Request.CreateResponse(HttpStatusCode.InternalServerError, errorResponse);
            }
        }

        // Helper to convert WayOfPointsResult to WayOfPointsResultResponse
        private static WayOfPointsResultResponse ToWayOfPointsResultResponse(WayOfPointsResult result)
        {
            return new WayOfPointsResultResponse
            {
                WayName = result.WayName ?? string.Empty,
                LineType = result.LineType ?? string.Empty,
                CanBeEstablished = result.CanBeEstablished,
                AllPaths = result.AllPaths.Select(p => new WayOfPointsPathResponse
                {
                    Houses = p.Houses ?? new List<int>(),
                    RowReached = p.RowReached,
                    PathType = p.PathType.ToString(),
                    EndpointHouse = p.EndpointHouse,
                    Description = p.Description ?? string.Empty
                }).ToList(),
                StrongPaths = result.StrongPaths.Select(p => new WayOfPointsPathResponse
                {
                    Houses = p.Houses ?? new List<int>(),
                    RowReached = p.RowReached,
                    PathType = p.PathType.ToString(),
                    EndpointHouse = p.EndpointHouse,
                    Description = p.Description ?? string.Empty
                }).ToList(),
                PassivePaths = result.PassivePaths.Select(p => new WayOfPointsPathResponse
                {
                    Houses = p.Houses ?? new List<int>(),
                    RowReached = p.RowReached,
                    PathType = p.PathType.ToString(),
                    EndpointHouse = p.EndpointHouse,
                    Description = p.Description ?? string.Empty
                }).ToList(),
                StrongPassivePaths = result.StrongPassivePaths.Select(p => new WayOfPointsPathResponse
                {
                    Houses = p.Houses ?? new List<int>(),
                    RowReached = p.RowReached,
                    PathType = p.PathType.ToString(),
                    EndpointHouse = p.EndpointHouse,
                    Description = p.Description ?? string.Empty
                }).ToList(),
                WeakerPassivePaths = result.WeakerPassivePaths.Select(p => new WayOfPointsPathResponse
                {
                    Houses = p.Houses ?? new List<int>(),
                    RowReached = p.RowReached,
                    PathType = p.PathType.ToString(),
                    EndpointHouse = p.EndpointHouse,
                    Description = p.Description ?? string.Empty
                }).ToList(),
                Notes = result.Notes ?? new List<string>()
            };
        }

        #endregion

        #region Reference Directory (HouseAndCourtDirectory JSON files)

        /// <summary>
        /// Get the full reference directory for the 12 houses (id, traditional name, governs, notes, etc.)
        /// </summary>
        [HttpGet]
        [Route("directory/houses")]
        [ResponseType(typeof(List<HouseDirectoryEntryResponse>))]
        public HttpResponseMessage GetHousesDirectory()
        {
            try
            {
                var entries = HouseDirectoryLoader.GetHouses();
                return Request.CreateResponse(HttpStatusCode.OK, entries);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new ErrorResponse
                {
                    Error = "DirectoryLoadFailed",
                    Message = $"Error loading house directory: {ex.Message}",
                    Timestamp = DateTime.UtcNow
                });
            }
        }

        /// <summary>
        /// Get a single house directory entry by id (1-12).
        /// </summary>
        [HttpGet]
        [Route("directory/houses/{id:int}")]
        [ResponseType(typeof(HouseDirectoryEntryResponse))]
        public HttpResponseMessage GetHouseDirectoryEntry(int id)
        {
            try
            {
                var entry = HouseDirectoryLoader.GetHouse(id);
                if (entry == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, new ErrorResponse
                    {
                        Error = "NotFound",
                        Message = $"No house directory entry with id {id}",
                        Timestamp = DateTime.UtcNow
                    });
                }
                return Request.CreateResponse(HttpStatusCode.OK, entry);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new ErrorResponse
                {
                    Error = "DirectoryLoadFailed",
                    Message = $"Error loading house directory entry: {ex.Message}",
                    Timestamp = DateTime.UtcNow
                });
            }
        }

        /// <summary>
        /// Get the full reference directory for the four court placements (Right Witness, Left Witness, Judge, Reconciler).
        /// </summary>
        [HttpGet]
        [Route("directory/courts")]
        [ResponseType(typeof(List<CourtDirectoryEntryResponse>))]
        public HttpResponseMessage GetCourtsDirectory()
        {
            try
            {
                var entries = HouseDirectoryLoader.GetCourts();
                return Request.CreateResponse(HttpStatusCode.OK, entries);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new ErrorResponse
                {
                    Error = "DirectoryLoadFailed",
                    Message = $"Error loading court directory: {ex.Message}",
                    Timestamp = DateTime.UtcNow
                });
            }
        }

        /// <summary>
        /// Get a single court placement by normalized key:
        /// <c>right-witness</c>, <c>left-witness</c>, <c>judge</c>, <c>reconciler</c>
        /// (with aliases <c>sentence</c> / <c>fallout</c> mapping to Reconciler).
        /// </summary>
        [HttpGet]
        [Route("directory/courts/{placement}")]
        [ResponseType(typeof(CourtDirectoryEntryResponse))]
        public HttpResponseMessage GetCourtDirectoryEntry(string placement)
        {
            try
            {
                var entry = HouseDirectoryLoader.GetCourt(placement);
                if (entry == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, new ErrorResponse
                    {
                        Error = "NotFound",
                        Message = $"No court directory entry for '{placement}'",
                        Timestamp = DateTime.UtcNow
                    });
                }
                return Request.CreateResponse(HttpStatusCode.OK, entry);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new ErrorResponse
                {
                    Error = "DirectoryLoadFailed",
                    Message = $"Error loading court directory entry: {ex.Message}",
                    Timestamp = DateTime.UtcNow
                });
            }
        }

        #endregion
    }
} 