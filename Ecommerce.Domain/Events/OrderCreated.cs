

using Ecommerce.Domain.Common;

namespace Ecommerce.Domain.Events
{
    public class OrderCreated:DomainEvent
    {
        public Guid OrderId { get; }
        public OrderCreated(Guid orderId)
        {
            OrderId = orderId;
        }
    }
}
