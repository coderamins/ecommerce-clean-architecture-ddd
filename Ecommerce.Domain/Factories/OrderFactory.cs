using Ecommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Domain.Factories
{
    public class OrderFactory : IOrderFactory
    {
        public Order Create(Guid customerId, List<CreateOrderItem> items)
        {
            //var order = Order.Create(
            //        customerId
            //    );

            //foreach (
            //   var item
            //   in items)
            //{
            //    order.AddItem(
            //        item.ProductName,
            //        item.Quantity,
            //        item.Price
            //    );
            //}

            //return order;
            throw new NotImplementedException();
        }
    }
}
