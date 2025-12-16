using Bikes.Application.Contracts.Dtos;
using Bikes.Application.Contracts.Services;
using Bikes.Application.Mapper;
using Bikes.Core.Entities;
using Bikes.Core.Repositories;

namespace Bikes.Application.Services;

/// <summary>
/// Service for managing bike model entities
/// </summary>
public class BikeModelService(IRepository<BikeModel> repository) : IBikeModelService
{
    /// <summary>
    /// Create a new bike model record
    /// </summary>
    public async Task<int> CreateBikeModel(BikeModelCreateDto dto) =>
        await repository.Create(MapperClass.ToEntity(dto));

    /// <summary>
    /// Get all bike models
    /// </summary>
    public async Task<List<BikeModelDto>> GetBikeModels() =>
        [.. (await repository.Read()).Select(MapperClass.ToDto)];

    /// <summary>
    /// Get bike model by ID
    /// </summary>
    public async Task<BikeModelDto?> GetBikeModel(int id)
    {
        var entity = await repository.Read(id);
        return entity == null ? null : MapperClass.ToDto(entity);
    }

    /// <summary>
    /// Update bike model by ID
    /// </summary>
    public async Task<BikeModelDto?> UpdateBikeModel(int id, BikeModelCreateDto dto)
    {
        var entity = await repository.Update(id, MapperClass.ToEntity(dto));
        return entity == null ? null : MapperClass.ToDto(entity);
    }

    /// <summary>
    /// Delete bike model by ID
    /// </summary>
    public async Task<bool> DeleteBikeModel(int id) =>
        await repository.Delete(id);
}