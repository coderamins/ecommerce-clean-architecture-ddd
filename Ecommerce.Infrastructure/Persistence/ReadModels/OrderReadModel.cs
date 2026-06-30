using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Infrastructure.Persistence.ReadModels
{
    public class OrderReadModel
    {
        public Guid Id { get; set; }

        public decimal Total { get; set; }

        public bool IsPaid { get; set; }

        public List<OrderItemReadModel> Items { get; set; } = [];
    }
}
