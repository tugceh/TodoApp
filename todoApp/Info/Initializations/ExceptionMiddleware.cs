using System;
using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Info.Initializations
{
    public static class ExceptionMiddleware
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILogger logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        var errorDetails = new ErrorDetails()
                        {
                            message = contextFeature.Error.Message,
                            isSuccess = false
                        };

                        logger.LogError(errorDetails.ToString());

                        await context.Response.WriteAsync("ExceptionHandler:" + errorDetails.ToString());
                    }
                });
            });
        }
    }

    public class ErrorDetails
    {
        public string message { get; set; }
        public bool isSuccess { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
