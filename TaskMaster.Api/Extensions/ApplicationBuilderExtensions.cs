using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using System.Net.Mime;
using System.Text.Json;
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

                context.Response.StatusCode = error switch
                {
                    ApplicationException => (int)HttpStatusCode.BadRequest,
                    NotFoundException => (int)HttpStatusCode.NotFound,
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
                await context.Response.WriteAsJsonAsync(new ErrorResponse(errorMessage, context.Response.StatusCode)
                {
                    StackTrace = error?.StackTrace ?? "Stack trace is missing"
                });
            }

            #endregion
        }
    }
}
