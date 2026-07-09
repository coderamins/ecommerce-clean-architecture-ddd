using Ecommerce.Application.Behaviors;
using Ecommerce.Application.Features.Orders.Create;
using Ecommerce.Application.Validation;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(typeof(CreateOrderHandler).Assembly);
            });

            services.AddValidatorsFromAssembly(typeof(CreateOrderValidator).Assembly);

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            services.AddTransient(typeof(IPipelineBehavior<,>),typeof(TransactionBehavior<,>));

            return services;
        }
    }
}
