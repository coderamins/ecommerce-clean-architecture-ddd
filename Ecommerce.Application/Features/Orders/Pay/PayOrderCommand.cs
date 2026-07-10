using Ecommerce.Application.Common.Abstractions;
using MediatR;

namespace Ecommerce.Application.Features.Orders.Pay
{
    public sealed record PayOrderCommand(Guid OrderId) :
        ICommand<Unit>,
        IInvalidateCache
    {
        public IEnumerable<string> CacheKeys
        {
            get
            {
                yield return $"orders:{OrderId}";
            }
        }
    }
}
