using Ecommerce.Application.Commands.CreateOrder;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<CreateOrderHandler>();

            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

            return services;    
        }
    }
}
