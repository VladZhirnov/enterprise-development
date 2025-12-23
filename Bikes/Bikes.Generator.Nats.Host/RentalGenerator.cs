using Bikes.Application.Contracts.Dtos;
using Bogus;

namespace Bikes.Generator.Nats.Host;

/// <summary>
/// Provides functionality for generating random <see cref="RentalCreateDto"/> contracts.
/// </summary>
public static class RentalGenerator
{
    /// <summary>
    /// Generates a collection of randomly populated <see cref="RentalCreateDto"/> objects.
    /// </summary>
    /// <param name="count">The number of rental contracts to generate.</param>
    /// <returns>A list of randomly generated <see cref="RentalCreateDto"/> instances.</returns>
    public static IList<RentalCreateDto> GenerateContract(int count) =>
        new Faker<RentalCreateDto>()
            .CustomInstantiator(f => new RentalCreateDto(
                f.Random.Int(1, 10),
                f.Random.Int(1, 10),
                f.Date.Recent(),
                f.Random.Int(1, 24)
            ))
            .Generate(count);
}