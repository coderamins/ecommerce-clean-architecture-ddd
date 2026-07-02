

using Ecommerce.Domain.Common;

namespace Ecommerce.Domain.Events
{
    public sealed record OrderCreated(Guid orderId) : DomainEvent;
}
