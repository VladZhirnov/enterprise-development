namespace Bikes.Core.Entities;

/// <summary>
/// Bike rental information
/// </summary>
public class Rental
{
    /// <summary>
    /// Unique identifier
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Bicycle ID
    /// </summary>
    public required int BikeId { get; set; }

    /// <summary>
    /// Rented bike
    /// </summary>
    public Bike? Bike { get; set; }

    /// <summary>
    /// Client ID
    /// </summary>
    public required int ClientId { get; set; }

    /// <summary>
    /// Client who rented the bike
    /// </summary>
    public Client? Client { get; set; }

    /// <summary>
    /// Rental start time
    /// </summary>
    public required DateTime StartTime { get; set; }

    /// <summary>
    /// Rental duration in hours
    /// </summary>
    public required int DurationHours { get; set; }
}