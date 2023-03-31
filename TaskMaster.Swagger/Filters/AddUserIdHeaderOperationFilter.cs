using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using static TaskMaster.Shared.Constants;

namespace TaskMaster.Swagger.Filters
{
    /// <summary>
    /// An operation filter for adding the 'TM-User-Id' header to each Swagger operation, if it doesn't already exist.
    /// </summary>
    internal class AddUserIdHeaderOperationFilter : IOperationFilter
    {
        /// <summary>
        /// Adds a "TM-User-Id" header to all Swagger operations if one doesn't already exist.
        /// </summary>
        /// <param name="operation">The Swagger operation to modify.</param>
        /// <param name="context">The context of the operation filter.</param>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var existingHeader = operation.Parameters.FirstOrDefault(p => p.Name == HttpHeaders.UserIdHeader);
            if (existingHeader == null)
            {
                operation.Parameters.Add(new OpenApiParameter()
                {
                    Name = HttpHeaders.UserIdHeader,
                    In = ParameterLocation.Header,
                    Required = true,
                    Schema = new OpenApiSchema() { Type = "string" },
                    Description = $"The {HttpHeaders.UserIdHeader} request header, used for filtering by the user identifier (instead of authorization)"
                });
            }
        }
    }
}
