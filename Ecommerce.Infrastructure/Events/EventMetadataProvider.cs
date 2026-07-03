using Ecommerce.Application.Events;
using Ecommerce.Domain.Common;
using System.Reflection;

namespace Ecommerce.Infrastructure.Events
{
    public class EventMetadataProvider : IEventMetadataProvider
    {
        public string GetEventName(DomainEvent domainEvent)
        {
            var attribute = domainEvent
               .GetType()
               .GetCustomAttribute<EventNameAttribute>();

            if (attribute is null)
            {
                throw new InvalidOperationException($"Event '{domainEvent.GetType().Name}' has no EventNameAttribute.");
            }

            return attribute.Name;
        }
    }
}
