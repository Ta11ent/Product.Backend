namespace ProductCatalog.Application.Common.Abstractions
{
    public interface ICashService
    {
        Task<T> GetByIdAsync<T>(
            Guid key,
            Func<Task<T>> retrieveDataFunc,
            TimeSpan? slidingExpiration = null,
            CancellationToken cancellationToken = default) where T : class;

        Task CreateAsync<T>(
            Guid key,
            T data,
            TimeSpan? slidingExpiration = null,
            CancellationToken cancellationToken = default) where T : class;

        Task DeleteByIdAsync<T>(
            Guid key,
            CancellationToken cancellationToken = default);
    }
}

