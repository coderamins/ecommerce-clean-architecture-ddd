using Ecommerce.Application.Common.Abstractions;
using Ecommerce.Application.Common.Interfaces;
using MediatR;

namespace Ecommerce.Application.Behaviors
{
    public sealed class CachingBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse> where TRequest : IQuery<TResponse>
    {
        private readonly ICacheService _cache;

        public CachingBehavior(ICacheService cache)
        {
            _cache = cache;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            if (request is not ICachable cachable)
                return await next();

            var cached =
                await _cache.GetAsync<TResponse>(
                        cachable.CacheKey,
                        cancellationToken);

            if (cached is not null)
                return cached;

            var response= await next();

            await _cache.SetAsync(
                cachable.CacheKey,
                response,
                cachable.Expiration, 
                cancellationToken);

            return response;
        }
    }
}
