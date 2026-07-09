using Ecommerce.Application.Common.Interfaces;
using Ecommerce.Application.Features.Orders.Get;
using Ecommerce.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Repositories
{
    public class OrderReadRepository : IOrderReadRepository
    {
        private readonly ApplicationDbContext _db;

        public OrderReadRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<GetOrderResponse?> GetByIdAsync(
            Guid orderId, 
            CancellationToken cancellationToken = default)
        {
            return await _db.OrderReads
                .Where(x => x.Id == orderId)
                .Select(
                    x =>
                    new GetOrderResponse(
                        x.Id,
                        x.Total,
                        x.Status,
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
