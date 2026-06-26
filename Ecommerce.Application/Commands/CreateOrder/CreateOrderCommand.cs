using Ecommerce.Application.DTOs;

namespace Ecommerce.Application.Commands.CreateOrder
{
    public record CreateOrderCommand(
            CreateOrderDto Order
        );
}
