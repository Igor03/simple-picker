using JIgor.Projects.SimplePicker.Api.Database.Contracts;
using JIgor.Projects.SimplePicker.Api.Database.DataContexts;
using JIgor.Projects.SimplePicker.Api.Database.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JIgor.Projects.SimplePicker.Api.Extensions
{
    internal static class DatabaseServicesRegistrationExtensions
    {

        public static IServiceCollection AddDatabaseServices(this IServiceCollection services, 
            IConfiguration configuration)
        {
            return services
                .AddSimplePickerDatabaseContext(configuration)
                .AddRepositories();
        }
        private static IServiceCollection AddSimplePickerDatabaseContext(this IServiceCollection services,
            IConfiguration configuration)
        {
            return services.AddDbContext<ISimplePickerDatabaseContext, SimplePickerDatabaseContext>(options =>
            {
                _ = options.UseSqlServer(configuration.GetConnectionString("mssqlserver"));
            });
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
                .AddTransient<IEventRepository, EventRepository>()
                .AddTransient<IEventValueRepository, EventValueRepository>();
        }
    }
}
