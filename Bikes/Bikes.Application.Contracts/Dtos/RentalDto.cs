namespace Bikes.Application.Contracts.Dtos;

/// <summary>
/// DTO for reading lease
/// </summary>
public record RentalDto
{
    /// <summary>
    /// Unique lease identifier
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Bike
    /// </summary>
    public BikeDto Bike { get; init; }

    /// <summary>
    /// Client
    /// </summary>
    public ClientDto Client { get; init; }

    /// <summary>
    /// Rental start time
    /// </summary>
    public DateTime StartTime { get; init; }

    /// <summary>
    /// Rental duration in hours
    /// </summary>
    public int DurationHours { get; init; }

    /// <summary>
    /// DTO constructor
    /// </summary>
    public RentalDto(int id, BikeDto bike, ClientDto client, DateTime startTime, int durationHours)
    {
        Id = id;
        Bike = bike;
        Client = client;
        StartTime = startTime;
        DurationHours = durationHours;
    }
}