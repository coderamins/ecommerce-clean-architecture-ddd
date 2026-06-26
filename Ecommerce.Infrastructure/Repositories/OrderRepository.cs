using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Repositories;
using Ecommerce.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _db;

        public OrderRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Order?> Get(Guid id)
        {
            return await
                _db.Orders
                .FindAsync(id); 
        }

        public async Task Save(Order order)
        {
            _db.Orders.Add(order);

            await _db.SaveChangesAsync();   
        }
    }
}
