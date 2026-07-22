namespace Ecommerce.Infrastructure.Messaging.RabbitMQ
{
    public interface IRabbitMqInitializer
    {
        Task InitializeAsync(
        CancellationToken cancellationToken = default);
    }
}
