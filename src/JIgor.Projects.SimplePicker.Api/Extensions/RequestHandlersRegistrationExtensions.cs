using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace JIgor.Projects.SimplePicker.Api.Extensions
{
    public static class RequestHandlersRegistrationExtensions
    {
        public static IServiceCollection AddRequestHandlers(this IServiceCollection services)
        {
            // Assembly scanning here
            return services.AddMediatR(Assembly.GetExecutingAssembly());
        }
    }
}
