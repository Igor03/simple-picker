using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace JIgor.Projects.SimplePicker.Api.Extensions
{
    public static class RequestValidatorsRegistrationExtensions
    {
        public static IServiceCollection AddRequestValidators(this IServiceCollection services)
        {
            return services.AddFluentValidation(fv =>
            {
                fv.ImplicitlyValidateRootCollectionElements = true;

                // Assembly scanning here
                fv.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            });
        }
    }
}