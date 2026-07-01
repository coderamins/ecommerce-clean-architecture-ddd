using Ecommerce.Application.Events;
using Ecommerce.Domain.Common;
using Ecommerce.Domain.Events;
using Ecommerce.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json;

namespace Ecommerce.Infrastructure.Workers
{
    public class OutboxProcessor : BackgroundService
    {
        private readonly IServiceProvider _provider;

        public OutboxProcessor(IServiceProvider provider)
        {
            _provider = provider;
        }

        protected override async Task ExecuteAsync(CancellationToken ct)
        {
            while (!ct.IsCancellationRequested)
            {
                using (var scope = _provider.CreateScope())
                {
                    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                    var dispatcher = scope.ServiceProvider
                        .GetRequiredService<IDomainEventDispatcher>();

                    var messages = await db.Outbox
                        .Where(x => x.ProcessedAt == null).ToListAsync();

                    foreach (var m in messages)
                    {
                        try
                        {
                            DomainEvent? domainEvent = m.Type switch
                            {
                                nameof(OrderCreated) => JsonSerializer.Deserialize<OrderCreated>(m.Data),
                                _ => null
                            };

                            if (domainEvent is not null)
                            {
                                await dispatcher.Dispatch([domainEvent]);
                            }

                            m.ProcessedAt = DateTime.UtcNow;
                        }
                        catch
                        {
                        }
                    }

                    await db.SaveChangesAsync();

                    await Task.Delay(3000, ct);
                }
            }
        }
    }
}
