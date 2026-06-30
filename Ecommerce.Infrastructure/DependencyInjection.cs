using Ecommerce.Application.Events;
using Ecommerce.Application.Interfaces;
using Ecommerce.Domain.Repositories;
using Ecommerce.Infrastructure.Events;
using Ecommerce.Infrastructure.Persistence;
using Ecommerce.Infrastructure.Repositories;
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
            //services.AddHostedService<OutboxProcessor>();

            services.AddScoped<IOrderRepository, OrderRepository>();

            return services;
        }
    }
}
