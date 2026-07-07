namespace Ecommerce.Domain.Orders.Events
{
    public sealed record OrderCreatedItem(
        string ProductName,
        int Quantity,
        decimal Price);
}
