using Ecommerce.Application.Common.Abstractions.Messaging;
using Ecommerce.Application.Common.Constants;
using Ecommerce.Application.Common.Interfaces;
using Ecommerce.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog.Context;

namespace Ecommerce.Infrastructure.Workers
{
    public class OutboxProcessor : BackgroundService
    {
        private readonly IServiceProvider _provider;
        private readonly IEventBus _eventBus;
        public OutboxProcessor(
            IServiceProvider provider, 
            IEventBus eventBus)
        {
            _provider = provider;
            _eventBus = eventBus;
        }

        protected override async Task ExecuteAsync(CancellationToken ct)
        {
            while (!ct.IsCancellationRequested)
            {
                using var scope = _provider.CreateScope();

                var db =
                    scope.ServiceProvider
                        .GetRequiredService<ApplicationDbContext>();

                var processor =
                    scope.ServiceProvider
                        .GetRequiredService<IOutboxMessageProcessor>();

                var messages =
                    await db.Outbox
                        .Where(x => x.ProcessedAt == null)
                        .ToListAsync();

                foreach (var message in messages)
                {
                    await _eventBus.PublishAsync(
                            message.EventName,
                            message.Data,
                            message.CorrelationId,
                            ct
                        );

                    message.ProcessedAt = DateTime.UtcNow;

                    using (LogContext.PushProperty(
                            CorrelationConstants.PropertyName,
                            message.CorrelationId))
                    {
                        await processor.Process(message.Id,ct);
                    }

                    await db.SaveChangesAsync(ct);
                }

                await Task.Delay(
                    TimeSpan.FromSeconds(5));
            }
        }
    }
}
