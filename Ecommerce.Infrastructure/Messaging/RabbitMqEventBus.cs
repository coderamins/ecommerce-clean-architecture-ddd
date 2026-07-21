using Ecommerce.Application.Common.Abstractions.Messaging;
using Microsoft.Extensions.Options;

namespace Ecommerce.Infrastructure.Messaging
{
    public class RabbitMqEventBus : IEventBus
    {
        public RabbitMqEventBus(IOptions<RabbitMqOptions> options)
        {            
        }
        public Task PublishAsync(
            IntegrationEvent integrationEvent, 
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
