using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using JIgor.Projects.SimplePicker.Api.Commons.Classes;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace JIgor.Projects.SimplePicker.Api.Middlewares
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHostEnvironment _env;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next, IHostEnvironment env)
        {
            _next = next;
            _env = env;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            _ = httpContext ?? throw new ArgumentNullException(nameof(httpContext));

            try
            {
                await _next(httpContext).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex).ConfigureAwait(false);
            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception,
            CancellationToken cancellationToken = default)
        {
            _ = httpContext ?? throw new ArgumentNullException(nameof(httpContext));
            _ = exception ?? throw new ArgumentNullException(nameof(exception));

            var error = new DefaultErrorResponse(exception.Message, 
                _env.IsDevelopment() ? (exception.StackTrace ?? string.Empty) : string.Empty, 
                (int)HttpStatusCode.InternalServerError);

            httpContext.Response.StatusCode = error.StatusCode;
            httpContext.Response.ContentType = "application/json";

            await httpContext.Response.WriteAsync(error.ToString(), cancellationToken).ConfigureAwait(false);
        }
    }
}
