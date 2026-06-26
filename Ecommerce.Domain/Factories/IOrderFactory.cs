using Ecommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Domain.Factories
{
    public interface IOrderFactory
    {
        Order Create(
                Guid customerId,
                List<CreateOrderItem> items
            );
    }
}
