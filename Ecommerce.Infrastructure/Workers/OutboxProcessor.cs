using Ecommerce.Application.Events;
using Ecommerce.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
                            //await dispatcher.Dispatch(m);

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
