using System;
using System.Collections.Generic;
using System.Linq;
using Info.Initializations;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;

namespace todoApp.Initializations
{
    public static class Initialization
    {
        public static void Initialize<TStartup>(string[] args) where TStartup : class
        {
            var configuration = ConfigurationFactory.Create();
            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration)
                .MinimumLevel.Override("OpenTracing.Contrib.NetCore", LogEventLevel.Warning)
                .CreateLogger();

            try
            {
                Log.Information("Starting web host");

                var builder = WebHost.CreateDefaultBuilder(args)
                    .UseSerilog()
                    .UseStartup<TStartup>()
                    .UseKestrel()
                    .UseUrls(configuration["HostUrl"])
                    .Build();

                //var seedDataList = builder.Services.GetService<IEnumerable<IInitialization>>()?.ToList();
                //if (seedDataList != null && seedDataList.Any())
                //    foreach (var seedData in seedDataList)
                //    {
                //        seedData?.Execute().GetAwaiter().GetResult();
                //    }

                builder.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
