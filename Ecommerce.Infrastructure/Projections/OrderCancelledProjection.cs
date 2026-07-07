using Ecommerce.Application.Events;
using Ecommerce.Domain.Orders.Events;
using Ecommerce.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Projections
{
    public sealed class OrderCancelledProjection : IDomainEventHandler<OrderCancelled>
    {
        private readonly ApplicationDbContext _db;

        public OrderCancelledProjection(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task Handle(OrderCancelled domainEvent)
        {
            var order = await _db.OrderReads
                .FirstOrDefaultAsync(x => x.Id == domainEvent.OrderId);

            if (order is null)
                return;

            order.Status = "Cancelled";

        }
    }
}
