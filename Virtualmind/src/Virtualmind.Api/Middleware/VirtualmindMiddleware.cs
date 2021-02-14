using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Virtualmind.Api.Exceptions;

namespace Virtualmind.Api.Middleware
{
    public class VirtualmindMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerFactory _loggerFactory;
        private readonly ILogger _logger;
        private readonly IHostEnvironment _env;

        public VirtualmindMiddleware(RequestDelegate next, ILoggerFactory loggerFactory, IHostEnvironment env)
        {
            _env = env;
            _loggerFactory = loggerFactory;
            _next = next;

            _logger = _loggerFactory.CreateLogger<VirtualmindMiddleware>();
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                if (_next != null)
                {
                    await _next(context);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var exception = ex;

            var status = exception is UnsupportedCurrencyException ? HttpStatusCode.BadRequest :
                exception is NotFoundException ? HttpStatusCode.NotFound :
                 exception is BusinessException ? HttpStatusCode.BadRequest
                 : HttpStatusCode.InternalServerError;

            var errorResponse = new ExceptionResponse(exception.Message, NotificationType.Error);



            if (ex is BusinessException businessException)
            {
                errorResponse.Title = businessException.Title;
            }

            await ProccessResponse(context, status, errorResponse);
        }

        private async Task ProccessResponse(HttpContext context, HttpStatusCode status, ExceptionResponse errorResponse)
        {
            var errrorReturn = JsonConvert.SerializeObject(errorResponse);

            _logger.LogError(errrorReturn);

            var responseReturn = JsonConvert.SerializeObject(errorResponse);

            context.Response.StatusCode = (int)status;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(responseReturn);
        }

    }

    // Extension method used to add the middleware to the HTTP request pipeline, on Startup.
    public static class VBaseProjectMiddlewareExtensions
    {
        public static IApplicationBuilder UseVirtualmindMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<VirtualmindMiddleware>();
        }
    }

}
