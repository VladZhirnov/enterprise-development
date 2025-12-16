using System.ComponentModel.DataAnnotations;

namespace Bikes.Application.Contracts.Dtos;

/// <summary>
/// DTO for creating a bicycle
/// </summary>
public record BikeCreateDto
{
    /// <summary>
    /// Serial number
    /// </summary>
    [Required(ErrorMessage = "Serial number is required")]
    [StringLength(50, MinimumLength = 1, ErrorMessage = "The serial number must be between 1 and 50 characters")]
    public string SerialNumber { get; init; }

    /// <summary>
    /// Bicycle color
    /// </summary>
    [Required(ErrorMessage = "Bicycle color is mandatory")]
    [StringLength(30, MinimumLength = 1, ErrorMessage = "Color must be between 1 and 30 characters")]
    public string Color { get; init; }

    /// <summary>
    /// Model ID
    /// </summary>
    [Required(ErrorMessage = "Model ID is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Invalid model ID")]
    public int ModelId { get; init; }

    /// <summary>
    /// DTO constructor
    /// </summary>
    public BikeCreateDto(string serialNumber, string color, int modelId)
    {
        SerialNumber = serialNumber;
        Color = color;
        ModelId = modelId;
    }
}