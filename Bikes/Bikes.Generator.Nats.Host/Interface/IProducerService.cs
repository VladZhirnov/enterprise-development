using Bikes.Application.Contracts.Dtos;
using Bikes.Application.Contracts.Nats;

namespace Bikes.Generator.Nats.Host.Interface;

/// <summary>
/// Interface of a service responsible for sending messages over the bus.
/// </summary>
public interface IProducerService
{
    /// <summary>
    /// Sends a collection of contracts.
    /// </summary>
    /// <param name="batch">Collection of contracts.</param>
    public Task<BatchAckResponse> SendAsync(IList<RentalCreateDto> batch);
}