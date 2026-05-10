using System;
using System.Collections.Generic;
using GeomancyAPI.Handlers;
using GeomancyAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace GeomancyWebUI.Controllers
{
    /// <summary>
    /// ASP.NET Core port of the F4.8 self-host controller. Same routes and same
    /// response shapes as <c>GeomancyAPI.Controllers.GeomancyController</c>.
    /// Both delegate to <see cref="GeomancyHandlers"/> for endpoint logic, so
    /// dev (F4.8 self-host on :5000) and production (Linux container) stay
    /// byte-identical.
    /// </summary>
    [ApiController]
    [Route("api/geomancy")]
    public class GeomancyController : ControllerBase
    {
        // ---------------------------------------------------------------------
        // Test
        // ---------------------------------------------------------------------

        [HttpGet("test")]
        public IActionResult Test() => Ok(GeomancyHandlers.Test());

        // ---------------------------------------------------------------------
        // Figures
        // ---------------------------------------------------------------------

        [HttpPost("figure")]
        public IActionResult GenerateFigure([FromBody] GenerateFigureRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                return Ok(GeomancyHandlers.GenerateFigure(request));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new GenerateFigureResponse
                {
                    Success = false,
                    Message = $"Error generating figure: {ex.Message}"
                });
            }
        }

        [HttpPost("figures")]
        public IActionResult GenerateFourFigures([FromBody] GenerateFourFiguresRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                return Ok(GeomancyHandlers.GenerateFourFigures(request));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new GenerateFourFiguresResponse
                {
                    Success = false,
                    Message = $"Error generating figures: {ex.Message}"
                });
            }
        }

        [HttpPost("chart")]
        public IActionResult GenerateHouseChart([FromBody] GenerateFourFiguresRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                return Ok(GeomancyHandlers.GenerateHouseChart(request));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorResponse
                {
                    Error = "Chart Generation Error",
                    Message = $"Error generating house chart: {ex.Message}",
                    Timestamp = DateTime.UtcNow
                });
            }
        }

        [HttpGet("figure/{figureName}")]
        public IActionResult GetFigureByName(string figureName)
        {
            try
            {
                return Ok(GeomancyHandlers.GetFigureByName(figureName));
            }
            catch (Exception ex)
            {
                return NotFound(new GenerateFigureResponse
                {
                    Success = false,
                    Message = $"Error retrieving figure '{figureName}': {ex.Message}"
                });
            }
        }

        [HttpGet("figures/all")]
        public IActionResult GetAllFigures()
        {
            try
            {
                return Ok(GeomancyHandlers.GetAllFigures());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorResponse
                {
                    Error = "Retrieval Error",
                    Message = $"Error retrieving all figures: {ex.Message}",
                    Timestamp = DateTime.UtcNow
                });
            }
        }

        [HttpGet("figure/pattern")]
        public IActionResult GetFigureByPattern(int headLine, int neckLine, int bodyLine, int footLine)
        {
            try
            {
                return Ok(GeomancyHandlers.GetFigureByPattern(headLine, neckLine, bodyLine, footLine));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return NotFound(new GenerateFigureResponse
                {
                    Success = false,
                    Message = $"Error retrieving figure by pattern: {ex.Message}"
                });
            }
        }

        // ---------------------------------------------------------------------
        // Charts via figure names
        // ---------------------------------------------------------------------

        [HttpGet("chart/figures")]
        public IActionResult GenerateChartFromFigureNames(string firstMother, string secondMother, string thirdMother, string fourthMother)
        {
            try
            {
                return Ok(GeomancyHandlers.GenerateChartFromFigureNames(firstMother, secondMother, thirdMother, fourthMother));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorResponse
                {
                    Error = "Chart Generation Error",
                    Message = $"Error generating house chart from figure names: {ex.Message}",
                    Timestamp = DateTime.UtcNow
                });
            }
        }

        [HttpGet("chart/figures/comma")]
        public IActionResult GenerateChartFromCommaSeparatedFigures(string mothers)
        {
            try
            {
                return Ok(GeomancyHandlers.GenerateChartFromCommaSeparatedFigures(mothers));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorResponse
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

        [HttpPost("aspects")]
        public IActionResult GetChartAspects([FromBody] GenerateFourFiguresRequest request, [FromQuery] string minAspect = "Sextile")
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                return Ok(GeomancyHandlers.GetChartAspects(request, minAspect));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new AspectsResponse
                {
                    Success = false,
                    Message = $"Error calculating aspects: {ex.Message}"
                });
            }
        }

        [HttpGet("aspects/figures")]
        public IActionResult GetAspectsFromFigures(string firstMother, string secondMother, string thirdMother, string fourthMother, string minAspect = "Sextile")
        {
            try
            {
                return Ok(GeomancyHandlers.GetAspectsFromFigures(firstMother, secondMother, thirdMother, fourthMother, minAspect));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new AspectsResponse
                {
                    Success = false,
                    Message = $"Error calculating aspects: {ex.Message}"
                });
            }
        }

        [HttpGet("aspects/pattern")]
        public IActionResult GetAspectsFromPattern(
            int h1Head, int h1Neck, int h1Body, int h1Foot,
            int h2Head, int h2Neck, int h2Body, int h2Foot,
            int h3Head, int h3Neck, int h3Body, int h3Foot,
            int h4Head, int h4Neck, int h4Body, int h4Foot,
            string minAspect = "Sextile")
        {
            try
            {
                return Ok(GeomancyHandlers.GetAspectsFromPattern(
                    h1Head, h1Neck, h1Body, h1Foot,
                    h2Head, h2Neck, h2Body, h2Foot,
                    h3Head, h3Neck, h3Body, h3Foot,
                    h4Head, h4Neck, h4Body, h4Foot,
                    minAspect));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new AspectsResponse
                {
                    Success = false,
                    Message = $"Error calculating aspects: {ex.Message}"
                });
            }
        }

        // ---------------------------------------------------------------------
        // Perfections
        // ---------------------------------------------------------------------

        [HttpPost("perfection")]
        public IActionResult CalculatePerfection([FromBody] PerfectionRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                return Ok(GeomancyHandlers.CalculatePerfection(request));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new MultiplePerfectionsResponse
                {
                    Success = false,
                    Message = $"Error calculating perfection: {ex.Message}",
                    Perfections = new List<PerfectionResponse>(),
                    TotalPerfections = 0
                });
            }
        }

        [HttpGet("perfection/figures")]
        public IActionResult CalculatePerfectionFromFigures(
            string firstMother, string secondMother, string thirdMother, string fourthMother,
            int querentHouse, int quesitedHouse)
        {
            try
            {
                return Ok(GeomancyHandlers.CalculatePerfectionFromFigures(firstMother, secondMother, thirdMother, fourthMother, querentHouse, quesitedHouse));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new PerfectionResponse
                {
                    Success = false,
                    Message = $"Error calculating perfection: {ex.Message}"
                });
            }
        }

        [HttpGet("perfection/pattern")]
        public IActionResult CalculatePerfectionFromPattern(
            int h1Head, int h1Neck, int h1Body, int h1Foot,
            int h2Head, int h2Neck, int h2Body, int h2Foot,
            int h3Head, int h3Neck, int h3Body, int h3Foot,
            int h4Head, int h4Neck, int h4Body, int h4Foot,
            int querentHouse, int quesitedHouse)
        {
            try
            {
                return Ok(GeomancyHandlers.CalculatePerfectionFromPattern(
                    h1Head, h1Neck, h1Body, h1Foot,
                    h2Head, h2Neck, h2Body, h2Foot,
                    h3Head, h3Neck, h3Body, h3Foot,
                    h4Head, h4Neck, h4Body, h4Foot,
                    querentHouse, quesitedHouse));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new PerfectionResponse
                {
                    Success = false,
                    Message = $"Error calculating perfection: {ex.Message}"
                });
            }
        }

        [HttpGet("perfections/figures")]
        public IActionResult GetAllPerfectionsFromFigures(string firstMother, string secondMother, string thirdMother, string fourthMother)
        {
            try
            {
                return Ok(GeomancyHandlers.GetAllPerfectionsFromFigures(firstMother, secondMother, thirdMother, fourthMother));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new MultiplePerfectionsResponse
                {
                    Success = false,
                    Message = $"Error calculating perfections: {ex.Message}"
                });
            }
        }

        [HttpGet("aspect-analysis/figures")]
        public IActionResult GetAspectAnalysisFromFigures(string firstMother, string secondMother, string thirdMother, string fourthMother)
        {
            try
            {
                return Ok(GeomancyHandlers.GetAspectAnalysisFromFigures(firstMother, secondMother, thirdMother, fourthMother));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new AspectAnalysisResponse
                {
                    Success = false,
                    Message = $"Error analyzing aspects: {ex.Message}"
                });
            }
        }

        [HttpGet("perfections/figures/pair")]
        public IActionResult GetAllPerfectionsForPair(
            string firstMother, string secondMother, string thirdMother, string fourthMother,
            int querentHouse, int quesitedHouse)
        {
            try
            {
                return Ok(GeomancyHandlers.GetAllPerfectionsForPair(firstMother, secondMother, thirdMother, fourthMother, querentHouse, quesitedHouse));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new MultiplePerfectionsResponse
                {
                    Success = false,
                    Message = $"Error calculating perfections: {ex.Message}"
                });
            }
        }

        [HttpGet("indentation")]
        public IActionResult GetIndentation(
            string firstMother, string secondMother, string thirdMother, string fourthMother,
            int querentHouse, int quesitedHouse, int? translatorHouse = null)
        {
            try
            {
                return Ok(GeomancyHandlers.GetIndentation(firstMother, secondMother, thirdMother, fourthMother, querentHouse, quesitedHouse, translatorHouse));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorResponse
                {
                    Error = "Indentation Calculation Error",
                    Message = $"Error calculating indentation: {ex.Message}",
                    Timestamp = DateTime.UtcNow
                });
            }
        }

        [HttpPost("perfection/analyze")]
        public IActionResult AnalyzePerfections([FromBody] PerfectionRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                return Ok(GeomancyHandlers.AnalyzePerfections(request));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new PerfectionAnalysisResponse
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

        [HttpPost("way-of-points")]
        public IActionResult CalculateWayOfPoints([FromBody] GenerateFourFiguresRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                return Ok(GeomancyHandlers.CalculateWayOfPoints(request));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new WayOfPointsAnalysisResponse
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

        [HttpGet("directory/houses")]
        public IActionResult GetHousesDirectory()
        {
            try
            {
                return Ok(GeomancyHandlers.GetHousesDirectory());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorResponse
                {
                    Error = "DirectoryLoadFailed",
                    Message = $"Error loading house directory: {ex.Message}",
                    Timestamp = DateTime.UtcNow
                });
            }
        }

        [HttpGet("directory/houses/{id:int}")]
        public IActionResult GetHouseDirectoryEntry(int id)
        {
            try
            {
                var entry = GeomancyHandlers.GetHouseDirectoryEntry(id);
                if (entry == null)
                {
                    return NotFound(new ErrorResponse
                    {
                        Error = "NotFound",
                        Message = $"No house directory entry with id {id}",
                        Timestamp = DateTime.UtcNow
                    });
                }
                return Ok(entry);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorResponse
                {
                    Error = "DirectoryLoadFailed",
                    Message = $"Error loading house directory entry: {ex.Message}",
                    Timestamp = DateTime.UtcNow
                });
            }
        }

        [HttpGet("directory/courts")]
        public IActionResult GetCourtsDirectory()
        {
            try
            {
                return Ok(GeomancyHandlers.GetCourtsDirectory());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorResponse
                {
                    Error = "DirectoryLoadFailed",
                    Message = $"Error loading court directory: {ex.Message}",
                    Timestamp = DateTime.UtcNow
                });
            }
        }

        [HttpGet("directory/courts/{placement}")]
        public IActionResult GetCourtDirectoryEntry(string placement)
        {
            try
            {
                var entry = GeomancyHandlers.GetCourtDirectoryEntry(placement);
                if (entry == null)
                {
                    return NotFound(new ErrorResponse
                    {
                        Error = "NotFound",
                        Message = $"No court directory entry for '{placement}'",
                        Timestamp = DateTime.UtcNow
                    });
                }
                return Ok(entry);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorResponse
                {
                    Error = "DirectoryLoadFailed",
                    Message = $"Error loading court directory entry: {ex.Message}",
                    Timestamp = DateTime.UtcNow
                });
            }
        }

        [HttpGet("way-of-points/elements")]
        public IActionResult GetWayOfPointsElementsDirectory()
        {
            try
            {
                return Ok(GeomancyHandlers.GetWayOfPointsElementsDirectory());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorResponse
                {
                    Error = "DirectoryLoadFailed",
                    Message = $"Error loading Way Of Points elements directory: {ex.Message}",
                    Timestamp = DateTime.UtcNow
                });
            }
        }

        [HttpGet("way-of-points/elements/{key}")]
        public IActionResult GetWayOfPointsElementEntry(string key)
        {
            try
            {
                var entry = GeomancyHandlers.GetWayOfPointsElementEntry(key);
                if (entry == null)
                {
                    return NotFound(new ErrorResponse
                    {
                        Error = "NotFound",
                        Message = $"No Way Of Points element entry for '{key}'",
                        Timestamp = DateTime.UtcNow
                    });
                }
                return Ok(entry);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorResponse
                {
                    Error = "DirectoryLoadFailed",
                    Message = $"Error loading Way Of Points element entry: {ex.Message}",
                    Timestamp = DateTime.UtcNow
                });
            }
        }

        [HttpGet("way-of-points/path-types")]
        public IActionResult GetWayOfPointsPathTypesDirectory()
        {
            try
            {
                return Ok(GeomancyHandlers.GetWayOfPointsPathTypesDirectory());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorResponse
                {
                    Error = "DirectoryLoadFailed",
                    Message = $"Error loading Way Of Points path types directory: {ex.Message}",
                    Timestamp = DateTime.UtcNow
                });
            }
        }

        [HttpGet("way-of-points/path-types/{id}")]
        public IActionResult GetWayOfPointsPathTypeEntry(string id)
        {
            try
            {
                var entry = GeomancyHandlers.GetWayOfPointsPathTypeEntry(id);
                if (entry == null)
                {
                    return NotFound(new ErrorResponse
                    {
                        Error = "NotFound",
                        Message = $"No Way Of Points path type entry for '{id}'",
                        Timestamp = DateTime.UtcNow
                    });
                }
                return Ok(entry);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorResponse
                {
                    Error = "DirectoryLoadFailed",
                    Message = $"Error loading Way Of Points path type entry: {ex.Message}",
                    Timestamp = DateTime.UtcNow
                });
            }
        }
    }
}
