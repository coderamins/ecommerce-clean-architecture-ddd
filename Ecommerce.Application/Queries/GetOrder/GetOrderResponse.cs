using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Application.Queries.GetOrder
{
    public record OrderItemResponse
    (
            string ProductName,
            int Quantity,
            decimal price
    );

    public record GetOrderResponse
    (
        Guid Id,
        decimal Total,
        bool IsPaid,
        List<OrderItemResponse> Items
    );
}
