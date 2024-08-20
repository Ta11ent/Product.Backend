using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using ProductCatalog.Application.Common.Abstractions;

namespace ProductCatalog.Infrastructure.Services
{
    public class CashService : ICashService
    {
        private readonly IDistributedCache _distributedCache;
        public CashService(IDistributedCache distributedCache) 
            => _distributedCache = distributedCache ?? throw new ArgumentNullException(nameof(distributedCache));
        public async Task<T> GetByIdAsync<T>(
            Guid key, 
            Func<Task<T>> retrieveDataFunc, 
            TimeSpan? slidingExpiration = null,
            CancellationToken cancellationToken = default) where T : class
        {
            var entityKey = GenerateRedisKey<T>(key);
            string? stringData = await _distributedCache.GetStringAsync(entityKey, cancellationToken);

            T data;
            if (string.IsNullOrEmpty(stringData))
            {
                data = await retrieveDataFunc();
                if (data is null) return data!;

                await CreateAsync<T>(
                    key, 
                    data, 
                    null, 
                    cancellationToken);

                return data;
            }
            return JsonConvert.DeserializeObject<T>(stringData)!;
        }

        public async Task CreateAsync<T>(
            Guid key, 
            T data, 
            TimeSpan? slidingExpiration = null, 
            CancellationToken cancellationToken = default) where T : class
        {
            var entityKey = GenerateRedisKey<T>(key);
            var cacheEntryOptions = new DistributedCacheEntryOptions
            {
                SlidingExpiration = slidingExpiration ?? TimeSpan.FromMinutes(60)
            };

            await _distributedCache.SetStringAsync(
               entityKey,
               JsonConvert.SerializeObject(data),
               cacheEntryOptions,
               cancellationToken);
        }

        public async Task DeleteByIdAsync<T>(
            Guid key,
            CancellationToken cancellationToken = default)
        {
            var entityKey = GenerateRedisKey<T>(key);
            string? stringData = await _distributedCache.GetStringAsync(entityKey, cancellationToken);
            if (!string.IsNullOrEmpty(stringData))
            {
                await _distributedCache.RemoveAsync(entityKey, cancellationToken);
            }
        }
        private string GenerateRedisKey<T>(Guid key) => $"{typeof(T)}-{key}";
    }
}
