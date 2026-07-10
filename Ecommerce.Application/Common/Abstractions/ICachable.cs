namespace Ecommerce.Application.Common.Abstractions
{
    public interface ICachable
    {
        string CacheKey { get; }
        TimeSpan Expiration { get; }
    }
}
