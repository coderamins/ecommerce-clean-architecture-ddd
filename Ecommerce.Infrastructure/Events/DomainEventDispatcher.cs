
using Ecommerce.Application.Events;
using Ecommerce.Domain.Common;
using Ecommerce.Domain.Events;
using Ecommerce.Infrastructure.Projections;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Infrastructure.Events
{
    public class DomainEventDispatcher:IDomainEventDispatcher
    {
        public readonly IServiceProvider _provider;

        public DomainEventDispatcher(IServiceProvider provider)
        {
            _provider = provider;
        }

        public async Task Dispatch(IReadOnlyCollection<DomainEvent> events)
        {
            foreach (var item in events)
            {
                if(item is OrderCreated created)
                {
                    var _handler = _provider.GetRequiredService<OrderCreatedProjection>();
                        
                    await _handler.Handle(created);
                }
            }
        }
    }

}
