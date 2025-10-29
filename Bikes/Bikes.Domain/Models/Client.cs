namespace Bikes.Domain.Models;

/// <summary>
/// Client information
/// </summary>
public class Client
{
    /// <summary>
    /// Unique identifier of the client
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// First name of the client
    /// </summary>
    public required string FirstName { get; set; }

    /// <summary>
    /// Last name of the client
    /// </summary>
    public required string LastName { get; set; }

    /// <summary>
    /// Middle name of the client
    /// </summary>
    public required string MiddleName { get; set; }

    /// <summary>
    /// Phone number of the client
    /// </summary>
    public required string Phone { get; set; }
}