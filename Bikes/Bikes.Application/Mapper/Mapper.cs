using Bikes.Application.Contracts.Dtos;
using Bikes.Core.Entities;

namespace Bikes.Application.Mapper;

/// <summary>
/// Helper class for converting between entities and DTOs
/// </summary>
public static class MapperClass
{
    #region BikeModel

    /// <summary>
    /// Converts BikeModel entity to BikeModelDto
    /// </summary>
    public static BikeModelDto ToDto(BikeModel entity)
    {
        return entity == null
            ? throw new ArgumentNullException(nameof(entity))
            : new BikeModelDto(
                entity.Id,
                entity.Type,
                entity.WheelSize,
                entity.MaxWeight,
                entity.Weight,
                entity.BrakeType,
                entity.ModelYear,
                entity.PricePerHour);
    }

    /// <summary>
    /// Converts BikeModelCreateDto to BikeModel entity
    /// </summary>
    public static BikeModel ToEntity(BikeModelCreateDto dto)
    {
        return dto == null
            ? throw new ArgumentNullException(nameof(dto))
            : new BikeModel
            {
                Id = 0,
                Type = dto.Type,
                WheelSize = dto.WheelSize,
                MaxWeight = dto.MaxWeight,
                Weight = dto.Weight,
                BrakeType = dto.BrakeType,
                ModelYear = dto.ModelYear,
                PricePerHour = dto.PricePerHour
            };
    }

    #endregion

    #region Bike

    /// <summary>
    /// Converts Bike entity to BikeDto
    /// </summary>
    public static BikeDto ToDto(Bike entity)
    {
        return entity == null
            ? throw new ArgumentNullException(nameof(entity))
            : new BikeDto(
                entity.Id,
                entity.SerialNumber,
                entity.Color,
                ToDto(entity.Model!));
    }

    /// <summary>
    /// Converts BikeCreateDto to Bike entity
    /// </summary>
    public static Bike ToEntity(BikeCreateDto dto)
    {
        return dto == null
            ? throw new ArgumentNullException(nameof(dto))
            : new Bike
            {
                Id = 0,
                SerialNumber = dto.SerialNumber,
                Color = dto.Color,
                ModelId = dto.ModelId
            };
    }

    #endregion

    #region Client

    /// <summary>
    /// Converts Client entity to ClientDto
    /// </summary>
    public static ClientDto ToDto(Client entity)
    {
        return entity == null
            ? throw new ArgumentNullException(nameof(entity))
            : new ClientDto(
                entity.Id,
                entity.LastName,
                entity.FirstName,
                entity.MiddleName,
                entity.Phone);
    }

    /// <summary>
    /// Converts ClientCreateDto to Client entity
    /// </summary>
    public static Client ToEntity(ClientCreateDto dto)
    {
        return dto == null
            ? throw new ArgumentNullException(nameof(dto))
            : new Client
            {
                Id = 0,
                LastName = dto.LastName,
                FirstName = dto.FirstName,
                MiddleName = dto.MiddleName,
                Phone = dto.Phone
            };
    }

    #endregion

    #region Rental

    /// <summary>
    /// Converts Rental entity to RentalDto
    /// </summary>
    public static RentalDto ToDto(Rental entity)
    {
        return entity == null
            ? throw new ArgumentNullException(nameof(entity))
            : new RentalDto(
                entity.Id,
                ToDto(entity.Bike!),
                ToDto(entity.Client!),
                entity.StartTime,
                entity.DurationHours);
    }

    /// <summary>
    /// Converts RentalCreateDto to Rental entity
    /// </summary>
    public static Rental ToEntity(RentalCreateDto dto)
    {
        return dto == null
            ? throw new ArgumentNullException(nameof(dto))
            : new Rental
            {
                Id = 0,
                BikeId = dto.BikeId,
                ClientId = dto.ClientId,
                StartTime = dto.StartTime,
                DurationHours = dto.DurationHours
            };
    }

    #endregion

    #region Analytic DTOs

    /// <summary>
    /// Creates TopModelByRentalDto
    /// </summary>
    public static TopModelByRentalDto ToTopModelByRentalDto(int modelId, int totalDuration)
    {
        return new TopModelByRentalDto(modelId, totalDuration);
    }

    /// <summary>
    /// Creates TopModelByProfitDto
    /// </summary>
    public static TopModelByProfitDto ToTopModelByProfitDto(int modelId, decimal totalProfit)
    {
        return new TopModelByProfitDto(modelId, totalProfit);
    }

    /// <summary>
    /// Creates RentalDurationStatsDto
    /// </summary>
    public static RentalDurationStatsDto ToRentalDurationStatsDto(int min, int max, double avg)
    {
        return new RentalDurationStatsDto(min, max, avg);
    }

    /// <summary>
    /// Creates RentalTimeByBikeTypeDto
    /// </summary>
    public static RentalTimeByBikeTypeDto ToRentalTimeByBikeTypeDto(string bikeType, int totalHours)
    {
        return new RentalTimeByBikeTypeDto(bikeType, totalHours);
    }

    /// <summary>
    /// Creates TopClientDto
    /// </summary>
    public static TopClientDto ToTopClientDto(Client client, int rentalCount)
    {
        return new TopClientDto(
            client.Id,
            $"{client.LastName} {client.FirstName} {client.MiddleName}",
            rentalCount);
    }

    #endregion
}