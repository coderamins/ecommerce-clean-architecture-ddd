using Ecommerce.Application.Events;
using Ecommerce.Domain.Common;
using Ecommerce.Domain.Events;
using Ecommerce.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Projections
{
    public class OrderPaidProjection : IDomainEventHandler<OrderPaid>
    {
        private readonly ApplicationDbContext _db;

        public OrderPaidProjection(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task Handle(OrderPaid domainEvent)
        {
            var order =
                await _db.OrderReads
                    .FirstAsync(x => x.Id == domainEvent.OrderId);

            order.Status = OrderStatus.Paid.ToString();

            await _db.SaveChangesAsync();
        }
    }
}
