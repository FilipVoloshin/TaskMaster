using Microsoft.AspNetCore.Builder;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace TaskMaster.Swagger.Extensions
{
    /// <summary>
    /// Extension method for configuring Swagger for the application.
    /// </summary>
    public static class ApplicationBuilderExtension
    {
        /// <summary>
        /// Adds Swagger middleware to the specified application builder.
        /// </summary>
        /// <param name="app">The application builder.</param>
        /// <returns>The modified application builder.</returns>
        public static IApplicationBuilder UseSwaggerModule(this IApplicationBuilder app)
        {
            app.UseSwagger(c =>
            {
                c.RouteTemplate = "swagger/{documentname}/swagger.json";
            });

            app.UseSwaggerUI(config =>
            {
                config.DisplayRequestDuration();
                config.DefaultModelsExpandDepth(-1);
                config.DocExpansion(DocExpansion.None);
                config.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                config.RoutePrefix = "swagger";
            });

            return app;
        }
    }
}
