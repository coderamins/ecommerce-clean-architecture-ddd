using Ecommerce.Application.Interfaces;
using Ecommerce.Application.Queries.GetOrder;
using Ecommerce.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Infrastructure.Repositories
{
    public class OrderReadRepository : IOrderReadRepository
    {
        private readonly ApplicationDbContext _db;

        public OrderReadRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<GetOrderResponse?> Get(Guid id)
        {
            return await _db.OrderReads
                .Where(x => x.Id == id)
                .Select(
                    x =>
                    new GetOrderResponse(
                        x.Id,
                        x.Total,
                        x.IsPaid,
                        x.Items
                        .Select(
                            i =>
                            new OrderItemResponse(
                                i.ProductName,
                                i.Quantity,
                                i.Price
                            )
                        )
                        .ToList()
                    )
                )
                .FirstOrDefaultAsync();
        }
    }
}
