using Ecommerce.Api.Middleware;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;

namespace Ecommerce.Api.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static WebApplication UsePresentation(
                this WebApplication app)
        {
            app.UseHttpsRedirection();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseExceptionHandler();

            app.UseGlobalExceptions();

            app.UseMiddleware<CorrelationIdMiddleware>();

            app.UseSerilogRequestLogging();

            app.UseAuthorization();

            app.MapControllers();

            app.MapHealthChecks(
                "/health/live",
                new HealthCheckOptions
                {
                    Predicate = x => x.Tags.Contains("live")
                });

            app.MapHealthChecks(
                "/health/ready",
                new HealthCheckOptions
                {
                    Predicate = x => x.Tags.Contains("ready")
                });

            return app;
        }
    }
}
