using Bikes.Application.Contracts.Dtos;
using Bikes.Application.Contracts.Services;
using Bikes.Application.Mapper;
using Bikes.Core.Entities;
using Bikes.Core.Repositories;

namespace Bikes.Application.Services;

/// <summary>
/// Service for managing rental entities
/// </summary>
public class RentalService(
    IRepository<Rental> rentalRepository,
    IRepository<Bike> bikeRepository,
    IRepository<Client> clientRepository) : IRentalService
{
    /// <summary>
    /// Create a new rental record
    /// </summary>
    public async Task<int> CreateRental(RentalCreateDto dto)
    {
        var bike = await bikeRepository.Read(dto.BikeId)
            ?? throw new ArgumentException("Invalid Bike ID");
        var client = await clientRepository.Read(dto.ClientId)
            ?? throw new ArgumentException("Invalid Client ID");

        var entity = MapperClass.ToEntity(dto);
        return await rentalRepository.Create(entity);
    }

    /// <summary>
    /// Get all rentals
    /// </summary>
    public async Task<List<RentalDto>> GetRentals() =>
        [.. (await rentalRepository.Read()).Select(MapperClass.ToDto)];

    /// <summary>
    /// Get rentals by bike ID
    /// </summary>
    public async Task<List<RentalDto>> GetRentalsByBikeId(int bikeId)
    {
        var bike = await bikeRepository.Read(bikeId);
        if (bike == null) return [];

        var rentals = await rentalRepository.Read();
        return rentals
            .Where(r => r.BikeId == bikeId)
            .Select(MapperClass.ToDto)
            .ToList();
    }

    /// <summary>
    /// Get rentals by client ID
    /// </summary>
    public async Task<List<RentalDto>> GetRentalsByClientId(int clientId)
    {
        var client = await clientRepository.Read(clientId);
        if (client == null) return [];

        var rentals = await rentalRepository.Read();
        return rentals
            .Where(r => r.ClientId == clientId)
            .Select(MapperClass.ToDto)
            .ToList();
    }

    /// <summary>
    /// Get rental by ID
    /// </summary>
    public async Task<RentalDto?> GetRental(int id)
    {
        var entity = await rentalRepository.Read(id);
        return entity == null ? null : MapperClass.ToDto(entity);
    }

    /// <summary>
    /// Update rental by ID
    /// </summary>
    public async Task<RentalDto?> UpdateRental(int id, RentalCreateDto dto)
    {
        var bike = await bikeRepository.Read(dto.BikeId)
            ?? throw new ArgumentException("Invalid Bike ID");
        var client = await clientRepository.Read(dto.ClientId)
            ?? throw new ArgumentException("Invalid Client ID");

        var entity = MapperClass.ToEntity(dto);
        var updated = await rentalRepository.Update(id, entity);
        return updated == null ? null : MapperClass.ToDto(updated);
    }

    /// <summary>
    /// Delete rental by ID
    /// </summary>
    public async Task<bool> DeleteRental(int id) =>
        await rentalRepository.Delete(id);
}