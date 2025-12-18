namespace Bikes.Application.Contracts.Dtos;

/// <summary>
/// DTO for reading a bicycle
/// </summary>
/// <param name="Id">Unique bicycle identifier</param>
/// <param name="SerialNumber">Serial number</param>
/// <param name="Color">Bicycle color</param>
/// <param name="Model">Bicycle model</param>
public record BikeDto(int Id, string SerialNumber, string Color, BikeModelDto Model);