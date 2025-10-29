namespace Bikes.Domain.Models;

/// <summary>
/// Bike entity
/// </summary>
public class Bike
{
    /// <summary>
    /// Unique identifier
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Serial number
    /// </summary>
    public required string SerialNumber { get; set; }

    /// <summary>
    /// Bicycle model navigation property
    /// </summary>
    public required BikeModel Model { get; set; }

    /// <summary>
    /// Color of the bike
    /// </summary>
    public required string Color { get; set; }
}