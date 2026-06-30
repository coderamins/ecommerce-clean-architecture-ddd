using Ecommerce.Domain.Events;
using Ecommerce.Infrastructure.Persistence;
using Ecommerce.Infrastructure.Persistence.ReadModels;

namespace Ecommerce.Infrastructure.Projections
{
    public class OrderCreatedProjection
    {
        private readonly ApplicationDbContext _db;

        public OrderCreatedProjection(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task Handle(OrderCreated e)
        {
            _db.OrderReads.Add(new OrderReadModel
            {
                Id=e.OrderId,
                Total=0,
                IsPaid=false,
            });

            await _db.SaveChangesAsync();

        }
    }
}
