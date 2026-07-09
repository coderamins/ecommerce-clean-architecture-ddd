using Ecommerce.Application.Features.Orders.Get;

namespace Ecommerce.Application.Common.Interfaces
{
    public interface IOrderReadRepository
    {
        Task<GetOrderResponse?> GetByIdAsync(
            Guid orderId,
            CancellationToken cancellationToken = default);
    }
}
