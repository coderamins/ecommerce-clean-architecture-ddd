namespace Ecommerce.Application.Common.Abstractions
{
    public interface IInvalidateCache
    {
        IEnumerable<string> CacheKeys { get; }
    }
}
