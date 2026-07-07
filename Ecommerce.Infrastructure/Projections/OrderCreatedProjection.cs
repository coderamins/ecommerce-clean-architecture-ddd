using Ecommerce.Application.Events;
using Ecommerce.Domain.Common;
using Ecommerce.Domain.Events;
using Ecommerce.Infrastructure.Persistence;
using Ecommerce.Infrastructure.Persistence.ReadModels;

namespace Ecommerce.Infrastructure.Projections
{
    public class OrderCreatedProjection:IDomainEventHandler<OrderCreated>
    {
        private readonly ApplicationDbContext _db;

        public OrderCreatedProjection(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task Handle(OrderCreated e)
        {
            _db.OrderReads.Add(
                 new OrderReadModel
                 {
                     Id = e.OrderId,
                     Total = e.Total,
                     Status = OrderStatus.Pending.ToString(),
                     Items = e.Items.Select(x => new OrderItemReadModel
                     {
                         ProductName = x.ProductName,
                         Quantity = x.Quantity,
                         Price = x.Price
                     }).ToList()
                 });

            await _db.SaveChangesAsync();

        }
    }
}
