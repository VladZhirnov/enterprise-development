using System.ComponentModel.DataAnnotations;

namespace Bikes.Application.Contracts.Dtos;

/// <summary>
/// DTO for creating a client
/// </summary>
public record ClientCreateDto
{
    /// <summary>
    /// Client's last name
    /// </summary>
    [Required(ErrorMessage = "Last name is required")]
    [StringLength(50, MinimumLength = 1, ErrorMessage = "The last name must be between 1 and 50 characters long.")]
    public string LastName { get; init; }

    /// <summary>
    /// Client name
    /// </summary>
    [Required(ErrorMessage = "Name is required")]
    [StringLength(50, MinimumLength = 1, ErrorMessage = "The name must be between 1 and 50 characters")]
    public string FirstName { get; init; }

    /// <summary>
    /// Client patronymic
    /// </summary>
    [Required(ErrorMessage = "Patronymic is required")]
    [StringLength(50, MinimumLength = 1, ErrorMessage = "Patronymic must be between 1 and 50 characters long")]
    public string MiddleName { get; init; }

    /// <summary>
    /// Client's phone number
    /// </summary>
    [Required(ErrorMessage = "Phone number is required")]
    [RegularExpression(@"^\+7-\d{3}-\d{3}-\d{2}-\d{2}$", ErrorMessage = "The phone number must be in the format +7-XXX-XXX-XX-XX")]
    public string Phone { get; init; }

    /// <summary>
    /// DTO constructor
    /// </summary>
    public ClientCreateDto(string lastName, string firstName, string middleName, string phone)
    {
        LastName = lastName;
        FirstName = firstName;
        MiddleName = middleName;
        Phone = phone;
    }
}