using Bikes.Core.Entities;
using Bikes.Core.Repositories;
using Bikes.Infrastructure.EfCore.Data;
using Microsoft.EntityFrameworkCore;

namespace Bikes.Infrastructure.EfCore.Repositories;

/// <summary>
/// Repository for managing BikeModel entities in the database
/// </summary>
public class BikeModelRepository(AppDbContext dbContext) : IRepository<BikeModel>
{
    /// <summary>
    /// Create a new BikeModel record
    /// </summary>
    public async Task<int> Create(BikeModel entity)
    {
        await dbContext.BikeModels.AddAsync(entity);
        await dbContext.SaveChangesAsync();
        return entity.Id;
    }

    /// <summary>
    /// Return all BikeModel records
    /// </summary>
    public async Task<List<BikeModel>> Read() =>
        await dbContext.BikeModels
            .AsNoTracking()
            .ToListAsync();

    /// <summary>
    /// Return BikeModel by ID
    /// </summary>
    public async Task<BikeModel?> Read(int id) =>
        await dbContext.BikeModels
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

    /// <summary>
    /// Update BikeModel by ID
    /// </summary>
    public async Task<BikeModel?> Update(int id, BikeModel entity)
    {
        var existingEntity = await dbContext.BikeModels.FindAsync(id);
        if (existingEntity == null) return null;

        existingEntity.Type = entity.Type;
        existingEntity.WheelSize = entity.WheelSize;
        existingEntity.MaxWeight = entity.MaxWeight;
        existingEntity.Weight = entity.Weight;
        existingEntity.BrakeType = entity.BrakeType;
        existingEntity.ModelYear = entity.ModelYear;
        existingEntity.PricePerHour = entity.PricePerHour;

        await dbContext.SaveChangesAsync();

        return existingEntity;
    }

    /// <summary>
    /// Delete BikeModel by ID
    /// </summary>
    public async Task<bool> Delete(int id)
    {
        var existingEntity = await dbContext.BikeModels.FindAsync(id);
        if (existingEntity == null) return false;

        dbContext.BikeModels.Remove(existingEntity);
        await dbContext.SaveChangesAsync();

        return true;
    }
}