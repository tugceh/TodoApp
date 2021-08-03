using System;
namespace Info.Initializations
{
    public class CacheItem<T> // HAN CacheItem<K,V> should be helpfull too
    {
        public string Key { get; set; }
        public T Value { get; set; }

        /// <summary>
        /// Initialization of CacheItem
        /// </summary>
        /// <param name="key">The Cache Key</param>
        /// <param name="value">The cache value derived by cache type</param>
        public CacheItem(string key, T value)
        {
            Key = key;
            Value = value;
        }
    }
}
