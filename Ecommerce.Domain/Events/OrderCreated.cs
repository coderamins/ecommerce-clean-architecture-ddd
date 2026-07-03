

using Ecommerce.Domain.Common;

namespace Ecommerce.Domain.Events
{
    [EventName(DomainEventNames.OrderCreatedV1)]
    public sealed record OrderCreated(Guid orderId) : DomainEvent;
}
