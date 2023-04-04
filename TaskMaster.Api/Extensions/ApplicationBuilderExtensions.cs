using Microsoft.AspNetCore.Diagnostics;
using Npgsql;
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

                if (error?.InnerException is NpgsqlException npgsqlEx && npgsqlEx.SqlState == PostgresErrorCodes.UniqueViolation)
                {
                    error = new ResourceConflictException("A resource with the specified constraints already exists.");
                }

                context.Response.StatusCode = error switch
                {
                    ApplicationException => (int)HttpStatusCode.BadRequest,
                    NoContentException => (int)HttpStatusCode.NoContent,
                    ResourceConflictException => (int)HttpStatusCode.Conflict,
                    InvalidUserIdHeaderException or MissingUserIdHeaderException or NotOwnedByYouException => (int)HttpStatusCode.Forbidden,
                    _ => (int)HttpStatusCode.InternalServerError,
                };

                var errorMessage = error?.Message ?? "Error message is missing";
                await WriteErrorResponseAsync(context, errorMessage, error);
            }));

            return app;

            #region Local functions

            static async Task WriteErrorResponseAsync(HttpContext context, string errorMessage, Exception? error)
            {
                context.Response.ContentType = MediaTypeNames.Application.Json;
                var errorResponse = new ErrorResponse(errorMessage, context.Response.StatusCode);

                if (error?.StackTrace != null)
                {
                    errorResponse.StackTrace = error.StackTrace;
                }

                await context.Response.WriteAsJsonAsync(errorResponse);
            }

            #endregion
        }
    }
}
