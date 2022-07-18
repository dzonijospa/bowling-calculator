using BowlingCalculator.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace BowlingCalculator.API.Middleware
{
    public class GlobalErrorHandlerMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<GlobalErrorHandlerMiddleware> _logger;

        public GlobalErrorHandlerMiddleware(RequestDelegate next, ILogger<GlobalErrorHandlerMiddleware> logger)
        {
            _logger = logger;
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                HandleException(httpContext, ex);
            }
        }

        private async void HandleException(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            int statusCode = (int)HttpStatusCode.InternalServerError;
            if (exception is Domain.Exceptions.DomainException)
            {
                _logger.LogError($"DomainException {exception.Message} {exception.StackTrace}");
                statusCode = (int)HttpStatusCode.BadRequest;
            }
            else
            {
                _logger.LogError("Status code {@StatusCode} {@Source} {@Exception} {@StackTrace}", context.Response.StatusCode, exception.TargetSite.DeclaringType.FullName, exception.Message, exception.StackTrace);              
            }

            context.Response.StatusCode = statusCode;

            await context.Response.WriteAsync(new ErrorDetails()
            {
                Message = exception.Message
            }.ToString());

        }
    }
}
