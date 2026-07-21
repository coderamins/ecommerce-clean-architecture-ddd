using Ecommerce.Application.Common.Abstractions.Messaging;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Ecommerce.Infrastructure.Messaging.RabbitMQ
{
    public class RabbitMqEventBus : IEventBus
    {
        private readonly IRabbitMqConnection _connection;
        private readonly ILogger<RabbitMqEventBus> _logger;
        public RabbitMqEventBus(
            ILogger<RabbitMqEventBus> logger, 
            IRabbitMqConnection connection)
        {
            _logger = logger;
            _connection = connection;
        }
        public async Task PublishAsync(
            IntegrationEvent integrationEvent, 
            CancellationToken cancellationToken = default)
        {
            var connection =
                await _connection.GetConnectoinAsync(cancellationToken);

            await using var channel =
                await connection.CreateChannelAsync(cancellationToken: cancellationToken);

            const string exchange = "ecommerce.events";

            await channel.ExchangeDeclareAsync(
                    exchange: exchange,
                    type: ExchangeType.Topic,
                    durable: true,
                    autoDelete: false,
                    cancellationToken: cancellationToken
                );

            var routingKey = integrationEvent.GetType().Name;

            var body = Encoding.UTF8.GetBytes(
                JsonSerializer.Serialize(integrationEvent));

            await channel.BasicPublishAsync(
                exchange: exchange,
                routingKey: routingKey,
                mandatory: false,
                body: body,
                cancellationToken: cancellationToken);

            _logger.LogInformation(
                "Published integration event {EventType}",
                routingKey);

        }
    }
}
