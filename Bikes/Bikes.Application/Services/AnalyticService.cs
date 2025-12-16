using Bikes.Application.Contracts.Dtos;
using Bikes.Application.Contracts.Services;
using Bikes.Application.Mapper;
using Bikes.Core.Entities;
using Bikes.Core.Repositories;
using Bikes.Infrastructure.EfCore.Repositories;

namespace Bikes.Application.Services;

/// <summary>
/// Analytic service for processing and analyzing bike rental data
/// </summary>
public class AnalyticService(
    IRepository<Rental> rentalRepository,
    IRepository<Bike> bikeRepository,
    IRepository<Client> clientRepository,
    RentalRepository rentalRepo) : IAnalyticService
{
    /// <summary>
    /// Display information about all sport bikes
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
    /// Display top 5 models by rental duration
    /// </summary>
    public async Task<List<TopModelByRentalDto>> GetTopModelsByRentalDuration()
    {
        var topModels = await rentalRepo.GetTopModelsByRentalDuration(5);
        return topModels
            .Select(x => MapperClass.ToTopModelByRentalDto(x.ModelId, x.TotalDuration))
            .ToList();
    }

    /// <summary>
    /// Display top 5 models by profit
    /// </summary>
    public async Task<List<TopModelByProfitDto>> GetTopModelsByProfit()
    {
        var topModels = await rentalRepo.GetTopModelsByProfit(5);
        return topModels
            .Select(x => MapperClass.ToTopModelByProfitDto(x.ModelId, x.TotalProfit))
            .ToList();
    }

    /// <summary>
    /// Display minimum, maximum and average rental duration
    /// </summary>
    public async Task<RentalDurationStatsDto> GetRentalDurationStatistics()
    {
        var stats = await rentalRepo.GetRentalDurationStatistics();
        return MapperClass.ToRentalDurationStatsDto(stats.Min, stats.Max, stats.Avg);
    }

    /// <summary>
    /// Display total rental time for each bike type
    /// </summary>
    public async Task<List<RentalTimeByBikeTypeDto>> GetRentalTimeByBikeType()
    {
        var stats = await rentalRepo.GetRentalStatisticsByBikeType();
        return stats
            .Select(kvp => MapperClass.ToRentalTimeByBikeTypeDto(
                kvp.Key.ToString(),
                kvp.Value))
            .ToList();
    }

    /// <summary>
    /// Display clients who rented bikes the most times
    /// </summary>
    public async Task<List<TopClientDto>> GetTopRenters()
    {
        var clients = await clientRepository.Read();
        var rentals = await rentalRepository.Read();

        var clientRentalCounts = rentals
            .GroupBy(r => r.ClientId)
            .Select(g => new
            {
                ClientId = g.Key,
                RentalCount = g.Count()
            })
            .OrderByDescending(x => x.RentalCount)
            .Take(3)
            .ToList();

        var result = new List<TopClientDto>();
        foreach (var item in clientRentalCounts)
        {
            var client = clients.FirstOrDefault(c => c.Id == item.ClientId);
            if (client != null)
            {
                result.Add(MapperClass.ToTopClientDto(client, item.RentalCount));
            }
        }

        return result;
    }
}