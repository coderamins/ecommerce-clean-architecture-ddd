namespace Ecommerce.Application.Interfaces
{
    public interface IOutboxMessageProcessor
    {
        Task Process(Guid messageId, CancellationToken cancellationToken = default);
    }
}
