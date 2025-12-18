namespace Bikes.Application.Contracts.Dtos;

/// <summary>
/// Top models by rental duration
/// </summary>
public record TopModelByRentalDto(
    int ModelId,
    string ModelType,
    int ModelYear,
    string BrakeType,
    int TotalDuration);

/// <summary>
/// Top models by profit
/// </summary>
public record TopModelByProfitDto(
    int ModelId,
    string ModelType,
    int ModelYear,
    decimal PricePerHour,
    decimal TotalProfit);

/// <summary>
/// Rental duration statistics
/// </summary>
public record RentalDurationStatsDto(int MinDuration, int MaxDuration, double AvgDuration);

/// <summary>
/// Rental time by bike type
/// </summary>
public record RentalTimeByBikeTypeDto(string BikeType, int TotalHours);

/// <summary>
/// Top clients by number of rentals
/// </summary>
public record TopClientDto(int ClientId, string FullName, int RentalCount);