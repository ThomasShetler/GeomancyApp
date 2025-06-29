using Microsoft.AspNetCore.Mvc;
using GeomancyAPI.Models;
using GeomancyAPI.Services;

namespace GeomancyAPI.Controllers
{
    /// <summary>
    /// API controller for geomantic figure and house chart generation
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class GeomanticController : ControllerBase
    {
        private readonly GeomanticService _geomanticService;

        public GeomanticController(GeomanticService geomanticService)
        {
            _geomanticService = geomanticService;
        }

        /// <summary>
        /// Generate a single geomantic figure from elemental pattern
        /// </summary>
        /// <param name="request">The elemental pattern for the figure</param>
        /// <returns>The generated geomantic figure</returns>
        /// <response code="200">Returns the generated figure</response>
        /// <response code="400">If the request is invalid</response>
        [HttpPost("figure")]
        [ProducesResponseType(typeof(ApiResponse<FigureResponse>), 200)]
        [ProducesResponseType(typeof(ApiResponse<object>), 400)]
        public ActionResult<ApiResponse<FigureResponse>> GenerateFigure([FromBody] GenerateFigureRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiResponse<object>
                    {
                        Success = false,
                        Error = "Invalid request parameters",
                        Data = ModelState
                    });
                }

                var figure = _geomanticService.GenerateFigure(request);
                return Ok(new ApiResponse<FigureResponse>
                {
                    Success = true,
                    Data = figure
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Error = $"Error generating figure: {ex.Message}"
                });
            }
        }

        /// <summary>
        /// Generate four geomantic figures
        /// </summary>
        /// <param name="request">The elemental patterns for the four figures</param>
        /// <returns>The four generated geomantic figures</returns>
        /// <response code="200">Returns the four generated figures</response>
        /// <response code="400">If the request is invalid</response>
        [HttpPost("figures")]
        [ProducesResponseType(typeof(ApiResponse<FourFiguresResponse>), 200)]
        [ProducesResponseType(typeof(ApiResponse<object>), 400)]
        public ActionResult<ApiResponse<FourFiguresResponse>> GenerateFourFigures([FromBody] GenerateFourFiguresRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiResponse<object>
                    {
                        Success = false,
                        Error = "Invalid request parameters",
                        Data = ModelState
                    });
                }

                var figures = _geomanticService.GenerateFourFigures(request);
                return Ok(new ApiResponse<FourFiguresResponse>
                {
                    Success = true,
                    Data = figures
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Error = $"Error generating figures: {ex.Message}"
                });
            }
        }

        /// <summary>
        /// Generate a complete house chart from the first four houses
        /// </summary>
        /// <param name="request">The elemental patterns for the first four houses</param>
        /// <returns>The complete house chart with all 12 houses, witnesses, judge, and sentence</returns>
        /// <response code="200">Returns the complete house chart</response>
        /// <response code="400">If the request is invalid</response>
        [HttpPost("chart")]
        [ProducesResponseType(typeof(ApiResponse<HouseChartResponse>), 200)]
        [ProducesResponseType(typeof(ApiResponse<object>), 400)]
        public ActionResult<ApiResponse<HouseChartResponse>> GenerateHouseChart([FromBody] GenerateHouseChartRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiResponse<object>
                    {
                        Success = false,
                        Error = "Invalid request parameters",
                        Data = ModelState
                    });
                }

                var chart = _geomanticService.GenerateHouseChart(request);
                return Ok(new ApiResponse<HouseChartResponse>
                {
                    Success = true,
                    Data = chart
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Error = $"Error generating house chart: {ex.Message}"
                });
            }
        }

        /// <summary>
        /// Get all available geomantic figures
        /// </summary>
        /// <returns>List of all 16 geomantic figures</returns>
        /// <response code="200">Returns all available figures</response>
        [HttpGet("figures/all")]
        [ProducesResponseType(typeof(ApiResponse<List<FigureResponse>>), 200)]
        public ActionResult<ApiResponse<List<FigureResponse>>> GetAllFigures()
        {
            try
            {
                var figures = _geomanticService.GetAllFigures();
                return Ok(new ApiResponse<List<FigureResponse>>
                {
                    Success = true,
                    Data = figures
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Error = $"Error retrieving figures: {ex.Message}"
                });
            }
        }

        /// <summary>
        /// Get a specific geomantic figure by name
        /// </summary>
        /// <param name="name">The name of the figure (e.g., "Via", "Populus", "Conjunctio")</param>
        /// <returns>The requested geomantic figure</returns>
        /// <response code="200">Returns the requested figure</response>
        /// <response code="404">If the figure is not found</response>
        [HttpGet("figures/{name}")]
        [ProducesResponseType(typeof(ApiResponse<FigureResponse>), 200)]
        [ProducesResponseType(typeof(ApiResponse<object>), 404)]
        public ActionResult<ApiResponse<FigureResponse>> GetFigureByName(string name)
        {
            try
            {
                var figure = _geomanticService.GetFigureByName(name);
                if (figure == null)
                {
                    return NotFound(new ApiResponse<object>
                    {
                        Success = false,
                        Error = $"Figure '{name}' not found"
                    });
                }

                return Ok(new ApiResponse<FigureResponse>
                {
                    Success = true,
                    Data = figure
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Error = $"Error retrieving figure: {ex.Message}"
                });
            }
        }

        /// <summary>
        /// Health check endpoint
        /// </summary>
        /// <returns>API status</returns>
        /// <response code="200">API is healthy</response>
        [HttpGet("health")]
        [ProducesResponseType(typeof(ApiResponse<object>), 200)]
        public ActionResult<ApiResponse<object>> Health()
        {
            return Ok(new ApiResponse<object>
            {
                Success = true,
                Data = new { Status = "Healthy", Message = "Geomancy API is running" }
            });
        }
    }
} 