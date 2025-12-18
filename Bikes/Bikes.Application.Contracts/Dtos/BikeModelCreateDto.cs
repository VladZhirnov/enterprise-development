using Bikes.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace Bikes.Application.Contracts.Dtos;

/// <summary>
/// DTO for creating a bicycle model
/// </summary>
public record BikeModelCreateDto(
    /// <summary>
    /// Bicycle type
    /// </summary>
    [Required(ErrorMessage = "Bicycle type is required")]
    BikeType Type,

    /// <summary>
    /// Wheel size in inches
    /// </summary>
    [Required(ErrorMessage = "Wheel size is required")]
    [Range(1, 100, ErrorMessage = "Wheel size must be between 1 and 100 inches")]
    double WheelSize,

    /// <summary>
    /// Maximum passenger weight in kg
    /// </summary>
    [Required(ErrorMessage = "Maximum weight is mandatory")]
    [Range(1, 300, ErrorMessage = "The maximum weight should be from 1 to 300 kg")]
    double MaxWeight,

    /// <summary>
    /// Bicycle weight in kg
    /// </summary>
    [Required(ErrorMessage = "Bicycle weight is mandatory")]
    [Range(1, 100, ErrorMessage = "The weight of the bicycle must be between 1 and 100 kg")]
    double Weight,

    /// <summary>
    /// Brake type
    /// </summary>
    [Required(ErrorMessage = "Brake type is mandatory")]
    [StringLength(50, MinimumLength = 1, ErrorMessage = "Brake type must be between 1 and 50 characters")]
    string BrakeType,

    /// <summary>
    /// Model year
    /// </summary>
    [Required(ErrorMessage = "Model year is required")]
    [Range(2000, 2100, ErrorMessage = "Model year must be between 2000 and 2100")]
    int ModelYear,

    /// <summary>
    /// Hourly rental price
    /// </summary>
    [Required(ErrorMessage = "Hourly rental price is mandatory")]
    [Range(0, 1000, ErrorMessage = "The rental price per hour must be between 0 and 1000")]
    decimal PricePerHour);