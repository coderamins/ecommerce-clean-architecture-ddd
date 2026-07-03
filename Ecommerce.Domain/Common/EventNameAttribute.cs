using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Domain.Common
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class EventNameAttribute:Attribute
    {
        public string Name { get;}
        public EventNameAttribute(string name)
        {
            Name = name;    
        }
    }
}
