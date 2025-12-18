using Bikes.Application.Contracts.Dtos;
using Bikes.Application.Contracts.Services;
using Bikes.Application.Mapper;
using Bikes.Core.Entities;
using Bikes.Core.Repositories;

namespace Bikes.Application.Services;

/// <summary>
/// Analytic service for processing and analyzing bike rental data
/// </summary>
public class AnalyticService(
    IRepository<Rental> rentalRepository,
    IRepository<Bike> bikeRepository,
    IRepository<Client> clientRepository) : IAnalyticService
{
    /// <summary>
    /// Display information about all sport bikes
    /// </summary>
    public async Task<List<BikeDto>> GetSportBikes()
    {
        var bikes = await bikeRepository.Read();
        return [.. bikes
            .Where(b => b.Model?.Type == Core.Enums.BikeType.Sport)
            .Select(MapperClass.ToDto)];
    }

    /// <summary>
    /// Display top 5 models by rental duration
    /// </summary>
    public async Task<List<TopModelByRentalDto>> GetTopModelsByRentalDuration()
    {
        var rentals = await rentalRepository.Read();

        var topModels = rentals
            .Where(r => r.Bike?.Model != null)
            .GroupBy(r => new
            {
                ModelId = r.Bike!.Model!.Id,
                ModelType = r.Bike.Model.Type,
                r.Bike.Model.ModelYear,
                r.Bike.Model.BrakeType
            })
            .Select(g => new
            {
                g.Key.ModelId,
                g.Key.ModelType,
                g.Key.ModelYear,
                g.Key.BrakeType,
                TotalDuration = g.Sum(r => r.DurationHours)
            })
            .OrderByDescending(x => x.TotalDuration)
            .Take(5)
            .ToList();

        return [.. topModels
            .Select(x => MapperClass.ToTopModelByRentalDto(
                x.ModelId,
                x.ModelType.ToString(),
                x.ModelYear,
                x.BrakeType,
                x.TotalDuration))];
    }

    /// <summary>
    /// Display top 5 models by profit
    /// </summary>
    public async Task<List<TopModelByProfitDto>> GetTopModelsByProfit()
    {
        var rentals = await rentalRepository.Read();

        var topModels = rentals
            .Where(r => r.Bike?.Model != null)
            .GroupBy(r => new
            {
                ModelId = r.Bike!.Model!.Id,
                ModelType = r.Bike.Model.Type,
                r.Bike.Model.ModelYear,
                r.Bike.Model.PricePerHour
            })
            .Select(g => new
            {
                g.Key.ModelId,
                g.Key.ModelType,
                g.Key.ModelYear,
                g.Key.PricePerHour,
                TotalProfit = g.Sum(r => r.DurationHours * r.Bike!.Model!.PricePerHour)
            })
            .OrderByDescending(x => x.TotalProfit)
            .Take(5)
            .ToList();

        return [.. topModels
            .Select(x => MapperClass.ToTopModelByProfitDto(
                x.ModelId,
                x.ModelType.ToString(),
                x.ModelYear,
                x.PricePerHour,
                x.TotalProfit))];
    }

    /// <summary>
    /// Display minimum, maximum and average rental duration
    /// </summary>
    public async Task<RentalDurationStatsDto> GetRentalDurationStatistics()
    {
        var rentals = await rentalRepository.Read();

        if (rentals.Count == 0)
        {
            return new RentalDurationStatsDto(0, 0, 0);
        }

        var durations = rentals.Select(r => r.DurationHours).ToList();
        var min = durations.Min();
        var max = durations.Max();
        var avg = durations.Average();

        return MapperClass.ToRentalDurationStatsDto(min, max, avg);
    }

    /// <summary>
    /// Display total rental time for each bike type
    /// </summary>
    public async Task<List<RentalTimeByBikeTypeDto>> GetRentalTimeByBikeType()
    {
        var rentals = await rentalRepository.Read();

        var stats = rentals
            .Where(r => r.Bike?.Model != null)
            .GroupBy(r => r.Bike!.Model!.Type)
            .Select(g => new
            {
                BikeType = g.Key.ToString(),
                TotalHours = g.Sum(r => r.DurationHours)
            })
            .ToList();

        return [.. stats.Select(s => MapperClass.ToRentalTimeByBikeTypeDto(s.BikeType, s.TotalHours))];
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