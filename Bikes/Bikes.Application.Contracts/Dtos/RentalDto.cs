namespace Bikes.Application.Contracts.Dtos;

/// <summary>
/// DTO for reading lease
/// </summary>
/// <param name="Id">Unique lease identifier</param>
/// <param name="Bike">Bike</param>
/// <param name="Client">Client</param>
/// <param name="StartTime">Rental start time</param>
/// <param name="DurationHours">Rental duration in hours</param>
public record RentalDto(int Id, BikeDto Bike, ClientDto Client, DateTime StartTime, int DurationHours);