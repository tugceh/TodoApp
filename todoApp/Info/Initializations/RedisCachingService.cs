using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace Info.Initializations
{
    public class RedisCachingService : IDistributedCachingService
    {
        private IDistributedCache DistributedCache { get; set; }
        private readonly ILogger<RedisCachingService> _logger;
        private readonly CachingOptions _cachingOptions;
        private readonly IRedisDatabaseFactory _redisDatabaseFactory;

        /// <summary>
        /// Initialization of Redis Caching Service
        /// </summary>
        /// <param name="distributedCache">Distributed Cache</param>
        /// <param name="logger">Logger</param>
        public RedisCachingService(IDistributedCache distributedCache, ILogger<RedisCachingService> logger, IOptions<CachingOptions> cachingOptions, IRedisDatabaseFactory redisDatabaseFactory)
        {
            DistributedCache = distributedCache;
            _logger = logger;
            _cachingOptions = cachingOptions?.Value;
            _redisDatabaseFactory = redisDatabaseFactory;
        }


        private static byte[] GetRaw(IDatabase db, string key)
        {
            var hashes = db.HashGetAll(key);
            foreach (var hash in hashes)
            {
                if (hash.Name == "data")
                {
                    return hash.Value;
                }
            }

            return default;
        }

        private static DistributedCacheEntryOptions ConvertOptions(CachingEntryOptions options)
        {
            return new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = options.AbsoluteExpiration,
                SlidingExpiration = options.SlidingExpiration,
                AbsoluteExpirationRelativeToNow = options.AbsoluteExpirationRelativeToNow
            };
        }

        public T Get2<T>(string key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            var data = DistributedCache.Get(key);
            if (data != null)
                return JsonHelper.DeserializeObject<T>(data);
            return default;
        }

        public T Get<T>(string key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            var db = _redisDatabaseFactory.GetDatabase();
            var data = GetRaw(db, key);
            if (data != null)
                return JsonHelper.DeserializeObject<T>(data);
            return default;
        }

        /// <summary>
        /// Async-Generic Get method of Caching Service
        /// </summary>
        /// <typeparam name="T">Generic Type</typeparam>
        /// <param name="key">Cache Key</param>
        /// <returns>Cache Value</returns>
        public async Task<T> GetAsync<T>(string key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            var data = await DistributedCache.GetAsync(key);
            if (data != null)
                return JsonHelper.DeserializeObject<T>(data);
            return default;
        }


        /// <summary>
        /// Removes the object associated with the given cache key.
        /// </summary>
        /// <param name="key">Cache Key</param>
        public void Remove(string key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            DistributedCache.Remove(key);

            _logger.LogTrace("Removed cache from redis:[Cache Key = " + key + "]");
        }

        /// <summary>
        /// Async-Removes the object associated with the given cache key.
        /// </summary>
        /// <param name="key">Cache Key</param>
        public async Task RemoveAsync(string key)
        {

            if (key == null)
                throw new ArgumentNullException(nameof(key));

            await DistributedCache.RemoveAsync(key);

            _logger.LogTrace("Removed cache from redis:[Cache Key = " + key + "]");
        }

        /// <summary>
        /// Sets the memory cache for the given caching entry options.
        /// </summary>
        /// <typeparam name="T">Generic Type</typeparam>
        /// <param name="key">Cache Key</param>
        /// <param name="value">Cache Value</param>
        /// <param name="cachingEntryOptions">Caching Entry Options (Optional Parameter)</param>
        public void Set<T>(string key, T value, CachingEntryOptions cachingEntryOptions = default)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            cachingEntryOptions ??= new CachingEntryOptions();

            DistributedCache.Set(key, JsonHelper.SerializeObject(value), ConvertOptions(cachingEntryOptions));

            _logger.LogTrace("Added cache to redis:[Cache Key = " + key + "]");
        }

        /// <summary>
        /// Async-Sets the memory cache for the given caching entry options.
        /// </summary>
        /// <typeparam name="T">Generic Type</typeparam>
        /// <param name="key">Cache Key</param>
        /// <param name="value">Cache Value</param>
        /// <param name="cachingEntryOptions">Caching Entry Options (Optional Parameter)</param>
        public async Task SetAsync<T>(string key, T value, CachingEntryOptions cachingEntryOptions = default)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            cachingEntryOptions ??= new CachingEntryOptions();

            await DistributedCache.SetAsync(key, JsonHelper.SerializeObject(value), ConvertOptions(cachingEntryOptions));

            _logger.LogTrace("Added cache to redis:[Cache Key = " + key + "]");
        }


        /// <summary>
        /// Generic method to get cache items 
        /// </summary>
        /// <typeparam name="T">Generic Type</typeparam>
        /// <param name="key">Cache Key</param>
        /// <returns>Cache Item</returns>
        public CacheItem<T> GetCacheItem<T>(string key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            var cacheValue = Get<T>(key);
            if (cacheValue != null)
                return new CacheItem<T>(key, cacheValue);
            return null;
        }

        /// <summary>
        /// Async-Generic method to get cache items
        /// </summary>
        /// <typeparam name="T">Generic Type</typeparam>
        /// <param name="key">Cache Key</param>
        /// <returns>Cache Item</returns>
        public async Task<CacheItem<T>> GetCacheItemAsync<T>(string key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            var cacheValue = await GetAsync<T>(key);
            if (cacheValue == null)
                return new CacheItem<T>(key, default);
            return null;
        }
    }
}
