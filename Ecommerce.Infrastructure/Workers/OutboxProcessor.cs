using Ecommerce.Application.Common.Interfaces;
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
                    await processor.Process(message.Id);
                }

                await Task.Delay(
                    TimeSpan.FromSeconds(5));
            }
        }
    }
}
