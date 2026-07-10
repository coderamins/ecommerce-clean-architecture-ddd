using Ecommerce.Application.Common.Abstractions;
using Ecommerce.Application.Common.Interfaces;
using MediatR;

namespace Ecommerce.Application.Behaviors
{
    public class CacheInvalidationBehavior<TRequest, TResponse> :
        IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICommand<TResponse>
    {
        private readonly ICacheService _cache;

        public CacheInvalidationBehavior(ICacheService cache)
        {
            _cache = cache;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            var response = await next();

            if (request is IInvalidateCache invalidate)
            {
                foreach (var key in invalidate.CacheKeys)
                {
                    await _cache.RemoveAsync(key, cancellationToken);
                }
            }

            return response;
        }
    }
}
