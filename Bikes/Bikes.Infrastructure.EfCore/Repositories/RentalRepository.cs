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

    /// <summary>
    /// Get rentals statistics by bike type
    /// </summary>
    public async Task<Dictionary<Core.Enums.BikeType, int>> GetRentalStatisticsByBikeType()
    {
        return await dbContext.Rentals
            .Include(r => r.Bike)
                .ThenInclude(b => b!.Model)
            .Where(r => r.Bike != null && r.Bike.Model != null)
            .GroupBy(r => r.Bike!.Model!.Type)
            .Select(g => new
            {
                BikeType = g.Key,
                TotalHours = g.Sum(r => r.DurationHours)
            })
            .ToDictionaryAsync(x => x.BikeType, x => x.TotalHours);
    }

    /// <summary>
    /// Get top models by rental duration
    /// </summary>
    public async Task<List<TopModelDurationResult>> GetTopModelsByRentalDuration(int topN)
    {
        return await dbContext.Rentals
            .Include(r => r.Bike)
                .ThenInclude(b => b!.Model)
            .Where(r => r.Bike != null && r.Bike.Model != null)
            .GroupBy(r => r.Bike!.Model!.Id)
            .Select(g => new TopModelDurationResult
            {
                ModelId = g.Key,
                TotalDuration = g.Sum(r => r.DurationHours)
            })
            .OrderByDescending(x => x.TotalDuration)
            .Take(topN)
            .ToListAsync();
    }

    /// <summary>
    /// Get top models by profit
    /// </summary>
    public async Task<List<TopModelProfitResult>> GetTopModelsByProfit(int topN)
    {
        return await dbContext.Rentals
            .Include(r => r.Bike)
                .ThenInclude(b => b!.Model)
            .Where(r => r.Bike != null && r.Bike.Model != null)
            .GroupBy(r => r.Bike!.Model!.Id)
            .Select(g => new TopModelProfitResult
            {
                ModelId = g.Key,
                TotalProfit = g.Sum(r => r.DurationHours * r.Bike!.Model!.PricePerHour)
            })
            .OrderByDescending(x => x.TotalProfit)
            .Take(topN)
            .ToListAsync();
    }

    /// <summary>
    /// Get rental duration statistics
    /// </summary>
    public async Task<RentalDurationStatsResult> GetRentalDurationStatistics()
    {
        var durations = await dbContext.Rentals
            .Select(r => r.DurationHours)
            .ToListAsync();

        return new RentalDurationStatsResult
        {
            Min = durations.Min(),
            Max = durations.Max(),
            Avg = durations.Average()
        };
    }

    /// <summary>
    /// Result class for top models by duration
    /// </summary>
    public class TopModelDurationResult
    {
        public int ModelId { get; set; }
        public int TotalDuration { get; set; }
    }

    /// <summary>
    /// Result class for top models by profit
    /// </summary>
    public class TopModelProfitResult
    {
        public int ModelId { get; set; }
        public decimal TotalProfit { get; set; }
    }

    /// <summary>
    /// Result class for rental duration statistics
    /// </summary>
    public class RentalDurationStatsResult
    {
        public int Min { get; set; }
        public int Max { get; set; }
        public double Avg { get; set; }
    }
}