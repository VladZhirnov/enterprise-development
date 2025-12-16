using Bikes.Application.Contracts.Dtos;

namespace Bikes.Application.Contracts.Services;

/// <summary>
/// Service interface for managing client entities
/// </summary>
public interface IClientService
{
    /// <summary>
    /// Creates a new client
    /// </summary>
    /// <param name="dto">Data for creating client</param>
    /// <returns>ID of the created client</returns>
    public Task<int> CreateClient(ClientCreateDto dto);

    /// <summary>
    /// Gets all clients
    /// </summary>
    /// <returns>List of all clients</returns>
    public Task<List<ClientDto>> GetClients();

    /// <summary>
    /// Gets client by ID
    /// </summary>
    /// <param name="id">Client ID</param>
    /// <returns>Client or null if not found</returns>
    public Task<ClientDto?> GetClient(int id);

    /// <summary>
    /// Updates an existing client
    /// </summary>
    /// <param name="id">Client ID</param>
    /// <param name="dto">Updated client data</param>
    /// <returns>Updated client or null if not found</returns>
    public Task<ClientDto?> UpdateClient(int id, ClientCreateDto dto);

    /// <summary>
    /// Deletes a client by ID
    /// </summary>
    /// <param name="id">Client ID</param>
    /// <returns>True if deleted, false if not found</returns>
    public Task<bool> DeleteClient(int id);
}