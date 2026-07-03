using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Application.Events
{
    public interface IEventRegistry
    {
        Type? Resolve(string eventName);
    }
}
