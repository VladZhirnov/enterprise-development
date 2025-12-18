using Bikes.Core.Entities;
using Bikes.Core.Repositories;
using Bikes.Infrastructure.EfCore.Data;
using Microsoft.EntityFrameworkCore;

namespace Bikes.Infrastructure.EfCore.Repositories;

/// <summary>
/// Repository for managing Bike entities in the database
/// </summary>
public class BikeRepository(AppDbContext dbContext) : IRepository<Bike>
{
    /// <summary>
    /// Create a new Bike record
    /// </summary>
    public async Task<int> Create(Bike entity)
    {
        await dbContext.Bikes.AddAsync(entity);
        await dbContext.SaveChangesAsync();
        return entity.Id;
    }

    /// <summary>
    /// Return all Bike records
    /// </summary>
    public async Task<List<Bike>> Read() =>
        await dbContext.Bikes
            .Include(x => x.Model)
            .Include(x => x.Rentals)
            .AsNoTracking()
            .ToListAsync();

    /// <summary>
    /// Return Bike by ID
    /// </summary>
    public async Task<Bike?> Read(int id) =>
        await dbContext.Bikes
            .Include(x => x.Model)
            .Include(x => x.Rentals)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

    /// <summary>
    /// Update Bike by ID
    /// </summary>
    public async Task<Bike?> Update(int id, Bike entity)
    {
        var existingEntity = await dbContext.Bikes.FindAsync(id);
        if (existingEntity == null) return null;

        var serialExists = await dbContext.Bikes
            .AnyAsync(b => b.SerialNumber == entity.SerialNumber && b.Id != id);

        if (serialExists)
        {
            throw new ArgumentException($"Bike with serial number {entity.SerialNumber} already exists");
        }

        existingEntity.SerialNumber = entity.SerialNumber;
        existingEntity.Color = entity.Color;
        existingEntity.ModelId = entity.ModelId;

        await dbContext.SaveChangesAsync();

        await dbContext.Entry(existingEntity)
            .Reference(b => b.Model)
            .LoadAsync();

        return existingEntity;
    }

    /// <summary>
    /// Delete Bike by ID
    /// </summary>
    public async Task<bool> Delete(int id)
    {
        var existingEntity = await dbContext.Bikes.FindAsync(id);
        if (existingEntity == null) return false;

        dbContext.Bikes.Remove(existingEntity);
        await dbContext.SaveChangesAsync();

        return true;
    }
}