using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using TaskMaster.Swagger.Filters;
using TaskMaster.Swagger.Options;

namespace TaskMaster.Swagger.Extensions
{
    /// <summary>
    /// Provides extension methods for registering Swagger services.
    /// </summary>
    public static class ServiceCollectionExtension
    {
        /// <summary>
        /// Adds Swagger services to the specified service collection.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <returns>The modified service collection.</returns>
        public static IServiceCollection AddSwaggerModule(this IServiceCollection services)
        {
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen(config =>
            {
                config.EnableAnnotations();
                config.OperationFilter<AddUserIdHeaderOperationFilter>();
            });

            services.AddSwaggerGenNewtonsoftSupport();

            return services;
        }
    }
}
