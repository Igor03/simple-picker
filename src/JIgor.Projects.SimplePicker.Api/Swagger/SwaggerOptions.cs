using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.IO;
using System.Reflection;

namespace JIgor.Projects.SimplePicker.Api.Swagger
{
    public sealed class SwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _apiVersionDescriptionProvider;

        public SwaggerOptions(IApiVersionDescriptionProvider apiVersionDescriptionProvider)
        {
            _apiVersionDescriptionProvider = apiVersionDescriptionProvider;
        }

        public void Configure(SwaggerGenOptions options)
        {
            _ = options ?? throw new ArgumentNullException(nameof(options));

            var assembly = GetType().Assembly;
            var assemblyProduct = assembly.GetCustomAttribute<AssemblyProductAttribute>()?.Product;
            var assemblyDescription = assembly.GetCustomAttribute<AssemblyDescriptionAttribute>()?.Description;

            foreach (var apiVersionDescription in _apiVersionDescriptionProvider.ApiVersionDescriptions)
            {
                var openApiInfo = new OpenApiInfo
                {
                    Description = assemblyDescription,
                    Title = assemblyProduct,
                    Version = apiVersionDescription.ApiVersion.ToString()
                };

                options.SwaggerDoc(apiVersionDescription.GroupName, openApiInfo);
            }

            // var applicationName =
            var projectPath = Path.Combine(AppContext.BaseDirectory, "JIgor.Projects.SimplePicker.Api.xml");
            options.IncludeXmlComments(projectPath);
        }
    }
}