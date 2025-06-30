using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using GeomancyAPI.Models;
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
        /// <returns>Perfection analysis result</returns>
        [HttpPost]
        [Route("perfection")]
        [ResponseType(typeof(PerfectionResponse))]
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

                // Calculate perfection
                var perfection = PerfectionCalculator.Find(houseChart, request.QuerentHouse, request.QuesitedHouse);

                // Convert to response model
                var response = new PerfectionResponse
                {
                    Mode = perfection.Mode.ToString(),
                    AspectBetweenSignificators = perfection.AspectBetweenSignificators.ToString(),
                    TranslatorHouse = perfection.TranslatorHouse,
                    TranslatorFigure = perfection.TranslatorHouse > 0 ? houseChart.GetHouseFigure(perfection.TranslatorHouse)?.Name : null,
                    Notes = perfection.Notes,
                    QuerentHouse = request.QuerentHouse,
                    QuesitedHouse = request.QuesitedHouse,
                    QuerentFigure = houseChart.GetHouseFigure(request.QuerentHouse)?.Name,
                    QuesitedFigure = houseChart.GetHouseFigure(request.QuesitedHouse)?.Name,
                    Success = true,
                    Message = "Perfection calculation completed successfully"
                };

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

                // Convert to response model
                var response = new PerfectionResponse
                {
                    Mode = perfection.Mode.ToString(),
                    AspectBetweenSignificators = perfection.AspectBetweenSignificators.ToString(),
                    TranslatorHouse = perfection.TranslatorHouse,
                    TranslatorFigure = perfection.TranslatorHouse > 0 ? houseChart.GetHouseFigure(perfection.TranslatorHouse)?.Name : null,
                    Notes = perfection.Notes,
                    QuerentHouse = querentHouse,
                    QuesitedHouse = quesitedHouse,
                    QuerentFigure = houseChart.GetHouseFigure(querentHouse)?.Name,
                    QuesitedFigure = houseChart.GetHouseFigure(quesitedHouse)?.Name,
                    Success = true,
                    Message = "Perfection calculation completed successfully"
                };

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

                // Convert to response model
                var response = new PerfectionResponse
                {
                    Mode = perfection.Mode.ToString(),
                    AspectBetweenSignificators = perfection.AspectBetweenSignificators.ToString(),
                    TranslatorHouse = perfection.TranslatorHouse,
                    TranslatorFigure = perfection.TranslatorHouse > 0 ? houseChart.GetHouseFigure(perfection.TranslatorHouse)?.Name : null,
                    Notes = perfection.Notes,
                    QuerentHouse = querentHouse,
                    QuesitedHouse = quesitedHouse,
                    QuerentFigure = houseChart.GetHouseFigure(querentHouse)?.Name,
                    QuesitedFigure = houseChart.GetHouseFigure(quesitedHouse)?.Name,
                    Success = true,
                    Message = "Perfection calculation completed successfully"
                };

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

                // Convert to response model
                var perfectionList = perfections.Select(p => new PerfectionResponse
                {
                    Mode = p.Mode.ToString(),
                    AspectBetweenSignificators = p.AspectBetweenSignificators.ToString(),
                    TranslatorHouse = p.TranslatorHouse,
                    TranslatorFigure = p.TranslatorHouse > 0 ? houseChart.GetHouseFigure(p.TranslatorHouse)?.Name : null,
                    Notes = p.Notes,
                    QuerentHouse = p.QuerentHouse,
                    QuesitedHouse = p.QuesitedHouse,
                    QuerentFigure = houseChart.GetHouseFigure(p.QuerentHouse)?.Name,
                    QuesitedFigure = houseChart.GetHouseFigure(p.QuesitedHouse)?.Name,
                    Success = true,
                    Message = "Perfection found"
                }).ToList();

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
                    ToFigure = houseChart.GetHouseFigure(d.To)?.Name
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

                // Convert to response model
                var perfectionList = perfections.Select(p => new PerfectionResponse
                {
                    Mode = p.Mode.ToString(),
                    AspectBetweenSignificators = p.AspectBetweenSignificators.ToString(),
                    TranslatorHouse = p.TranslatorHouse,
                    TranslatorFigure = p.TranslatorHouse > 0 ? houseChart.GetHouseFigure(p.TranslatorHouse)?.Name : null,
                    Notes = p.Notes,
                    QuerentHouse = querentHouse,
                    QuesitedHouse = quesitedHouse,
                    QuerentFigure = houseChart.GetHouseFigure(querentHouse)?.Name,
                    QuesitedFigure = houseChart.GetHouseFigure(quesitedHouse)?.Name,
                    Indentation = ToIndentScoreResponse(ChartAspectAnalysis.ComputeIndent(houseChart, querentHouse, quesitedHouse)),
                    TranslatorIndentation = (p.TranslatorHouse > 0)
                        ? ToIndentScoreResponse(ChartAspectAnalysis.ComputeIndent(houseChart, querentHouse, p.TranslatorHouse))
                        : null,
                    Success = true,
                    Message = "Perfection found"
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

            return new HouseChartResponse
            {
                Houses = houses,
                RightWitness = houseChart.RightWitness != null ? ConvertToFigureResponse(houseChart.RightWitness) : null,
                LeftWitness = houseChart.LeftWitness != null ? ConvertToFigureResponse(houseChart.LeftWitness) : null,
                Judge = houseChart.Judge != null ? ConvertToFigureResponse(houseChart.Judge) : null,
                Sentence = houseChart.Sentence != null ? ConvertToFigureResponse(houseChart.Sentence) : null,
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

        #endregion
    }
} 