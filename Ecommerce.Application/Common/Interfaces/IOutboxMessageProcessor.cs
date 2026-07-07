namespace Ecommerce.Application.Common.Interfaces
{
    public interface IOutboxMessageProcessor
    {
        Task Process(Guid messageId, CancellationToken cancellationToken = default);
    }
}
