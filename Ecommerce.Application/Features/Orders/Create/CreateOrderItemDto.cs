namespace Ecommerce.Application.Features.Orders.Create
{
    public record CreateOrderItemDto(
            string ProductName,
            int Quantity,
            decimal price
        );
}
