using Ecommerce.Domain.Orders;

namespace Ecommerce.Domain.Orders.Repositories
{
    public interface IOrderRepository
    {
        Task<Order?> Get(Guid id);
        Task Save(Order order);
        Task Update(Order order);
    }
}
