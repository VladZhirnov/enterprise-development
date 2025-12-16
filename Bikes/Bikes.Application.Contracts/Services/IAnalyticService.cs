using Bikes.Application.Contracts.Dtos;

namespace Bikes.Application.Contracts.Services;

/// <summary>
/// Service interface for analytical operations and reports
/// </summary>
public interface IAnalyticService
{
    /// <summary>
    /// Gets information about all sport bikes
    /// </summary>
    /// <returns>List of all sport bikes</returns>
    public Task<List<BikeDto>> GetSportBikes();

    /// <summary>
    /// Gets top 5 bike models by rental duration
    /// </summary>
    /// <returns>List of top 5 models by total rental hours</returns>
    public Task<List<TopModelByRentalDto>> GetTopModelsByRentalDuration();

    /// <summary>
    /// Gets top 5 bike models by profit
    /// </summary>
    /// <returns>List of top 5 models by total profit</returns>
    public Task<List<TopModelByProfitDto>> GetTopModelsByProfit();

    /// <summary>
    /// Gets rental duration statistics
    /// </summary>
    /// <returns>Minimum, maximum and average rental duration</returns>
    public Task<RentalDurationStatsDto> GetRentalDurationStatistics();

    /// <summary>
    /// Gets total rental time for each bike type
    /// </summary>
    /// <returns>List of bike types with total rental hours</returns>
    public Task<List<RentalTimeByBikeTypeDto>> GetRentalTimeByBikeType();

    /// <summary>
    /// Gets clients who rented bikes the most times
    /// </summary>
    /// <returns>List of top clients by number of rentals</returns>
    public Task<List<TopClientDto>> GetTopRenters();
}