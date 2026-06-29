using Ecommerce.Application.Commands.CreateOrder;
using Ecommerce.Application.Queries.GetOrder;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<CreateOrderHandler>();
            services.AddScoped<GetOrderHandler>();

            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

            return services;    
        }
    }
}
