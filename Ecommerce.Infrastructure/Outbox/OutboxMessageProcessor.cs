using Ecommerce.Application.Events;
using Ecommerce.Application.Interfaces;
using Ecommerce.Domain.Common;
using Ecommerce.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Ecommerce.Infrastructure.Outbox
{
    public class OutboxMessageProcessor : IOutboxMessageProcessor
    {
        private readonly ApplicationDbContext _db;
        private readonly IDomainEventDispatcher _dispatcher;
        private readonly IEventRegistry _registry;

        public OutboxMessageProcessor(ApplicationDbContext db, 
            IDomainEventDispatcher dispatcher, 
            IEventRegistry registry)
        {
            _db = db;
            _dispatcher = dispatcher;
            _registry = registry;
        }

        public async Task Process(Guid messageId, CancellationToken cancellationToken = default)
        {
            var message = await _db.Outbox
                .FirstAsync(x => x.Id == messageId);

            await using var transaction=
                await _db.Database.BeginTransactionAsync();

            if(await _db.ProcessedEvents.AnyAsync(x=>x.MessageId== messageId))
            {
                return;
            }

            var type = _registry.Resolve(message.EventName);

            var domainEvent = JsonSerializer.Deserialize(message.Data, type!);

            await _dispatcher.Dispatch((DomainEvent)domainEvent!);

            _db.ProcessedEvents.Add(new ProcessedEvent
            {
                Id = Guid.NewGuid(),
                MessageId = message.Id,
                ProcessedAt = DateTime.UtcNow,
            });

            message.ProcessedAt = DateTime.UtcNow;

            await _db.SaveChangesAsync();
            await transaction.CommitAsync();
        }
    }
}
