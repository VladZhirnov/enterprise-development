using Bikes.Core.Entities;
using Bikes.Core.Repositories;
using Bikes.Infrastructure.EfCore.Data;
using Microsoft.EntityFrameworkCore;

namespace Bikes.Infrastructure.EfCore.Repositories;

/// <summary>
/// Repository for managing Rental entities in the database
/// </summary>
public class RentalRepository(AppDbContext dbContext) : IRepository<Rental>
{
    /// <summary>
    /// Create a new Rental record
    /// </summary>
    public async Task<int> Create(Rental entity)
    {
        await dbContext.Rentals.AddAsync(entity);
        await dbContext.SaveChangesAsync();
        return entity.Id;
    }

    /// <summary>
    /// Return all Rental records
    /// </summary>
    public async Task<List<Rental>> Read() =>
        await dbContext.Rentals
            .Include(r => r.Bike)
                .ThenInclude(b => b!.Model)
            .Include(r => r.Client)
            .AsNoTracking()
            .ToListAsync();

    /// <summary>
    /// Return Rental by ID
    /// </summary>
    public async Task<Rental?> Read(int id) =>
        await dbContext.Rentals
            .Include(r => r.Bike)
                .ThenInclude(b => b!.Model)
            .Include(r => r.Client)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

    /// <summary>
    /// Update Rental by ID
    /// </summary>
    public async Task<Rental?> Update(int id, Rental entity)
    {
        var existingEntity = await dbContext.Rentals.FindAsync(id);
        if (existingEntity == null) return null;

        existingEntity.BikeId = entity.BikeId;
        existingEntity.ClientId = entity.ClientId;
        existingEntity.StartTime = entity.StartTime;
        existingEntity.DurationHours = entity.DurationHours;

        await dbContext.SaveChangesAsync();

        await dbContext.Entry(existingEntity)
            .Reference(r => r.Bike)
            .LoadAsync();

        if (existingEntity.Bike != null)
        {
            await dbContext.Entry(existingEntity.Bike)
                .Reference(b => b.Model)
                .LoadAsync();
        }

        await dbContext.Entry(existingEntity)
            .Reference(r => r.Client)
            .LoadAsync();

        return existingEntity;
    }

    /// <summary>
    /// Delete Rental by ID
    /// </summary>
    public async Task<bool> Delete(int id)
    {
        var existingEntity = await dbContext.Rentals.FindAsync(id);
        if (existingEntity == null) return false;

        dbContext.Rentals.Remove(existingEntity);
        await dbContext.SaveChangesAsync();

        return true;
    }
}