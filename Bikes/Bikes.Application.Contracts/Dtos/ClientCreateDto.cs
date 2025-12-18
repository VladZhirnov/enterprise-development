using System.ComponentModel.DataAnnotations;

namespace Bikes.Application.Contracts.Dtos;

/// <summary>
/// DTO for creating a client
/// </summary>
public record ClientCreateDto(
    /// <summary>
    /// Client's last name
    /// </summary>
    [Required(ErrorMessage = "Last name is required")]
    [StringLength(50, MinimumLength = 1, ErrorMessage = "The last name must be between 1 and 50 characters long.")]
    string LastName,

    /// <summary>
    /// Client name
    /// </summary>
    [Required(ErrorMessage = "Name is required")]
    [StringLength(50, MinimumLength = 1, ErrorMessage = "The name must be between 1 and 50 characters")]
    string FirstName,

    /// <summary>
    /// Client patronymic
    /// </summary>
    [Required(ErrorMessage = "Patronymic is required")]
    [StringLength(50, MinimumLength = 1, ErrorMessage = "Patronymic must be between 1 and 50 characters long")]
    string MiddleName,

    /// <summary>
    /// Client's phone number
    /// </summary>
    [Required(ErrorMessage = "Phone number is required")]
    [RegularExpression(@"^\+7-\d{3}-\d{3}-\d{2}-\d{2}$", ErrorMessage = "The phone number must be in the format +7-XXX-XXX-XX-XX")]
    string Phone);