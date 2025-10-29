namespace Bikes.Domain.Models;

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
    /// Rented bike
    /// </summary>
    public required Bike Bike { get; set; }

    /// <summary>
    /// Client who rented the bike
    /// </summary>
    public required Client Client { get; set; }

    /// <summary>
    /// Rental start time
    /// </summary>
    public required DateTime StartTime { get; set; }

    /// <summary>
    /// Rental duration in hours
    /// </summary>
    public required int DurationHours { get; set; }
}