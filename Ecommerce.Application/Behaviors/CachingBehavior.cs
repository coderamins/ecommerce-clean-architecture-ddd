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
            Console.WriteLine("CachingBehavior Executed");
            if (request is not ICacheable cacheable)
                return await next();

            var cached =
                await _cache.GetAsync<TResponse>(
                        cacheable.CacheKey,
                        cancellationToken);

            if (cached is not null)
            {
                Console.WriteLine($"Cache HIT: {cacheable.CacheKey}");
                return cached;
            }

            Console.WriteLine($"Cache MISS: {cacheable.CacheKey}");

            var response= await next();

            await _cache.SetAsync(
                cacheable.CacheKey,
                response,
                cacheable.Expiration, 
                cancellationToken);           

            return response;
        }
    }
}
