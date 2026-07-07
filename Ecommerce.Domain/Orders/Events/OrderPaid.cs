using Ecommerce.Domain.Common;

namespace Ecommerce.Domain.Events
{
    [EventName(DomainEventNames.OrderPaidV1)]
    public sealed record OrderPaid(Guid OrderId, DateTime PaidAt) : DomainEvent;
}
