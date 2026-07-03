using Ecommerce.Application.Events;
using Ecommerce.Application.Interfaces;
using Ecommerce.Domain.Events;
using Ecommerce.Domain.Orders.Repositories;
using Ecommerce.Infrastructure.Events;
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
            services.AddScoped<IDomainEventHandler<OrderPaid>, OrderPaidProjection>();
            services.AddScoped<IOrderRepository, OrderRepository>();
    
            services.AddSingleton<IEventRegistry, EventRegistry>();
            services.AddSingleton<IEventMetadataProvider,EventMetadataProvider>();


            return services;
        }
    }
}
