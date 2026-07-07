using Ecommerce.Application.Events;
using Ecommerce.Domain.Common;
using Ecommerce.Domain.Orders;
using Ecommerce.Domain.Orders.Repositories;
using Ecommerce.Infrastructure.Outbox;
using Ecommerce.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Ecommerce.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IEventMetadataProvider _metadataProvider;

        public OrderRepository(ApplicationDbContext db, 
            IEventMetadataProvider metadataProvider)
        {
            _db = db;
            _metadataProvider = metadataProvider;
        }

        public async Task<Order?> Get(Guid id)
        {
            return await
                _db.Orders
                .Include(x => x.Items)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Order?> GetById(Guid orderId)
        {
            return await _db.Orders
                    .Include(x => x.Items)
                    .FirstOrDefaultAsync(
                        x => x.Id == orderId);
        }

        public async Task Save(Order order)
        {
            _db.Orders.Add(order);

            AddOutboxMessages(order);

            await _db.SaveChangesAsync();
            order.ClearEvents();
        }

        public async Task Update(Order order)
        {
            AddOutboxMessages(order);

            await _db.SaveChangesAsync();

            order.ClearEvents();
        }

        private void AddOutboxMessages(Order order)
        {
            foreach (var e in order.Events)
            {
                var attribute = e.GetType()
                    .GetCustomAttribute<EventNameAttribute>();

                if (attribute is null)
                    throw new InvalidOperationException($"Event {e.GetType().Name} has no EventNameAttribute.");

                _db.Outbox.Add(new OutboxMessage
                {
                    Id = Guid.NewGuid(),
                    EventName = _metadataProvider.GetEventName(e),
                    Data = JsonSerializer.Serialize(e, e.GetType()),
                    CreatedAt = DateTime.UtcNow
                });
            }
        }
    }
}
