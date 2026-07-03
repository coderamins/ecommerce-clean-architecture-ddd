using Ecommerce.Domain.Orders.ValueObjects;

namespace Ecommerce.Domain.Factories
{
    public record CreateOrderItem(
            string ProductName,
            int Quantity,
            Money Price
        );
}
