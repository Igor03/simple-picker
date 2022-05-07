using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;

namespace JIgor.Projects.SimplePicker.Api.Swagger
{
    public class HealthChecksFilter : IDocumentFilter
    {
        public const string HealthCheckEndpoint = @"/CheckHealth";

        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var pathItem = new OpenApiPathItem();

            var operation = new OpenApiOperation();
            operation.Tags.Add(new OpenApiTag { Name = "HealthChecks" });

            var properties = new Dictionary<string, OpenApiSchema>();
            properties.Add("HealthCheck", new OpenApiSchema() { Type = "object" });

            var response = new OpenApiResponse();
            response.Content.Add("application/json", new OpenApiMediaType
            {
                Schema = new OpenApiSchema
                {
                    Type = "object",
                    AdditionalPropertiesAllowed = true,
                    Properties = properties,
                }
            });

            operation.Responses.Add("200", response);
            pathItem.AddOperation(OperationType.Get, operation);
            swaggerDoc?.Paths.Add(HealthCheckEndpoint, pathItem);
        }
    }
}