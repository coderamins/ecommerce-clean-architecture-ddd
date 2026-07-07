using Ecommerce.Domain.Common;

namespace Ecommerce.Infrastructure.Persistence.ReadModels
{
    public class OrderReadModel
    {
        public Guid Id { get; set; }

        public decimal Total { get; set; }

        public string Status { get; set; } = string.Empty;

        public List<OrderItemReadModel> Items { get; set; } = [];
    }
}
