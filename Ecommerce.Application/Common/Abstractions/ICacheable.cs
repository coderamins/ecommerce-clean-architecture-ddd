namespace Ecommerce.Application.Common.Abstractions
{
    public interface ICacheable
    {
        string CacheKey { get; }
        TimeSpan Expiration { get; }
    }
}
