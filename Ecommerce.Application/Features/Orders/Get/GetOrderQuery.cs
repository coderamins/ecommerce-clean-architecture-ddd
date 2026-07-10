using Ecommerce.Application.Common.Abstractions;

namespace Ecommerce.Application.Features.Orders.Get
{
    public record GetOrderQuery(Guid OrderId) :
        IQuery<GetOrderResponse>,
        ICacheable
    {
        public string CacheKey
            => $"orders:{OrderId}";

        public TimeSpan Expiration
            => TimeSpan.FromMinutes(5);
    }
}
