using Bikes.Application.Contracts.Dtos;

namespace Bikes.Application.Contracts.Services;

/// <summary>
/// Service interface for managing bike entities
/// </summary>
public interface IBikeService
{
    /// <summary>
    /// Creates a new bike
    /// </summary>
    /// <param name="dto">Data for creating bike</param>
    /// <returns>ID of the created bike</returns>
    public Task<int> CreateBike(BikeCreateDto dto);

    /// <summary>
    /// Gets all bikes
    /// </summary>
    /// <returns>List of all bikes</returns>
    public Task<List<BikeDto>> GetBikes();

    /// <summary>
    /// Gets bikes by model ID
    /// </summary>
    /// <param name="modelId">Bike model ID</param>
    /// <returns>List of bikes of the specified model</returns>
    public Task<List<BikeDto>> GetBikesByModelId(int modelId);

    /// <summary>
    /// Gets bike by ID
    /// </summary>
    /// <param name="id">Bike ID</param>
    /// <returns>Bike or null if not found</returns>
    public Task<BikeDto?> GetBike(int id);

    /// <summary>
    /// Updates an existing bike
    /// </summary>
    /// <param name="id">Bike ID</param>
    /// <param name="dto">Updated bike data</param>
    /// <returns>Updated bike or null if not found</returns>
    public Task<BikeDto?> UpdateBike(int id, BikeCreateDto dto);

    /// <summary>
    /// Deletes a bike by ID
    /// </summary>
    /// <param name="id">Bike ID</param>
    /// <returns>True if deleted, false if not found</returns>
    public Task<bool> DeleteBike(int id);
}