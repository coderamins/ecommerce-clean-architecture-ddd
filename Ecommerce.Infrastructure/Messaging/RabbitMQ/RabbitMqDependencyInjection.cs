using Ecommerce.Application.Common.Abstractions.Messaging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace Ecommerce.Infrastructure.Messaging.RabbitMQ
{
    public static class RabbitMqDependencyInjection
    {
        public static IServiceCollection AddRabbitMq(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<RabbitMqOptions>(
                configuration.GetSection(RabbitMqOptions.SectionName));

            services.AddSingleton(sp =>
            {
                var options = sp
                    .GetRequiredService<IOptions<RabbitMqOptions>>().Value;

                return new ConnectionFactory
                {
                    HostName = options.Host,
                    Port = options.Port,
                    UserName = options.UserName,
                    Password = options.Password,
                };
            });

            services.AddSingleton<IRabbitMqConnection, RabbitMqConnection>();

            services.AddSingleton<IEventBus, RabbitMqEventBus>();

            services.AddSingleton<IRabbitMqInitializer, RabbitMqInitializer>();

            return services;
        }
    }
}
