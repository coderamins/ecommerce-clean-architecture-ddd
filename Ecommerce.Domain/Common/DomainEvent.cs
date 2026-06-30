using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Domain.Common
{
    public abstract class DomainEvent
    {
        public DateTime OccuredOn => DateTime.UtcNow;
    }
}
