using Ecommerce.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Application.Events
{
    public interface IEventMetadataProvider
    {
        string GetEventName(DomainEvent domainEvent);
    }
}
