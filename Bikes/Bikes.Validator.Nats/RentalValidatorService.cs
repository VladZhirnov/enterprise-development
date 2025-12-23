using Bikes.Application.Contracts.Dtos;
using Bikes.Application.Contracts.Nats;
using Bikes.Infrastructure.EfCore.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NATS.Client.Core;
using NATS.Client.JetStream.Models;
using NATS.Net;
using System.Text.Json;

namespace Bikes.Validator.Nats;

/// <summary>
/// Background service that validates incoming rental batches from the raw NATS subject,
/// filters out invalid or duplicate rentals, and publishes valid rentals to the validated subject.
/// </summary>
public class RentalValidatorService(
    INatsConnection connection,
    IServiceScopeFactory scopeFactory,
    IConfiguration configuration,
    ILogger<RentalValidatorService> logger
) : BackgroundService
{
    private readonly string _streamName = configuration.GetSection("Nats")["StreamName"] ?? throw new KeyNotFoundException("StreamName section of Nats is missing");
    private readonly string _rawSubject = configuration.GetSection("Nats")["RawSubject"] ?? throw new KeyNotFoundException("RawSubject section of Nats is missing");
    private readonly string _validatedSubject = configuration.GetSection("Nats")["ValidatedSubject"] ?? throw new KeyNotFoundException("ValidatedSubject section of Nats is missing");

    /// <summary>
    /// Starts the background service and subscribes to the raw NATS subject to receive rental batches.
    /// </summary>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await connection.ConnectAsync();
        var context = connection.CreateJetStreamContext();
        await context.CreateOrUpdateStreamAsync(new StreamConfig(_streamName, [_validatedSubject]), stoppingToken);

        logger.LogInformation("RentalValidatorService started, subscribing to {subject}", _rawSubject);

        await foreach (var msg in connection.SubscribeAsync<byte[]>(_rawSubject, cancellationToken: stoppingToken))
        {
            _ = ProcessMessageAsync(msg, stoppingToken);
        }
    }

    /// <summary>
    /// Processes a single NATS message containing a batch of rentals.
    /// </summary>
    private async Task ProcessMessageAsync(NatsMsg<byte[]> msg, CancellationToken ct)
    {
        BatchMessage? batchMsg;
        try
        {
            batchMsg = JsonSerializer.Deserialize<BatchMessage>(msg.Data);
            if (batchMsg is null || batchMsg.Data is null)
            {
                logger.LogWarning("Malformed batch on {subject}", _rawSubject);
                await SendAck(msg.ReplyTo, new BatchAckResponse { BatchId = batchMsg?.BatchId ?? Guid.Empty });
                return;
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to deserialize batch from {subject}", _rawSubject);
            await SendAck(msg.ReplyTo, new BatchAckResponse { BatchId = Guid.Empty });
            return;
        }

        try
        {
            using var scope = scopeFactory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            var incoming = batchMsg.Data;
            var bikeIds = incoming.Select(r => r.BikeId).Distinct().ToList();
            var clientIds = incoming.Select(r => r.ClientId).Distinct().ToList();

            var existingBikes = await db.Bikes.Where(b => bikeIds.Contains(b.Id)).Select(b => b.Id).ToListAsync(ct);
            var existingClients = await db.Clients.Where(c => clientIds.Contains(c.Id)).Select(c => c.Id).ToListAsync(ct);

            var bikeSet = existingBikes.ToHashSet();
            var clientSet = existingClients.ToHashSet();

            var validated = new List<RentalCreateDto>();
            foreach (var r in incoming)
            {
                if (!bikeSet.Contains(r.BikeId))
                {
                    logger.LogDebug("Drop rental: bike {BikeId} not found", r.BikeId);
                    continue;
                }
                if (!clientSet.Contains(r.ClientId))
                {
                    logger.LogDebug("Drop rental: client {ClientId} not found", r.ClientId);
                    continue;
                }

                if (r.DurationHours < 1 || r.DurationHours > 720)
                {
                    logger.LogDebug("Drop rental: invalid duration {DurationHours}", r.DurationHours);
                    continue;
                }

                validated.Add(r);
            }

            if (validated.Count == 0)
            {
                logger.LogInformation("Batch {batchId} contains 0 valid rentals — replying 0", batchMsg.BatchId);
                await SendAck(msg.ReplyTo, new BatchAckResponse { BatchId = batchMsg.BatchId });
                return;
            }

            var outMsg = new BatchMessage { BatchId = batchMsg.BatchId, Data = validated };
            var payload = JsonSerializer.SerializeToUtf8Bytes(outMsg);

            await connection.PublishAsync(_validatedSubject, payload, replyTo: msg.ReplyTo, cancellationToken: ct);
            logger.LogInformation("Published validated batch {batchId} with {count} rentals to {subject}", batchMsg.BatchId, validated.Count, _validatedSubject);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unhandled error validating batch {batchId}", batchMsg.BatchId);
            await SendAck(msg.ReplyTo, new BatchAckResponse { BatchId = batchMsg.BatchId });
        }
    }

    /// <summary>
    /// Sends acknowledgment back to the producer indicating processing result of the batch.
    /// </summary>
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