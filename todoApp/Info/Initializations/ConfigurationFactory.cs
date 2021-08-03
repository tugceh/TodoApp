using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace todoApp.Initializations
{
    public class ConfigurationFactory
    {
        public static IConfigurationRoot Create()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{environment}.json", optional: false, reloadOnChange: false)
                .Build();
        }
    }
}
