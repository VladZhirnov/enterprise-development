using Bikes.Application.Contracts.Dtos;

namespace Bikes.Application.Contracts.Services;

/// <summary>
/// Service interface for managing rental entities
/// </summary>
public interface IRentalService
{
    /// <summary>
    /// Creates a new rental
    /// </summary>
    /// <param name="dto">Data for creating rental</param>
    /// <returns>ID of the created rental</returns>
    public Task<int> CreateRental(RentalCreateDto dto);

    /// <summary>
    /// Gets all rentals
    /// </summary>
    /// <returns>List of all rentals</returns>
    public Task<List<RentalDto>> GetRentals();

    /// <summary>
    /// Gets rentals by bike ID
    /// </summary>
    /// <param name="bikeId">Bike ID</param>
    /// <returns>List of rentals for the specified bike</returns>
    public Task<List<RentalDto>> GetRentalsByBikeId(int bikeId);

    /// <summary>
    /// Gets rentals by client ID
    /// </summary>
    /// <param name="clientId">Client ID</param>
    /// <returns>List of rentals for the specified client</returns>
    public Task<List<RentalDto>> GetRentalsByClientId(int clientId);

    /// <summary>
    /// Gets rental by ID
    /// </summary>
    /// <param name="id">Rental ID</param>
    /// <returns>Rental or null if not found</returns>
    public Task<RentalDto?> GetRental(int id);

    /// <summary>
    /// Updates an existing rental
    /// </summary>
    /// <param name="id">Rental ID</param>
    /// <param name="dto">Updated rental data</param>
    /// <returns>Updated rental or null if not found</returns>
    public Task<RentalDto?> UpdateRental(int id, RentalCreateDto dto);

    /// <summary>
    /// Deletes a rental by ID
    /// </summary>
    /// <param name="id">Rental ID</param>
    /// <returns>True if deleted, false if not found</returns>
    public Task<bool> DeleteRental(int id);
}