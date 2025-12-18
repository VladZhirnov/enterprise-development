using System.ComponentModel.DataAnnotations;

namespace Bikes.Application.Contracts.Dtos;

/// <summary>
/// DTO for creating a lease
/// </summary>
public record RentalCreateDto(
    /// <summary>
    /// Bicycle ID
    /// </summary>
    [Required(ErrorMessage = "Bicycle ID is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Invalid bike ID")]
    int BikeId,

    /// <summary>
    /// Client ID
    /// </summary>
    [Required(ErrorMessage = "Client ID is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Invalid client ID")]
    int ClientId,

    /// <summary>
    /// Rental start time
    /// </summary>
    [Required(ErrorMessage = "Rental start time is required")]
    [DataType(DataType.DateTime)]
    DateTime StartTime,

    /// <summary>
    /// Rental duration in hours
    /// </summary>
    [Required(ErrorMessage = "Rental duration is mandatory")]
    [Range(1, 720, ErrorMessage = "The rental duration must be between 1 and 720 hours")]
    int DurationHours);