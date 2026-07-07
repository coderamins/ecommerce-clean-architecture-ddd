namespace Ecommerce.Application.Features.Orders.Get
{
    public record OrderItemResponse
    (
            string ProductName,
            int Quantity,
            decimal Price
    );

    public record GetOrderResponse
    (
        Guid Id,
        decimal Total,
        string Status,
        List<OrderItemResponse> Items
    );
}
