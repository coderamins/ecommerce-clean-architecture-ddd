using Ecommerce.Api.Exceptions;
using Ecommerce.Application;

namespace Ecommerce.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPresentation(
            this IServiceCollection services)
        {
            services.AddControllers();

            services.AddOpenApi();

            services.AddControllers();

            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen();

            services.AddProblemDetails();

            services.AddExceptionHandler<GlobalExceptionHandler>();

            return services;
        }
    }
}
