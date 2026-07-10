namespace Ecommerce.Application.Common.Interfaces
{
    public interface ICacheService
    {
        Task<T?> GetAsync<T>(
            string key,
            CancellationToken cancellationToken=default);

        Task SetAsync<T>(
            string key, 
            T value,
            TimeSpan expiration,
            CancellationToken cancellationToken=default);

        Task RemoveAsync(
            string keu,
            CancellationToken cancellationToken=default)    ;


    }
}
