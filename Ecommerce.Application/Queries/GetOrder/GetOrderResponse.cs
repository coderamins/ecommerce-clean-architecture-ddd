
namespace Ecommerce.Application.Queries.GetOrder
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
        bool IsPaid,
        List<OrderItemResponse> Items
    );
}
