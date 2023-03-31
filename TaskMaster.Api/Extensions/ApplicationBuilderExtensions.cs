using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using System.Net.Mime;
using TaskMaster.Api.Middlewares;
using TaskMaster.Api.Models;
using TaskMaster.Shared.Exceptions;

namespace TaskMaster.Api.Extensions
{
    /// <summary>
    /// Provides extension methods for the <see cref="IApplicationBuilder"/> interface to register middleware for handling exceptions and errors.
    /// </summary>
    internal static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Registers all required middlewares for the application
        /// </summary>
        /// <param name="app">The application builder instance.</param>
        /// <returns>The application builder instance.</returns>
        internal static IApplicationBuilder UseApplicationMiddlewares(this IApplicationBuilder app)
        {
            app.UseExceptionMiddleware();
            app.UseMiddleware<UserContextMiddleware>();

            return app;
        }

        /// <summary>
        /// Registers middleware for handling exceptions and returning them in a standardized format.
        /// </summary>
        /// <param name="app">The application builder instance.</param>
        /// <returns>The application builder instance.</returns>
        internal static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(a => a.Run(async context =>
            {
                var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();

                var error = exceptionHandlerPathFeature?.Error;

                context.Response.StatusCode = error switch
                {
                    ApplicationException => (int)HttpStatusCode.BadRequest,
                    KeyNotFoundException => (int)HttpStatusCode.NotFound,
                    InvalidUserIdHeaderException or 
                    MissingUserIdHeaderException => (int)HttpStatusCode.Forbidden,
                    _ => (int)HttpStatusCode.InternalServerError,
                };

                context.Response.ContentType = MediaTypeNames.Application.Json;
                var errorMessage = error?.Message ?? "Error message is missing";
                await context.Response.WriteAsJsonAsync(new ErrorResponse(errorMessage, context.Response.StatusCode)
                {
                    StackTrace = error?.StackTrace ?? "Stack trace is missing"
                });
            }));

            return app;
        }
    }
}
