using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace Ecommerce.Infrastructure.Messaging.RabbitMQ
{
    public class RabbitMqConnection : IRabbitMqConnection
    {
        private readonly ConnectionFactory _factory;

        private readonly ILogger<RabbitMqConnection> _logger;

        private readonly SemaphoreSlim _lock = new(1, 1);

        private IConnection? _connection;

        public RabbitMqConnection(
            ConnectionFactory factory,
            ILogger<RabbitMqConnection> logger)
        {
            _logger = logger;
            _factory = factory;
        }

        public async ValueTask<IConnection> GetConnectoinAsync(
            CancellationToken cancellationToken = default)
        {
            if (_connection is { IsOpen: true })
                return _connection;

            await _lock.WaitAsync(cancellationToken);

            try
            {
                if (_connection is { IsOpen: true })
                    return _connection;

                _logger.LogInformation("Creating RabbitMq connection...");

                _connection = 
                    await _factory.CreateConnectionAsync(cancellationToken);

                return _connection;
            }
            finally
            {
                _lock.Release();
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (_connection is not null)
                await _connection.DisposeAsync();

            _lock.Dispose();
        }


    }
}
