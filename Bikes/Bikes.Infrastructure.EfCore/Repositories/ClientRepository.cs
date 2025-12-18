using Bikes.Core.Entities;
using Bikes.Core.Repositories;
using Bikes.Infrastructure.EfCore.Data;
using Microsoft.EntityFrameworkCore;

namespace Bikes.Infrastructure.EfCore.Repositories;

/// <summary>
/// Repository for managing Client entities in the database
/// </summary>
public class ClientRepository(AppDbContext dbContext) : IRepository<Client>
{
    /// <summary>
    /// Create a new Client record
    /// </summary>
    public async Task<int> Create(Client entity)
    {
        await dbContext.Clients.AddAsync(entity);
        await dbContext.SaveChangesAsync();
        return entity.Id;
    }

    /// <summary>
    /// Return all Client records
    /// </summary>
    public async Task<List<Client>> Read() =>
        await dbContext.Clients
            .AsNoTracking()
            .ToListAsync();

    /// <summary>
    /// Return Client by ID
    /// </summary>
    public async Task<Client?> Read(int id) =>
        await dbContext.Clients
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

    /// <summary>
    /// Update Client by ID
    /// </summary>
    public async Task<Client?> Update(int id, Client entity)
    {
        var existingEntity = await dbContext.Clients.FindAsync(id);
        if (existingEntity == null) return null;

        existingEntity.LastName = entity.LastName;
        existingEntity.FirstName = entity.FirstName;
        existingEntity.MiddleName = entity.MiddleName;
        existingEntity.Phone = entity.Phone;

        await dbContext.SaveChangesAsync();

        return existingEntity;
    }

    /// <summary>
    /// Delete Client by ID
    /// </summary>
    public async Task<bool> Delete(int id)
    {
        var existingEntity = await dbContext.Clients.FindAsync(id);
        if (existingEntity == null) return false;

        dbContext.Clients.Remove(existingEntity);
        await dbContext.SaveChangesAsync();

        return true;
    }
}