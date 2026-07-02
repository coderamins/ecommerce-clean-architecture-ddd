using Ecommerce.Domain.Entities;

namespace Ecommerce.Domain.Repositories
{
    public interface IOrderRepository
    {
        Task<Order?> Get(Guid id);
        Task Save(Order order);
        Task Update(Order order);
    }
}
