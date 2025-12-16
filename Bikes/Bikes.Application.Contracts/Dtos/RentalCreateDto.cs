using System.ComponentModel.DataAnnotations;

namespace Bikes.Application.Contracts.Dtos;

/// <summary>
/// DTO for creating a lease
/// </summary>
public record RentalCreateDto
{
    /// <summary>
    /// Bicycle ID
    /// </summary>
    [Required(ErrorMessage = "Bicycle ID is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Invalid bike ID")]
    public int BikeId { get; init; }

    /// <summary>
    /// Client ID
    /// </summary>
    [Required(ErrorMessage = "Client ID is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Invalid client ID")]
    public int ClientId { get; init; }

    /// <summary>
    /// Rental start time
    /// </summary>
    [Required(ErrorMessage = "Rental start time is required")]
    [DataType(DataType.DateTime)]
    public DateTime StartTime { get; init; }

    /// <summary>
    /// Rental duration in hours
    /// </summary>
    [Required(ErrorMessage = "Rental duration is mandatory")]
    [Range(1, 720, ErrorMessage = "The rental duration must be between 1 and 720 hours")]
    public int DurationHours { get; init; }

    /// <summary>
    /// DTO constructor
    /// </summary>
    public RentalCreateDto(int bikeId, int clientId, DateTime startTime, int durationHours)
    {
        BikeId = bikeId;
        ClientId = clientId;
        StartTime = startTime;
        DurationHours = durationHours;
    }
}