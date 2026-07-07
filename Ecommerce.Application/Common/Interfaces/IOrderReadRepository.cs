using Ecommerce.Application.Features.Orders.Get;

namespace Ecommerce.Application.Common.Interfaces
{
    public interface IOrderReadRepository
    {
        Task<GetOrderResponse?> Get(Guid id);
    }
}
