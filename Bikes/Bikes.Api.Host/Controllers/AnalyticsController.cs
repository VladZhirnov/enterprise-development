using Bikes.Application.Contracts.Dtos;
using Bikes.Application.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bikes.Api.Host.Controllers;

/// <summary>
/// Controller exposing analytic endpoints
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AnalyticsController(
    IAnalyticService service,
    ILogger<AnalyticsController> logger) : ControllerBase
{
    /// <summary>
    /// Information about all sport bikes
    /// </summary>
    [HttpGet("sport-bikes")]
    [ProducesResponseType(typeof(List<BikeDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<BikeDto>>> GetSportBikes()
    {
        try
        {
            var result = await service.GetSportBikes();
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting sport bikes");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Top 5 models by rental duration
    /// </summary>
    [HttpGet("top-models-by-duration")]
    [ProducesResponseType(typeof(List<TopModelByRentalDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<TopModelByRentalDto>>> GetTopModelsByRentalDuration()
    {
        try
        {
            var result = await service.GetTopModelsByRentalDuration();
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting top models by rental duration");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Top 5 models by profit
    /// </summary>
    [HttpGet("top-models-by-profit")]
    [ProducesResponseType(typeof(List<TopModelByProfitDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<TopModelByProfitDto>>> GetTopModelsByProfit()
    {
        try
        {
            var result = await service.GetTopModelsByProfit();
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting top models by profit");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Rental duration statistics
    /// </summary>
    [HttpGet("rental-duration-stats")]
    [ProducesResponseType(typeof(RentalDurationStatsDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<RentalDurationStatsDto>> GetRentalDurationStats()
    {
        try
        {
            var result = await service.GetRentalDurationStatistics();
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting rental duration statistics");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Total rental time for each bike type
    /// </summary>
    [HttpGet("rental-time-by-bike-type")]
    [ProducesResponseType(typeof(List<RentalTimeByBikeTypeDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<RentalTimeByBikeTypeDto>>> GetRentalTimeByBikeType()
    {
        try
        {
            var result = await service.GetRentalTimeByBikeType();
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting rental time by bike type");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Clients who rented bikes the most times
    /// </summary>
    [HttpGet("top-renters")]
    [ProducesResponseType(typeof(List<TopClientDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<TopClientDto>>> GetTopRenters()
    {
        try
        {
            var result = await service.GetTopRenters();
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting top renters");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }
}