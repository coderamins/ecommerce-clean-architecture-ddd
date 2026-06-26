using Ecommerce.Domain.Common;
using Ecommerce.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Domain.Entities
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
                throw new Exception("Quantity Invalid!");

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
