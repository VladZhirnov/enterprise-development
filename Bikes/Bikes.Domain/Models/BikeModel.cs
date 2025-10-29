using Bikes.Domain.Enum;

namespace Bikes.Domain.Models;

/// <summary>
/// Bike model information
/// </summary>
public class BikeModel
{
    /// <summary>
    /// Unique identifier of the bike model
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Type of bike
    /// </summary>
    public required BikeType Type { get; set; }

    /// <summary>
    /// Wheel size in inches
    /// </summary>
    public required double WheelSize { get; set; }

    /// <summary>
    /// Maximum allowed rider weight in kg
    /// </summary>
    public required double MaxWeight { get; set; }

    /// <summary>
    /// Bike weight in kg
    /// </summary>
    public required double Weight { get; set; }

    /// <summary>
    /// Type of brakes
    /// </summary>
    public required string BrakeType { get; set; }

    /// <summary>
    /// Model year
    /// </summary>
    public required int ModelYear { get; set; }

    /// <summary>
    /// Rental price per hour
    /// </summary>
    public required decimal PricePerHour { get; set; }
}