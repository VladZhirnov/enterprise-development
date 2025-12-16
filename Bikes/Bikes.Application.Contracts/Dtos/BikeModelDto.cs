using Bikes.Core.Enums;

namespace Bikes.Application.Contracts.Dtos;

/// <summary>
/// DTO for reading the bicycle model
/// </summary>
public record BikeModelDto
{
    /// <summary>
    /// Unique model identifier
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Bicycle type
    /// </summary>
    public BikeType Type { get; init; }

    /// <summary>
    /// Wheel size in inches
    /// </summary>
    public double WheelSize { get; init; }

    /// <summary>
    /// Maximum passenger weight in kg
    /// </summary>
    public double MaxWeight { get; init; }

    /// <summary>
    /// Bicycle weight in kg
    /// </summary>
    public double Weight { get; init; }

    /// <summary>
    /// Brake type
    /// </summary>
    public string BrakeType { get; init; }

    /// <summary>
    /// Model year
    /// </summary>
    public int ModelYear { get; init; }

    /// <summary>
    /// Hourly rental price
    /// </summary>
    public decimal PricePerHour { get; init; }

    /// <summary>
    /// DTO constructor
    /// </summary>
    public BikeModelDto(
        int id,
        BikeType type,
        double wheelSize,
        double maxWeight,
        double weight,
        string brakeType,
        int modelYear,
        decimal pricePerHour)
    {
        Id = id;
        Type = type;
        WheelSize = wheelSize;
        MaxWeight = maxWeight;
        Weight = weight;
        BrakeType = brakeType;
        ModelYear = modelYear;
        PricePerHour = pricePerHour;
    }
}