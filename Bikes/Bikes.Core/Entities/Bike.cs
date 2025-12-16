namespace Bikes.Core.Entities;

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
    /// Color of the bike
    /// </summary>
    public required string Color { get; set; }

    /// <summary>
    /// Model ID
    /// </summary>
    public required int ModelId { get; set; }

    /// <summary>
    /// Bicycle model
    /// </summary>
    public BikeModel? Model { get; set; }

    /// <summary>
    /// Rent this bike
    /// </summary>
    public List<Rental> Rentals { get; set; } = [];
}