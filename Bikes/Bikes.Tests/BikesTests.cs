using Bikes.Domain.Enum;
using Xunit;

namespace Bikes.Tests;

/// <summary>
/// Unit tests for bike rental system queries
/// </summary>
public class BikesTests(BikesFixture fixture) : IClassFixture<BikesFixture>
{
    /// <summary>
    /// Test 1: Display information about all sport bikes
    /// </summary>
    [Fact]  
    public void SportBikesList()
    {
        var expectedBikeIds = new List<int> { 1, 5, 8 };

        var actualIds = fixture.Bikes
            .Where(bike => bike.Model.Type == BikeType.Sport)
            .Select(bike => bike.Id)
            .ToList();

        Assert.Equal(expectedBikeIds, actualIds);
    }

    /// <summary>
    /// Test 2: Display top 5 bike models by rental duration
    /// </summary>
    [Fact]
    public void TopModelsByRentalTime()
    {
        var expectedModelIds = new List<int> { 10, 1, 5, 2, 3 };

        var actualIds = fixture.Rentals
            .GroupBy(rent => rent.Bike.Model.Id)
            .Select(group => new
            {
                ModelId = group.Key,
                TotalDuration = group.Sum(rent => rent.DurationHours)
            })
            .OrderByDescending(x => x.TotalDuration)
            .Select(x => x.ModelId)
            .Take(5)
            .ToList();

        Assert.Equal(expectedModelIds, actualIds);
    }

    /// <summary>
    /// Test 3: Display top 5 bike models by rental profit
    /// </summary>
    [Fact]
    public void TopModelsByProfit()
    {
        var expectedModelIds = new List<int> { 10, 5, 1, 2, 6 };

        var actualIds = fixture.Rentals
            .GroupBy(rent => rent.Bike.Model.Id)
            .Select(group => new
            {
                ModelId = group.Key,
                TotalProfit = group.Sum(rent => rent.DurationHours * rent.Bike.Model.PricePerHour)
            })
            .OrderByDescending(x => x.TotalProfit)
            .Select(x => x.ModelId)
            .Take(5)
            .ToList();

        Assert.Equal(expectedModelIds, actualIds);
    }

    /// <summary>
    /// Test 4: Display minimum, maximum and average bike rental duration
    /// </summary>
    [Fact]
    public void RentalDurationStats()
    {
        const int expectedMin = 1;
        const int expectedMax = 20;
        const double expectedAvg = 8.1;

        var durations = fixture.Rentals.Select(rent => rent.DurationHours).ToList();
        var actualMin = durations.Min();
        var actualMax = durations.Max();
        var actualAvg = durations.Average();

        Assert.Equal(expectedMin, actualMin);
        Assert.Equal(expectedMax, actualMax);
        Assert.Equal(expectedAvg, actualAvg);
    }

    /// <summary>
    /// Test 5: Display total rental duration for each bike type
    /// </summary>
    [Theory]
    [InlineData(BikeType.Sport, 47)]
    [InlineData(BikeType.Mountain, 26)]
    [InlineData(BikeType.Road, 56)]
    [InlineData(BikeType.Hybrid, 16)]
    [InlineData(BikeType.Electric, 6)]
    [InlineData(BikeType.BMX, 11)]
    public void RentalTimeByBikeType(BikeType bikeType, int expectedRentalTime)
    {
        var actualRentalTime = fixture.Rentals
            .Where(rent => rent.Bike.Model.Type == bikeType)
            .Sum(rent => rent.DurationHours);

        Assert.Equal(expectedRentalTime, actualRentalTime);
    }

    /// <summary>
    /// Test 6: Display clients who rented bikes the most times
    /// </summary>
    [Fact]
    public void TopRenters()
    {
        var expectedTopClientsIds = new List<int> { 1, 2, 6 };

        var actualTopClientsIds = fixture.Rentals
            .GroupBy(rent => rent.Client.Id)
            .Select(group => new
            {
                ClientId = group.Key,
                TotalRentals = group.Count()
            })
            .OrderByDescending(r => r.TotalRentals)
            .Select(x => x.ClientId)
            .Take(3)
            .ToList();

        Assert.Equal(expectedTopClientsIds, actualTopClientsIds);
    }
}