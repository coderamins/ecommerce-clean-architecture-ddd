using Serilog;

namespace Ecommerce.Api.Extensions
{
    public static class HostBuilderExtensions
    {
        public static ConfigureHostBuilder AddSerilogLogging(
            this ConfigureHostBuilder host)
        {
            host.UseSerilog((context, services, configuration) =>
            {
                configuration
                    .ReadFrom.Configuration(context.Configuration)
                    .ReadFrom.Services(services)
                    .Enrich.FromLogContext();
            });

            return host;
        }
    }
}
