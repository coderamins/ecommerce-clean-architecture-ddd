using Ecommerce.Application.Common.Interfaces;
using Ecommerce.Application.Events;
using Ecommerce.Domain.Events;
using Ecommerce.Domain.Orders.Events;
using Ecommerce.Domain.Orders.Repositories;
using Ecommerce.Infrastructure.Cache;
using Ecommerce.Infrastructure.Events;
using Ecommerce.Infrastructure.Extensions;
using Ecommerce.Infrastructure.Messaging;
using Ecommerce.Infrastructure.Outbox;
using Ecommerce.Infrastructure.Persistence;
using Ecommerce.Infrastructure.Projections;
using Ecommerce.Infrastructure.Repositories;
using Ecommerce.Infrastructure.Services;
using Ecommerce.Infrastructure.Workers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Infrastructure.DependencyInjection
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(x =>
                x.UseNpgsql(configuration.GetConnectionString("DefaultConnection")?? "")
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

            services.AddHttpContextAccessor();
            services.AddScoped<IRequestContext, RequestContext>();

            services.AddScoped<IUnitOfWork,UnitOfWork>();

            services.AddApplicationHealthChecks(configuration);

            services.Configure<RabbitMqOptions>(
                 configuration.GetSection(RabbitMqOptions.SectionName));

            return services;
        }
    }
}
