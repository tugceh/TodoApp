using System;
namespace Info.Initializations
{
    public class CachingEntryOptions
    {
        /// <summary>
        /// An absolute expiration date for the cache entry.
        /// </summary>
        public DateTimeOffset? AbsoluteExpiration { get; set; }

        /// <summary>
        /// An absolute expiration time, relative to now.
        /// </summary>
        public TimeSpan? AbsoluteExpirationRelativeToNow { get; set; }

        /// <summary>
        /// How long a cache entry can be inactive (e.g. not accessed) before it will be removed. This will not extend the entry lifetime beyond the absolute expiration (if set).
        /// </summary>
        public TimeSpan? SlidingExpiration { get; set; }
    }
}
