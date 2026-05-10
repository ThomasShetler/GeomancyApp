using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using GeomancyAPI.Handlers;
using GeomancyAPI.Models;

namespace GeomancyAPI.Controllers
{
    /// <summary>
    /// .NET Framework 4.8 self-host controller. After the Geomancy.Api.Handlers
    /// extraction every action is a thin shim that delegates to the matching
    /// static handler function. Same routes, same response shapes as before -
    /// the .NET 8 Web project's controller delegates to the same handlers so
    /// dev (F4.8 self-host on :5000) and prod (Linux container) stay byte-identical.
    /// </summary>
    [RoutePrefix("api/geomancy")]
    public class GeomancyController : ApiController
    {
        // ---------------------------------------------------------------------
        // Test
        // ---------------------------------------------------------------------

        [HttpGet]
        [Route("test")]
        public HttpResponseMessage Test()
        {
            return Request.CreateResponse(HttpStatusCode.OK, GeomancyHandlers.Test());
        }

        // ---------------------------------------------------------------------
        // Figures
        // ---------------------------------------------------------------------

        [HttpPost]
        [Route("figure")]
        [ResponseType(typeof(GenerateFigureResponse))]
        public HttpResponseMessage GenerateFigure([FromBody] GenerateFigureRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);

                return Request.CreateResponse(HttpStatusCode.OK, GeomancyHandlers.GenerateFigure(request));
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new GenerateFigureResponse
                {
                    Success = false,
                    Message = $"Error generating figure: {ex.Message}"
                });
            }
        }

        [HttpPost]
        [Route("figures")]
        [ResponseType(typeof(GenerateFourFiguresResponse))]
        public HttpResponseMessage GenerateFourFigures([FromBody] GenerateFourFiguresRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);

                return Request.CreateResponse(HttpStatusCode.OK, GeomancyHandlers.GenerateFourFigures(request));
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new GenerateFourFiguresResponse
                {
                    Success = false,
                    Message = $"Error generating figures: {ex.Message}"
                });
            }
        }

        [HttpPost]
        [Route("chart")]
        [ResponseType(typeof(HouseChartResponse))]
        public HttpResponseMessage GenerateHouseChart([FromBody] GenerateFourFiguresRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);

                return Request.CreateResponse(HttpStatusCode.OK, GeomancyHandlers.GenerateHouseChart(request));
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new ErrorResponse
                {
                    Error = "Chart Generation Error",
                    Message = $"Error generating house chart: {ex.Message}",
                    Timestamp = DateTime.UtcNow
                });
            }
        }

        [HttpGet]
        [Route("figure/{figureName}")]
        [ResponseType(typeof(GenerateFigureResponse))]
        public HttpResponseMessage GetFigureByName(string figureName)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, GeomancyHandlers.GetFigureByName(figureName));
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new GenerateFigureResponse
                {
                    Success = false,
                    Message = $"Error retrieving figure '{figureName}': {ex.Message}"
                });
            }
        }

        [HttpGet]
        [Route("figures/all")]
        [ResponseType(typeof(List<FigureResponse>))]
        public HttpResponseMessage GetAllFigures()
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, GeomancyHandlers.GetAllFigures());
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new ErrorResponse
                {
                    Error = "Retrieval Error",
                    Message = $"Error retrieving all figures: {ex.Message}",
                    Timestamp = DateTime.UtcNow
                });
            }
        }

        [HttpGet]
        [Route("figure/pattern")]
        [ResponseType(typeof(GenerateFigureResponse))]
        public HttpResponseMessage GetFigureByPattern(int headLine, int neckLine, int bodyLine, int footLine)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, GeomancyHandlers.GetFigureByPattern(headLine, neckLine, bodyLine, footLine));
            }
            catch (ArgumentException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new GenerateFigureResponse
                {
                    Success = false,
                    Message = $"Error retrieving figure by pattern: {ex.Message}"
                });
            }
        }

        // ---------------------------------------------------------------------
        // Charts via figure names
        // ---------------------------------------------------------------------

        [HttpGet]
        [Route("chart/figures")]
        [ResponseType(typeof(HouseChartResponse))]
        public HttpResponseMessage GenerateChartFromFigureNames(string firstMother, string secondMother, string thirdMother, string fourthMother)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                    GeomancyHandlers.GenerateChartFromFigureNames(firstMother, secondMother, thirdMother, fourthMother));
            }
            catch (ArgumentException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new ErrorResponse
                {
                    Error = "Chart Generation Error",
                    Message = $"Error generating house chart from figure names: {ex.Message}",
                    Timestamp = DateTime.UtcNow
                });
            }
        }

        [HttpGet]
        [Route("chart/figures/comma")]
        [ResponseType(typeof(HouseChartResponse))]
        public HttpResponseMessage GenerateChartFromCommaSeparatedFigures(string mothers)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, GeomancyHandlers.GenerateChartFromCommaSeparatedFigures(mothers));
            }
            catch (ArgumentException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new ErrorResponse
                {
                    Error = "Chart Generation Error",
                    Message = $"Error generating house chart from comma-separated figures: {ex.Message}",
                    Timestamp = DateTime.UtcNow
                });
            }
        }

        // ---------------------------------------------------------------------
        // Aspects
        // ---------------------------------------------------------------------

        [HttpPost]
        [Route("aspects")]
        [ResponseType(typeof(AspectsResponse))]
        public HttpResponseMessage GetChartAspects([FromBody] GenerateFourFiguresRequest request, string minAspect = "Sextile")
        {
            try
            {
                if (!ModelState.IsValid)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);

                return Request.CreateResponse(HttpStatusCode.OK, GeomancyHandlers.GetChartAspects(request, minAspect));
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new AspectsResponse
                {
                    Success = false,
                    Message = $"Error calculating aspects: {ex.Message}"
                });
            }
        }

        [HttpGet]
        [Route("aspects/figures")]
        [ResponseType(typeof(AspectsResponse))]
        public HttpResponseMessage GetAspectsFromFigures(string firstMother, string secondMother, string thirdMother, string fourthMother, string minAspect = "Sextile")
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                    GeomancyHandlers.GetAspectsFromFigures(firstMother, secondMother, thirdMother, fourthMother, minAspect));
            }
            catch (ArgumentException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new AspectsResponse
                {
                    Success = false,
                    Message = $"Error calculating aspects: {ex.Message}"
                });
            }
        }

        [HttpGet]
        [Route("aspects/pattern")]
        [ResponseType(typeof(AspectsResponse))]
        public HttpResponseMessage GetAspectsFromPattern(
            int h1Head, int h1Neck, int h1Body, int h1Foot,
            int h2Head, int h2Neck, int h2Body, int h2Foot,
            int h3Head, int h3Neck, int h3Body, int h3Foot,
            int h4Head, int h4Neck, int h4Body, int h4Foot,
            string minAspect = "Sextile")
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                    GeomancyHandlers.GetAspectsFromPattern(
                        h1Head, h1Neck, h1Body, h1Foot,
                        h2Head, h2Neck, h2Body, h2Foot,
                        h3Head, h3Neck, h3Body, h3Foot,
                        h4Head, h4Neck, h4Body, h4Foot,
                        minAspect));
            }
            catch (ArgumentException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new AspectsResponse
                {
                    Success = false,
                    Message = $"Error calculating aspects: {ex.Message}"
                });
            }
        }

        // ---------------------------------------------------------------------
        // Perfections
        // ---------------------------------------------------------------------

        [HttpPost]
        [Route("perfection")]
        [ResponseType(typeof(MultiplePerfectionsResponse))]
        public HttpResponseMessage CalculatePerfection([FromBody] PerfectionRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);

                return Request.CreateResponse(HttpStatusCode.OK, GeomancyHandlers.CalculatePerfection(request));
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new MultiplePerfectionsResponse
                {
                    Success = false,
                    Message = $"Error calculating perfection: {ex.Message}",
                    Perfections = new List<PerfectionResponse>(),
                    TotalPerfections = 0
                });
            }
        }

        [HttpGet]
        [Route("perfection/figures")]
        [ResponseType(typeof(PerfectionResponse))]
        public HttpResponseMessage CalculatePerfectionFromFigures(
            string firstMother, string secondMother, string thirdMother, string fourthMother,
            int querentHouse, int quesitedHouse)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                    GeomancyHandlers.CalculatePerfectionFromFigures(firstMother, secondMother, thirdMother, fourthMother, querentHouse, quesitedHouse));
            }
            catch (ArgumentException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new PerfectionResponse
                {
                    Success = false,
                    Message = $"Error calculating perfection: {ex.Message}"
                });
            }
        }

        [HttpGet]
        [Route("perfection/pattern")]
        [ResponseType(typeof(PerfectionResponse))]
        public HttpResponseMessage CalculatePerfectionFromPattern(
            int h1Head, int h1Neck, int h1Body, int h1Foot,
            int h2Head, int h2Neck, int h2Body, int h2Foot,
            int h3Head, int h3Neck, int h3Body, int h3Foot,
            int h4Head, int h4Neck, int h4Body, int h4Foot,
            int querentHouse, int quesitedHouse)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                    GeomancyHandlers.CalculatePerfectionFromPattern(
                        h1Head, h1Neck, h1Body, h1Foot,
                        h2Head, h2Neck, h2Body, h2Foot,
                        h3Head, h3Neck, h3Body, h3Foot,
                        h4Head, h4Neck, h4Body, h4Foot,
                        querentHouse, quesitedHouse));
            }
            catch (ArgumentException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new PerfectionResponse
                {
                    Success = false,
                    Message = $"Error calculating perfection: {ex.Message}"
                });
            }
        }

        [HttpGet]
        [Route("perfections/figures")]
        [ResponseType(typeof(MultiplePerfectionsResponse))]
        public HttpResponseMessage GetAllPerfectionsFromFigures(string firstMother, string secondMother, string thirdMother, string fourthMother)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                    GeomancyHandlers.GetAllPerfectionsFromFigures(firstMother, secondMother, thirdMother, fourthMother));
            }
            catch (ArgumentException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new MultiplePerfectionsResponse
                {
                    Success = false,
                    Message = $"Error calculating perfections: {ex.Message}"
                });
            }
        }

        [HttpGet]
        [Route("aspect-analysis/figures")]
        [ResponseType(typeof(AspectAnalysisResponse))]
        public HttpResponseMessage GetAspectAnalysisFromFigures(string firstMother, string secondMother, string thirdMother, string fourthMother)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                    GeomancyHandlers.GetAspectAnalysisFromFigures(firstMother, secondMother, thirdMother, fourthMother));
            }
            catch (ArgumentException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new AspectAnalysisResponse
                {
                    Success = false,
                    Message = $"Error analyzing aspects: {ex.Message}"
                });
            }
        }

        [HttpGet]
        [Route("perfections/figures/pair")]
        [ResponseType(typeof(MultiplePerfectionsResponse))]
        public HttpResponseMessage GetAllPerfectionsForPair(
            string firstMother, string secondMother, string thirdMother, string fourthMother,
            int querentHouse, int quesitedHouse)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                    GeomancyHandlers.GetAllPerfectionsForPair(firstMother, secondMother, thirdMother, fourthMother, querentHouse, quesitedHouse));
            }
            catch (ArgumentException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new MultiplePerfectionsResponse
                {
                    Success = false,
                    Message = $"Error calculating perfections: {ex.Message}"
                });
            }
        }

        [HttpGet]
        [Route("indentation")]
        [ResponseType(typeof(IndentationResponse))]
        public HttpResponseMessage GetIndentation(
            string firstMother, string secondMother, string thirdMother, string fourthMother,
            int querentHouse, int quesitedHouse, int? translatorHouse = null)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                    GeomancyHandlers.GetIndentation(firstMother, secondMother, thirdMother, fourthMother, querentHouse, quesitedHouse, translatorHouse));
            }
            catch (ArgumentException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new ErrorResponse
                {
                    Error = "Indentation Calculation Error",
                    Message = $"Error calculating indentation: {ex.Message}",
                    Timestamp = DateTime.UtcNow
                });
            }
        }

        [HttpPost]
        [Route("perfection/analyze")]
        [ResponseType(typeof(PerfectionAnalysisResponse))]
        public HttpResponseMessage AnalyzePerfections([FromBody] PerfectionRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);

                return Request.CreateResponse(HttpStatusCode.OK, GeomancyHandlers.AnalyzePerfections(request));
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new PerfectionAnalysisResponse
                {
                    Success = false,
                    Message = $"Error analyzing perfections: {ex.Message}",
                    Perfections = new List<PerfectionResponse>(),
                    Denials = new List<PerfectionResponse>(),
                    PositiveAspects = new List<AspectRecordResponse>(),
                    NegativeAspects = new List<AspectRecordResponse>()
                });
            }
        }

        // ---------------------------------------------------------------------
        // Way Of Points
        // ---------------------------------------------------------------------

        [HttpPost]
        [Route("way-of-points")]
        [ResponseType(typeof(WayOfPointsAnalysisResponse))]
        public HttpResponseMessage CalculateWayOfPoints([FromBody] GenerateFourFiguresRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);

                return Request.CreateResponse(HttpStatusCode.OK, GeomancyHandlers.CalculateWayOfPoints(request));
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new WayOfPointsAnalysisResponse
                {
                    Success = false,
                    Message = $"Error calculating way of points: {ex.Message}",
                    FireWay = new WayOfPointsResultResponse(),
                    AirWay = new WayOfPointsResultResponse(),
                    WaterWay = new WayOfPointsResultResponse(),
                    EarthWay = new WayOfPointsResultResponse()
                });
            }
        }

        // ---------------------------------------------------------------------
        // Reference directories (House / Court / Way Of Points)
        // ---------------------------------------------------------------------

        [HttpGet]
        [Route("directory/houses")]
        [ResponseType(typeof(List<HouseDirectoryEntryResponse>))]
        public HttpResponseMessage GetHousesDirectory()
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, GeomancyHandlers.GetHousesDirectory());
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

        [HttpGet]
        [Route("directory/houses/{id:int}")]
        [ResponseType(typeof(HouseDirectoryEntryResponse))]
        public HttpResponseMessage GetHouseDirectoryEntry(int id)
        {
            try
            {
                var entry = GeomancyHandlers.GetHouseDirectoryEntry(id);
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

        [HttpGet]
        [Route("directory/courts")]
        [ResponseType(typeof(List<CourtDirectoryEntryResponse>))]
        public HttpResponseMessage GetCourtsDirectory()
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, GeomancyHandlers.GetCourtsDirectory());
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

        [HttpGet]
        [Route("directory/courts/{placement}")]
        [ResponseType(typeof(CourtDirectoryEntryResponse))]
        public HttpResponseMessage GetCourtDirectoryEntry(string placement)
        {
            try
            {
                var entry = GeomancyHandlers.GetCourtDirectoryEntry(placement);
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

        [HttpGet]
        [Route("way-of-points/elements")]
        [ResponseType(typeof(List<WayOfPointsElementEntryResponse>))]
        public HttpResponseMessage GetWayOfPointsElementsDirectory()
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, GeomancyHandlers.GetWayOfPointsElementsDirectory());
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new ErrorResponse
                {
                    Error = "DirectoryLoadFailed",
                    Message = $"Error loading Way Of Points elements directory: {ex.Message}",
                    Timestamp = DateTime.UtcNow
                });
            }
        }

        [HttpGet]
        [Route("way-of-points/elements/{key}")]
        [ResponseType(typeof(WayOfPointsElementEntryResponse))]
        public HttpResponseMessage GetWayOfPointsElementEntry(string key)
        {
            try
            {
                var entry = GeomancyHandlers.GetWayOfPointsElementEntry(key);
                if (entry == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, new ErrorResponse
                    {
                        Error = "NotFound",
                        Message = $"No Way Of Points element entry for '{key}'",
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
                    Message = $"Error loading Way Of Points element entry: {ex.Message}",
                    Timestamp = DateTime.UtcNow
                });
            }
        }

        [HttpGet]
        [Route("way-of-points/path-types")]
        [ResponseType(typeof(List<WayOfPointsPathTypeEntryResponse>))]
        public HttpResponseMessage GetWayOfPointsPathTypesDirectory()
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, GeomancyHandlers.GetWayOfPointsPathTypesDirectory());
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new ErrorResponse
                {
                    Error = "DirectoryLoadFailed",
                    Message = $"Error loading Way Of Points path types directory: {ex.Message}",
                    Timestamp = DateTime.UtcNow
                });
            }
        }

        [HttpGet]
        [Route("way-of-points/path-types/{id}")]
        [ResponseType(typeof(WayOfPointsPathTypeEntryResponse))]
        public HttpResponseMessage GetWayOfPointsPathTypeEntry(string id)
        {
            try
            {
                var entry = GeomancyHandlers.GetWayOfPointsPathTypeEntry(id);
                if (entry == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, new ErrorResponse
                    {
                        Error = "NotFound",
                        Message = $"No Way Of Points path type entry for '{id}'",
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
                    Message = $"Error loading Way Of Points path type entry: {ex.Message}",
                    Timestamp = DateTime.UtcNow
                });
            }
        }
    }
}
