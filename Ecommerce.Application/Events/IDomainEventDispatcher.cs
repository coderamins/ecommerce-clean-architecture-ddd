using Ecommerce.Domain.Common;

namespace Ecommerce.Application.Events
{
    public interface IDomainEventDispatcher
    {
        Task Dispatch(DomainEvent domainEvent);
    }
}
