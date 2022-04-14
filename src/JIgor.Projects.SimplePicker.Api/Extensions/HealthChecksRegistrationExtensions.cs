using System.Linq;
using JIgor.Projects.SimplePicker.Api.Commons.Classes;
using JIgor.Projects.SimplePicker.Api.Commons.HealthCheckers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace JIgor.Projects.SimplePicker.Api.Extensions
{
    public static class HealthChecksRegistrationExtensions
    {
        public static IServiceCollection AddCustomHealthChecks(this IServiceCollection services)
        {
            _ = services.AddHealthChecks()
                .AddCheck<DatabaseHealthChecker>("SqlServer");

            return services;
        }

        public static IApplicationBuilder UseCustomHealthChecks(this IApplicationBuilder builder)
        {
            _ = builder.UseHealthChecks("/health", new HealthCheckOptions
            {
                ResponseWriter = async (context, report) =>
                {
                    context.Response.ContentType = "application/json";
                    var response = new HealthCheckResponse
                    {
                        Status = report.Status.ToString(),
                        Checks = report.Entries.Select(c => new HealthCheck
                        {
                            Component = c.Key,
                            Status = c.Value.Status.ToString(),
                            Description = c.Value.Description,
                        }),
                        Duration = report.TotalDuration,
                    };
                    await context.Response.WriteAsync(response.ToString());
                }
            });
            return builder;
        }
    }
}