using System;
using System.Linq;
using Info.Initializations;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Formatter;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using StackExchange.Redis;
using todoApp.Data.UnitOfWork;

namespace todoApp.Initializations
{
    public static class DependencyInjectionConfigs
    {
        public static IServiceCollection ConfigureServices(IServiceCollection services, IConfiguration configuration = null)
        {
            services.AddOData();
            services.AddMvcCore(options =>
            {
                foreach (var outputFormatter in options.OutputFormatters.OfType<ODataOutputFormatter>().Where(_ => _.SupportedMediaTypes.Count == 0))
                {
                    outputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/prs.odatatestxx-odata"));
                }
                foreach (var inputFormatter in options.InputFormatters.OfType<ODataInputFormatter>().Where(_ => _.SupportedMediaTypes.Count == 0))
                {
                    inputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/prs.odatatestxx-odata"));
                }
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();


            ////

            var sp = services.BuildServiceProvider();
            var cachingOptions = sp.GetService<IOptions<CachingOptions>>().Value;
            var configurationOptions = new ConfigurationOptions();

            if (cachingOptions.Port != 0)
                configurationOptions.EndPoints.Add(cachingOptions.Host, cachingOptions.Port);

            services.AddSingleton<IRedisDatabaseFactory, RedisDatabaseFactory>();
            services.AddSingleton<IDistributedCache>(new RedisCache(new RedisCacheOptions()
            {
                ConfigurationOptions = configurationOptions
            }));

            services.AddSingleton<IDistributedCachingService, RedisCachingService>();
            //services.AddScoped<AuthContext>();

            return services;
        }


        public static void AddInitialization<TInitialization>(this IServiceCollection services) where TInitialization : class, IInitialization
        {
            services.AddTransient<IInitialization, TInitialization>();
        }
    }
}
