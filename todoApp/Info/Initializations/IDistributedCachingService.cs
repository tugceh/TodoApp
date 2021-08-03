using System;
using System.Threading.Tasks;

namespace Info.Initializations
{
    public interface IDistributedCachingService
    {
        /// <summary>
        /// Generic Get method of Caching Service
        /// </summary>
        /// <typeparam name="T">Generic Type</typeparam>
        /// <param name="key">Cache Key</param>
        /// <returns>Cache Value</returns>
        T Get<T>(string key);

        /// <summary>
        /// Async-Generic Get method of Caching Service
        /// </summary>
        /// <typeparam name="T">Generic Type</typeparam>
        /// <param name="key">Cache Key</param>
        /// <returns>Cache Value</returns>
        Task<T> GetAsync<T>(string key);

        /// <summary>
        /// Generic method to get cache items of Caching Service
        /// </summary>
        /// <typeparam name="T">Generic Type</typeparam>
        /// <param name="key">Cache Key</param>
        /// <returns>Cache Item</returns>
        CacheItem<T> GetCacheItem<T>(string key);

        /// <summary>
        /// Async-Generic method to get cache items
        /// </summary>
        /// <typeparam name="T">Generic Type</typeparam>
        /// <param name="key">Cache Key</param>
        /// <returns>Cache Item</returns>
        Task<CacheItem<T>> GetCacheItemAsync<T>(string key);

        /// <summary>
        /// Sets the cache for the given caching entry options.
        /// </summary>
        /// <typeparam name="T">Generic Type</typeparam>
        /// <param name="key">Cache Key</param>
        /// <param name="value">Cache Value</param>
        /// <param name="cachingEntryOptions">Caching Entry Options (Optional Parameter)</param>
        void Set<T>(string key, T value, CachingEntryOptions cachingEntryOptions = default(CachingEntryOptions));

        /// <summary>
        /// Async-Sets the cache for the given caching entry options.
        /// </summary>
        /// <typeparam name="T">Generic Type</typeparam>
        /// <param name="key">Cache Key</param>
        /// <param name="value">Cache Value</param>
        /// <param name="cachingEntryOptions">Caching Entry Options (Optional Parameter)</param>
        Task SetAsync<T>(string key, T value, CachingEntryOptions cachingEntryOptions = default(CachingEntryOptions));

        /// <summary>
        /// Removes the object associated with the given cache key.
        /// </summary>
        /// <param name="key">Cache Key</param>
        void Remove(string key);

        /// <summary>
        /// Async-Removes the object associated with the given cache key.
        /// </summary>
        /// <param name="key">Cache Key</param>
        Task RemoveAsync(string key);
    }
}
