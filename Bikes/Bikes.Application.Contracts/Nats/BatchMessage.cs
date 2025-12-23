using Bikes.Application.Contracts.Dtos;

namespace Bikes.Application.Contracts.Nats;

/// <summary>
/// Represents a batch message sent from the producer to the consumer.
/// </summary>
public class BatchMessage
{
    /// <summary>
    /// The unique identifier of the batch.
    /// </summary>
    public Guid BatchId { get; set; }

    /// <summary>
    /// The list of rental contracts included in the batch.
    /// </summary>
    public List<RentalCreateDto> Data { get; set; } = null!;
}