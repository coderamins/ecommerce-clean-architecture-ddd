using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace Ecommerce.Infrastructure.Messaging.RabbitMQ
{
    public class RabbitMqInitializer : IRabbitMqInitializer
    {
        private readonly ILogger<IRabbitMqInitializer> _logger;
        private readonly IRabbitMqConnection _connection;
        public RabbitMqInitializer(
            IRabbitMqConnection connection,
            ILogger<IRabbitMqInitializer> logger)
        {
            _connection=connection;
            _logger = logger;
        }
        public async Task InitializeAsync(
            CancellationToken cancellationToken = default)
        {
            var connection = await _connection
                .GetConnectoinAsync(cancellationToken);

            await using var channel =
                await connection
                .CreateChannelAsync(cancellationToken: cancellationToken);

            const string queue = "orders";
            await channel.QueueDeclareAsync(
                queue: queue,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null,
                cancellationToken: cancellationToken);

            const string exchange = "ecommerce.events";

            await channel.ExchangeDeclareAsync(
                exchange: exchange,
                type: ExchangeType.Topic,
                durable: true,
                autoDelete: false,
                cancellationToken: cancellationToken);

            await channel.QueueBindAsync(
                queue: queue,
                exchange: exchange,
                routingKey: "#",
                cancellationToken: cancellationToken);

            _logger.LogInformation(
                "RabbitMQ topology initialized.");

        }
    }
}
