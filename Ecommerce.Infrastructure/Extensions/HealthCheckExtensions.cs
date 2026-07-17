using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Infrastructure.Extensions
{
    public static class HealthCheckExtensions
    {
        public static IServiceCollection AddApplicationHealthChecks(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services
                .AddHealthChecks()

                .AddCheck(
                    "self",
                    () => Microsoft.Extensions.Diagnostics.HealthChecks.HealthCheckResult.Healthy(),
                    tags: ["live"])

                .AddNpgSql(
                    configuration.GetConnectionString("DefaultConnection")!,
                    tags: ["ready"]);

            return services;
        }
    }
}
