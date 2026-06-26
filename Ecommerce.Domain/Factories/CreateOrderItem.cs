using Ecommerce.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Domain.Factories
{
    public record CreateOrderItem(
            string ProductName,
            int Quantity,
            Money Price
        );
}
