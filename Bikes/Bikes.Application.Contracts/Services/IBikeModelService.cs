using Bikes.Application.Contracts.Dtos;

namespace Bikes.Application.Contracts.Services;

/// <summary>
/// Service interface for managing bike model entities
/// </summary>
public interface IBikeModelService
{
    /// <summary>
    /// Creates a new bike model
    /// </summary>
    /// <param name="dto">Data for creating bike model</param>
    /// <returns>ID of the created bike model</returns>
    public Task<int> CreateBikeModel(BikeModelCreateDto dto);

    /// <summary>
    /// Gets all bike models
    /// </summary>
    /// <returns>List of all bike models</returns>
    public Task<List<BikeModelDto>> GetBikeModels();

    /// <summary>
    /// Gets bike model by ID
    /// </summary>
    /// <param name="id">Bike model ID</param>
    /// <returns>Bike model or null if not found</returns>
    public Task<BikeModelDto?> GetBikeModel(int id);

    /// <summary>
    /// Updates an existing bike model
    /// </summary>
    /// <param name="id">Bike model ID</param>
    /// <param name="dto">Updated bike model data</param>
    /// <returns>Updated bike model or null if not found</returns>
    public Task<BikeModelDto?> UpdateBikeModel(int id, BikeModelCreateDto dto);

    /// <summary>
    /// Deletes a bike model by ID
    /// </summary>
    /// <param name="id">Bike model ID</param>
    /// <returns>True if deleted, false if not found</returns>
    public Task<bool> DeleteBikeModel(int id);
}