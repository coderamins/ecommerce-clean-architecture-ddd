

using Ecommerce.Domain.Common;
using Ecommerce.Domain.Orders.Events;

namespace Ecommerce.Domain.Events
{
    [EventName(DomainEventNames.OrderCreatedV1)]
    public sealed record OrderCreated(
        Guid OrderId,
        decimal Total,
        IReadOnlyCollection<OrderCreatedItem> Items) : DomainEvent;
}
