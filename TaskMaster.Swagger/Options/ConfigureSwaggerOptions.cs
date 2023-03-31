using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace TaskMaster.Swagger.Options
{
    /// <summary>
    /// Configures Swagger options.
    /// </summary>
    internal class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        
        /// <inheritdoc />
        public void Configure(SwaggerGenOptions options)
        {
            options.SwaggerDoc("v1", new()
            {
                Version = "v1",
                Title = "Task Master Api v1"
            });

            options.DescribeAllParametersInCamelCase();

            IncludeXmlComments(options);
        }

        /// <summary>
        /// Includes XML comments from assemblies in Swagger documentation.
        /// </summary>
        /// <param name="options">The Swagger generation options.</param>
        private static void IncludeXmlComments(SwaggerGenOptions options)
        {
            var xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml");

            foreach (var xmlFile in xmlFiles)
            {
                options.IncludeXmlComments(xmlFile);
            }
        }
    }
}
