namespace Bikes.Application.Contracts.Dtos;

/// <summary>
/// DTO for reading a bicycle
/// </summary>
public record BikeDto
{
    /// <summary>
    /// Unique bicycle identifier
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Serial number
    /// </summary>
    public string SerialNumber { get; init; }

    /// <summary>
    /// Bicycle color
    /// </summary>
    public string Color { get; init; }

    /// <summary>
    /// Bicycle model
    /// </summary>
    public BikeModelDto Model { get; init; }

    /// <summary>
    /// DTO constructor
    /// </summary>
    public BikeDto(int id, string serialNumber, string color, BikeModelDto model)
    {
        Id = id;
        SerialNumber = serialNumber;
        Color = color;
        Model = model;
    }
}