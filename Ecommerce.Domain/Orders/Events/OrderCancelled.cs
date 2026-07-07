using Ecommerce.Domain.Common;

namespace Ecommerce.Domain.Orders.Events
{
    [EventName(DomainEventNames.OrderCanceledV1)]
    public sealed record OrderCancelled(Guid OrderId, DateTime CancelledAt) : DomainEvent;
}
