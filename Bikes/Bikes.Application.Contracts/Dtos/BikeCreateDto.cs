using System.ComponentModel.DataAnnotations;

namespace Bikes.Application.Contracts.Dtos;

/// <summary>
/// DTO for creating a bicycle
/// </summary>
public record BikeCreateDto(
    /// <summary>
    /// Serial number
    /// </summary>
    [Required(ErrorMessage = "Serial number is required")]
    [StringLength(50, MinimumLength = 1, ErrorMessage = "The serial number must be between 1 and 50 characters")]
    string SerialNumber,

    /// <summary>
    /// Bicycle color
    /// </summary>
    [Required(ErrorMessage = "Bicycle color is mandatory")]
    [StringLength(30, MinimumLength = 1, ErrorMessage = "Color must be between 1 and 30 characters")]
    string Color,

    /// <summary>
    /// Model ID
    /// </summary>
    [Required(ErrorMessage = "Model ID is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Invalid model ID")]
    int ModelId);