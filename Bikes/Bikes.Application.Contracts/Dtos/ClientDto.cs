namespace Bikes.Application.Contracts.Dtos;

/// <summary>
/// DTO for reading by the client
/// </summary>
/// <param name="Id">Unique client identifier</param>
/// <param name="LastName">Client's last name</param>
/// <param name="FirstName">Client name</param>
/// <param name="MiddleName">Client's patronymic</param>
/// <param name="Phone">Client's phone number</param>
public record ClientDto(int Id, string LastName, string FirstName, string MiddleName, string Phone);