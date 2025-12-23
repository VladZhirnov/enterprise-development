using Bikes.Application.Contracts.Nats;
using Bikes.Application.Contracts.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NATS.Client.Core;
using System.Text.Json;

namespace Bikes.Infrastructure.Nats;

/// <summary>
/// Background service consuming Rental contracts from a NATS JetStream subject.
/// </summary>
/// <param name="connection">NATS connection.</param>
/// <param name="scopeFactory">Factory for creating service scopes.</param>
/// <param name="configuration">Application configuration.</param>
/// <param name="logger">Logger for informational and error messages.</param>
public class BikesNatsConsumer(
    INatsConnection connection,
    IServiceScopeFactory scopeFactory,
    IConfiguration configuration,
    ILogger<BikesNatsConsumer> logger
) : BackgroundService
{
    private readonly string _validatedSubject = configuration.GetSection("Nats")["ValidatedSubject"] ?? throw new KeyNotFoundException("ValidatedSubject section of Nats is missing");

    /// <summary>
    /// Subscribes to the validated NATS subject and processes incoming batches of rental contracts.
    /// </summary>
    /// <param name="stoppingToken">Cancellation token used to stop the background service.</param>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await foreach (var msg in connection.SubscribeAsync<byte[]>(_validatedSubject, cancellationToken: stoppingToken))
        {
            var batchMsg = JsonSerializer.Deserialize<BatchMessage>(msg.Data);
            try
            {
                if (batchMsg is null || batchMsg.Data is null)
                {
                    logger.LogWarning("Received null or malformed batch from {subject}", _validatedSubject);
                    await SendAck(msg.ReplyTo, new BatchAckResponse { BatchId = batchMsg?.BatchId ?? Guid.Empty });
                    continue;
                }

                using var scope = scopeFactory.CreateScope();
                var rentalService = scope.ServiceProvider.GetRequiredService<IRentalService>();
                var inserted = await rentalService.ReceiveContractList(batchMsg.Data);

                await SendAck(msg.ReplyTo, new BatchAckResponse { BatchId = batchMsg.BatchId, InsertedDtos = batchMsg.Data });
                logger.LogInformation("Processed batch {batchId}: {inserted}/{count} inserted", batchMsg.BatchId, inserted, batchMsg.Data.Count);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error processing batch {batchId}", batchMsg!.BatchId);
                await SendAck(msg.ReplyTo, new BatchAckResponse { BatchId = batchMsg.BatchId });
            }
        }
    }

    /// <summary>
    /// Sends an acknowledgment response to the specified NATS inbox.
    /// </summary>
    /// <param name="replyTo">The NATS inbox to which the acknowledgment should be sent.</param>
    /// <param name="ack">The acknowledgment containing batch ID and optionally inserted rentals.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    private async Task SendAck(string? replyTo, BatchAckResponse ack)
    {
        if (string.IsNullOrEmpty(replyTo)) return;

        try
        {
            var payload = JsonSerializer.SerializeToUtf8Bytes(ack);
            await connection.PublishAsync(replyTo, payload);
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "Failed to send ack to {replyTo}", replyTo);
        }
    }
}