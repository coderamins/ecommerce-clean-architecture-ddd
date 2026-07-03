using Ecommerce.Application.Queries.GetOrder;

namespace Ecommerce.Application.Interfaces
{
    public interface IOrderReadRepository
    {
        Task<GetOrderResponse?> Get(Guid id);
    }
}
