using Ecommerce.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Application.Events
{
    public interface IDomainEventDispatcher
    {
        Task Dispatch(IReadOnlyCollection<DomainEvent> events);
    }
}
