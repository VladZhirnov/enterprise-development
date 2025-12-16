using Bikes.Application.Contracts.Dtos;
using Bikes.Application.Contracts.Services;
using Bikes.Application.Mapper;
using Bikes.Core.Entities;
using Bikes.Core.Repositories;

namespace Bikes.Application.Services;

/// <summary>
/// Service for managing bike entities
/// </summary>
public class BikeService(
    IRepository<Bike> bikeRepository,
    IRepository<BikeModel> modelRepository) : IBikeService
{
    /// <summary>
    /// Create a new bike record
    /// </summary>
    public async Task<int> CreateBike(BikeCreateDto dto)
    {
        var model = await modelRepository.Read(dto.ModelId)
            ?? throw new ArgumentException("Invalid BikeModel ID");

        var entity = MapperClass.ToEntity(dto);
        return await bikeRepository.Create(entity);
    }

    /// <summary>
    /// Get all bikes
    /// </summary>
    public async Task<List<BikeDto>> GetBikes() =>
        [.. (await bikeRepository.Read()).Select(MapperClass.ToDto)];

    /// <summary>
    /// Get bikes by model ID
    /// </summary>
    public async Task<List<BikeDto>> GetBikesByModelId(int modelId)
    {
        var model = await modelRepository.Read(modelId);
        if (model == null) return [];

        var bikes = await bikeRepository.Read();
        return bikes
            .Where(b => b.ModelId == modelId)
            .Select(MapperClass.ToDto)
            .ToList();
    }

    /// <summary>
    /// Get sport bikes
    /// </summary>
    public async Task<List<BikeDto>> GetSportBikes()
    {
        var bikes = await bikeRepository.Read();
        return bikes
            .Where(b => b.Model?.Type == Core.Enums.BikeType.Sport)
            .Select(MapperClass.ToDto)
            .ToList();
    }

    /// <summary>
    /// Get bike by ID
    /// </summary>
    public async Task<BikeDto?> GetBike(int id)
    {
        var entity = await bikeRepository.Read(id);
        return entity == null ? null : MapperClass.ToDto(entity);
    }

    /// <summary>
    /// Update bike by ID
    /// </summary>
    public async Task<BikeDto?> UpdateBike(int id, BikeCreateDto dto)
    {
        var model = await modelRepository.Read(dto.ModelId)
            ?? throw new ArgumentException("Invalid BikeModel ID");

        var entity = MapperClass.ToEntity(dto);
        var updated = await bikeRepository.Update(id, entity);
        return updated == null ? null : MapperClass.ToDto(updated);
    }

    /// <summary>
    /// Delete bike by ID
    /// </summary>
    public async Task<bool> DeleteBike(int id) =>
        await bikeRepository.Delete(id);
}