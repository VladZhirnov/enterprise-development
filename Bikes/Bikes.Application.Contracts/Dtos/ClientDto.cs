namespace Bikes.Application.Contracts.Dtos;

/// <summary>
/// DTO for reading by the client
/// </summary>
public record ClientDto
{
    /// <summary>
    /// Unique client identifier
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Client's last name
    /// </summary>
    public string LastName { get; init; }

    /// <summary>
    /// Client name
    /// </summary>
    public string FirstName { get; init; }

    /// <summary>
    /// Client's patronymic
    /// </summary>
    public string MiddleName { get; init; }

    /// <summary>
    /// Client's phone number
    /// </summary>
    public string Phone { get; init; }

    /// <summary>
    /// DTO constructor
    /// </summary>
    public ClientDto(int id, string lastName, string firstName, string middleName, string phone)
    {
        Id = id;
        LastName = lastName;
        FirstName = firstName;
        MiddleName = middleName;
        Phone = phone;
    }
}