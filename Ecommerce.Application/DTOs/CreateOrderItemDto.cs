using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Application.DTOs
{
    public record CreateOrderItemDto(
            string ProductName,
            int Quantity,
            decimal price
        );
}
