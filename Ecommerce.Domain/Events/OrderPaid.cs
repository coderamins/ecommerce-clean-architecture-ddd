using Ecommerce.Domain.Common;

namespace Ecommerce.Domain.Events
{
    public sealed record OrderPaid(Guid OrderId, DateTime PaidAt) : DomainEvent;
}
