using RabbitMQ.Client;

namespace Ecommerce.Infrastructure.Messaging.RabbitMQ
{
    public interface IRabbitMqConnection:IAsyncDisposable
    {
        ValueTask<IConnection> GetConnectoinAsync(
            CancellationToken cancellationToken=default);
    }
}
