using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Application.DTOs
{
    public record CreateOrderDto(
            List<CreateOrderItemDto> Items
        );
}
