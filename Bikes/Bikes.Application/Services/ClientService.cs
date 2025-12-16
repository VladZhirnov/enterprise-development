using Bikes.Application.Contracts.Dtos;
using Bikes.Application.Contracts.Services;
using Bikes.Application.Mapper;
using Bikes.Core.Entities;
using Bikes.Core.Repositories;

namespace Bikes.Application.Services;

/// <summary>
/// Service for managing client entities
/// </summary>
public class ClientService(IRepository<Client> repository) : IClientService
{
    /// <summary>
    /// Create a new client record
    /// </summary>
    public async Task<int> CreateClient(ClientCreateDto dto)
    {
        var entity = MapperClass.ToEntity(dto);
        return await repository.Create(entity);
    }

    /// <summary>
    /// Get all clients
    /// </summary>
    public async Task<List<ClientDto>> GetClients() =>
        [.. (await repository.Read()).Select(MapperClass.ToDto)];

    /// <summary>
    /// Get client by ID
    /// </summary>
    public async Task<ClientDto?> GetClient(int id)
    {
        var entity = await repository.Read(id);
        return entity == null ? null : MapperClass.ToDto(entity);
    }

    /// <summary>
    /// Update client by ID
    /// </summary>
    public async Task<ClientDto?> UpdateClient(int id, ClientCreateDto dto)
    {
        var entity = MapperClass.ToEntity(dto);
        var updated = await repository.Update(id, entity);
        return updated == null ? null : MapperClass.ToDto(updated);
    }

    /// <summary>
    /// Delete client by ID
    /// </summary>
    public async Task<bool> DeleteClient(int id) =>
        await repository.Delete(id);
}