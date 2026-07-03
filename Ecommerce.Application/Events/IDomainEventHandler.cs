using Ecommerce.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Application.Events
{
    public interface IDomainEventHandler<in TEvent> where TEvent:DomainEvent
    {
        Task Handle(TEvent domainEvent);
    }
}
