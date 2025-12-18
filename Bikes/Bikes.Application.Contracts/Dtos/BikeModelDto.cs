using Bikes.Core.Enums;

namespace Bikes.Application.Contracts.Dtos;

/// <summary>
/// DTO for reading the bicycle model
/// </summary>
/// <param name="Id">Unique model identifier</param>
/// <param name="Type">Bicycle type</param>
/// <param name="WheelSize">Wheel size in inches</param>
/// <param name="MaxWeight">Maximum passenger weight in kg</param>
/// <param name="Weight">Bicycle weight in kg</param>
/// <param name="BrakeType">Brake type</param>
/// <param name="ModelYear">Model year</param>
/// <param name="PricePerHour">Hourly rental price</param>
public record BikeModelDto(
    int Id,
    BikeType Type,
    double WheelSize,
    double MaxWeight,
    double Weight,
    string BrakeType,
    int ModelYear,
    decimal PricePerHour);