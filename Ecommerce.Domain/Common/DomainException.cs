using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Domain.Common
{
    public class DomainException:Exception
    {
        public DomainException(
            string message):base(message) { }
    }
}
