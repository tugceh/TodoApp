using System;
using System.Threading;
using Info.Initializations;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace Info.Initializations
{
    public class RedisDatabaseFactory : IRedisDatabaseFactory
    {
        private readonly Lazy<IConnectionMultiplexer> m_lazyConnectionMultiplexer;
        private readonly CachingOptions _cachingOptions;

        public RedisDatabaseFactory(IOptions<CachingOptions> cachingOptions)
        {
            _cachingOptions = cachingOptions?.Value;

            if (!string.IsNullOrEmpty(_cachingOptions.Host) && _cachingOptions.Port != 0)
            {
                var configOptions = new ConfigurationOptions
                {
                    EndPoints = { $"{_cachingOptions.Host}:{_cachingOptions.Port}" },
                    ConnectTimeout = 5000,
                    AbortOnConnectFail = false
                };

                m_lazyConnectionMultiplexer = new Lazy<IConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(configOptions), LazyThreadSafetyMode.PublicationOnly);
            }
        }

        private IConnectionMultiplexer Connection => m_lazyConnectionMultiplexer.Value;

        public IDatabase GetDatabase()
        {
            if (!Connection.IsConnected)
            {
                throw new Exception("Redis connection failure");
            }
            return Connection.GetDatabase();
        }
    }
}
