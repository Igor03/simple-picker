using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace JIgor.Projects.SimplePicker.Api.Extensions
{
    public static class MappingProfilesRegistrationExtensions
    {
        public static IServiceCollection AddMappingProfiles(this IServiceCollection services)
        {
            return services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}