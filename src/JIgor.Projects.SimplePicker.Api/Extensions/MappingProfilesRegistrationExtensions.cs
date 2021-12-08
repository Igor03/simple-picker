using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

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
