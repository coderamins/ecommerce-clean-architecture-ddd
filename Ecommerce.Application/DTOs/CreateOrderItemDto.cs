namespace Ecommerce.Application.DTOs
{
    public record CreateOrderItemDto(
            string ProductName,
            int Quantity,
            decimal price
        );
}
