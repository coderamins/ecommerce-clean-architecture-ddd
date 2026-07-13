using Ecommerce.Application.Common.Interfaces;
using Ecommerce.Application.Events;
using Ecommerce.Domain.Events;
using Ecommerce.Domain.Orders.Events;
using Ecommerce.Domain.Orders.Repositories;
using Ecommerce.Infrastructure.Cache;
using Ecommerce.Infrastructure.Events;
using Ecommerce.Infrastructure.Outbox;
using Ecommerce.Infrastructure.Persistence;
using Ecommerce.Infrastructure.Projections;
using Ecommerce.Infrastructure.Repositories;
using Ecommerce.Infrastructure.Workers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            string connection)
        {
            services.AddDbContext<ApplicationDbContext>(x =>
                x.UseNpgsql(connection)
            );

            services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
            services.AddScoped<IOrderReadRepository, OrderReadRepository>();
            services.AddHostedService<OutboxProcessor>();
            services.AddScoped<IOutboxMessageProcessor, OutboxMessageProcessor>();
            
            services.AddScoped<IDomainEventHandler<OrderCreated>, OrderCreatedProjection>();
            services.AddScoped<IDomainEventHandler<OrderPaid>, OrderPaidProjection>();
            services.AddScoped<IDomainEventHandler<OrderCancelled>, OrderCancelledProjection>();

            services.AddScoped<IOrderRepository, OrderRepository>();

            services.AddScoped<ITransactionManager,TransactionManager>();   

            services.AddSingleton<IEventRegistry, EventRegistry>();
            services.AddSingleton<IEventMetadataProvider,EventMetadataProvider>();

            services.AddMemoryCache();
            services.AddScoped<ICacheService, MemoryCacheService>();


            services.AddScoped<IUnitOfWork,UnitOfWork>();


            return services;
        }
    }
}
