using Ecommerce.Domain.Common;
using Ecommerce.Domain.Orders.ValueObjects;

namespace Ecommerce.Domain.Orders
{
    public class OrderItem : Entity
    {
        public string ProductName { get; private set; }
        public int Quantity { get; private set; }
        public Money Price { get; private set; }
        private OrderItem() { }
        public OrderItem(
            string productName,
            int quantity,
            Money price)
        {
            if (quantity <= 0)
                throw new DomainException("Quantity Invalid!");

            ProductName = productName;
            Quantity = quantity;
            Price = price;
        }

        public Money Total()
        {
            return new Money(
                    Price.Amount * Quantity
                );
        }
    }
}
