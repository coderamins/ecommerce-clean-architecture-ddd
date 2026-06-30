using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Repositories;
using Ecommerce.Infrastructure.Persistence;
using Ecommerce.Infrastructure.Persistence.ReadModels;
using Microsoft.EntityFrameworkCore;
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
                .Include(x => x.Items)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Save(Order order)
        {
            _db.Orders.Add(order);
            _db.OrderReads.Add(new OrderReadModel
            {
                Id = order.Id,

                Total = order.Total.Amount,

                IsPaid = order.IsPaid,

                Items = order.Items
                    .Select(
                        x =>
                        new OrderItemReadModel
                        {
                            ProductName =
                                x.ProductName,

                            Quantity =
                                x.Quantity,

                            Price =
                                x.Price.Amount
                        }).ToList()
            });

            await _db.SaveChangesAsync();
        }
    }
}
