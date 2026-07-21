namespace Ecommerce.Application.Common.Abstractions.Messaging
{
    public interface IEventBus
    {
        Task PublishAsync(
            IntegrationEvent integrationEvent,
            CancellationToken cancellationToken = default);
    }
}
