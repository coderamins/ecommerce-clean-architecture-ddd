
using Ecommerce.Application.Events;
using Ecommerce.Domain.Common;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Infrastructure.Events
{
    public class DomainEventDispatcher : IDomainEventDispatcher
    {
        public readonly IServiceProvider _provider;

        public DomainEventDispatcher(IServiceProvider provider)
        {
            _provider = provider;
        }

        public async Task Dispatch(DomainEvent domainEvent)
        {

            var handlerType =
                typeof(IDomainEventHandler<>)
                    .MakeGenericType(domainEvent.GetType());

            var handlers = _provider.GetServices(handlerType);

            foreach (var handler in handlers)
            {
                var method =
                    handlerType.GetMethod(nameof(IDomainEventHandler<DomainEvent>.Handle));

                if (method is null)
                    continue;

                await (Task)method!.Invoke(handler, new object[]
                    {
                            domainEvent
                    })!;
            }
        }
    }
}
