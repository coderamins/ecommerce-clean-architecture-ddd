
using Ecommerce.Domain.Factories;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Domain
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDomain(this IServiceCollection services) {
            services.AddScoped<IOrderFactory, OrderFactory>();

            return services;
        }
    }
}
