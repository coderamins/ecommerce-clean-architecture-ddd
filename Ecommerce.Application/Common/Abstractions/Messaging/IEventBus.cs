namespace Ecommerce.Application.Common.Abstractions.Messaging
{
    public interface IEventBus
    {
        Task PublishAsync(
            string eventName,
            string payload,
            string correlationId,
            //IntegrationEvent integrationEvent,
            CancellationToken cancellationToken = default);
    }
}
