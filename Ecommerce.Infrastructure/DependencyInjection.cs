using Ecommerce.Domain.Repositories;
using Ecommerce.Infrastructure.Persistence;
using Ecommerce.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

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

            services.AddScoped<IOrderRepository, OrderRepository>();

            return services;    
        }
    }
}
