using JIgor.Projects.SimplePicker.Api.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace JIgor.Projects.SimplePicker.Api.Extensions
{
    public static class GlobalExceptionHandlerRegistrationExtensions
    {
        public static IApplicationBuilder ConfigureCustomExceptionHandler(this IApplicationBuilder app)
        {
            return app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
        }
    }
}