namespace Ecommerce.Application.Features.Orders.Create
{
    public sealed record CreateOrderItem(
        string ProductName,
        int Quantity,
        decimal Price);
}
