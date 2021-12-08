using System;
using System.Collections.Generic;
using JIgor.Projects.SimplePicker.Api.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace JIgor.Projects.SimplePicker.Api.Extensions
{
    public static class SwaggerRegistrationExtensions
    {
        internal static IServiceCollection AddCustomSwagger(this IServiceCollection serviceCollection)
        {
            _ = serviceCollection ?? throw new ArgumentNullException(nameof(serviceCollection));

            return serviceCollection
                .AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerOptions>()
                .AddSwaggerGen(options =>
                {
                    options.OperationFilter<SwaggerDefaultValues>();
                });
        }

        internal static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder applicationBuilder,
            IApiVersionDescriptionProvider provider)
        {
            _ = applicationBuilder ?? throw new ArgumentNullException(nameof(applicationBuilder));
            _ = provider ?? throw new ArgumentNullException(nameof(provider));

            return applicationBuilder
                .UseSwagger()
                .UseSwaggerUI(options =>
                {
                    foreach (var description in provider.ApiVersionDescriptions)
                        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                            description.GroupName.ToUpperInvariant());
                });
        }
    }
}
