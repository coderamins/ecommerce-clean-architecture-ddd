using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Domain.Common
{
    public enum OrderStatus
    {
        Pending = 0,

        Paid = 1,

        Cancelled = 2,

        Shipped = 3,

        Delivered = 4
    }
}
