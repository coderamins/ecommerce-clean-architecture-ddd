using Ecommerce.Application.Events;
using Ecommerce.Domain.Common;
using Ecommerce.Domain.Events;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Ecommerce.Infrastructure.Events
{
    public class EventRegistry : IEventRegistry
    {
        private readonly Dictionary<string, Type> _events;

        public EventRegistry()
        {
            _events=Assembly.GetAssembly(typeof(DomainEvent))!
                .GetTypes().Where(x=>typeof(DomainEvent).IsAssignableFrom(x) && !x.IsAbstract)
                .Select(x=>new
                {
                    Type=x,
                    Attribute=x.GetCustomAttribute<EventNameAttribute>()
                })
                .Where(x=>x.Attribute is not null)
                .ToDictionary(x=>x.Attribute!.Name,x=>x.Type);
        }
        public Type? Resolve(string eventName)
        {
            return _events.TryGetValue(eventName,out var type) ? type : null; 
        }
    }
}
