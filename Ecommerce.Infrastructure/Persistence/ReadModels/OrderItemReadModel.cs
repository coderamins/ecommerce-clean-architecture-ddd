using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Infrastructure.Persistence.ReadModels
{
    public class OrderItemReadModel
    {
        public string ProductName
        {
            get;
            set;
        } = "";

        public int Quantity
        {
            get;
            set;
        }

        public decimal Price
        {
            get;
            set;
        }
    }
}
