using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Domain.Common
{
    public abstract record DomainEvent
    {
        public DateTime OccuredOn { get; init; } = DateTime.UtcNow;
    }
}
