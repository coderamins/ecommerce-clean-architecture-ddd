using Ecommerce.Application.Events;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Repositories;
using Ecommerce.Infrastructure.Persistence;
using Ecommerce.Infrastructure.Persistence.ReadModels;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IDomainEventDispatcher _dispatcher;

        public OrderRepository(ApplicationDbContext db, IDomainEventDispatcher dispatcher)
        {
            _db = db;
            _dispatcher = dispatcher;
        }

        public async Task<Order?> Get(Guid id)
        {
            return await
                _db.Orders
                .Include(x => x.Items)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Save(Order order)
        {
            _db.Orders.Add(order);
            
            await _db.SaveChangesAsync();

            await _dispatcher.Dispatch(order.Events);
            order.ClearEvents();
        }
    }
}
