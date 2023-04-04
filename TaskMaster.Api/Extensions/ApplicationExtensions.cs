using FluentValidation;
using MediatR;
using TaskMaster.Api.Models;
using TaskMaster.Application.Abstractions;

namespace TaskMaster.Api.Extensions
{

    public static class ApplicationExtensions
    {
        /// <summary>
        /// Configures a route with an HTTP request and response using a mediator pattern.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request object.</typeparam>
        /// <param name="app">The web application instance.</param>
        /// <param name="routeInfo">The route information containing the HTTP method, endpoint, description, summary, and tag.</param>
        /// <returns>The configured web application.</returns>
        /// <exception cref="ApplicationException">Thrown when the provided HTTP verb is invalid.</exception>
        public static WebApplication Mediate<TRequest>(
            this WebApplication app,
            RouteInfo routeInfo)
            where TRequest : IHttpRequest
        {
            Delegate handler = async (IMediator mediator, IServiceProvider serviceProvider, [AsParameters] TRequest request, HttpContext context) =>
            {
                var validator = serviceProvider.GetService<IValidator<TRequest>>();

                if (validator != null)
                {
                    var validationResult = await validator.ValidateAsync(request);
                    if (!validationResult.IsValid)
                    {
                        string errorMessage = string.Join(Environment.NewLine, validationResult.Errors.Select(error => error.ErrorMessage));
                        throw new ApplicationException(errorMessage);
                    }
                }

                return await mediator.Send(request);
            };

            IEndpointConventionBuilder builder = routeInfo.HttpMethod.ToUpperInvariant() switch
            {
                var method when method == HttpMethods.Get => app.MapGet(routeInfo.Endpoint, handler),
                var method when method == HttpMethods.Post => app.MapPost(routeInfo.Endpoint, handler),
                var method when method == HttpMethods.Put => app.MapPut(routeInfo.Endpoint, handler),
                var method when method == HttpMethods.Delete => app.MapDelete(routeInfo.Endpoint, handler),
                _ => throw new ApplicationException($"Invalid HTTP verb: {routeInfo.HttpMethod}"),
            };

            if (!string.IsNullOrEmpty(routeInfo.Description))
            {
                builder.WithDescription(routeInfo!.Description);
            }

            if (!string.IsNullOrEmpty(routeInfo.Summary))
            {
                builder.WithSummary(routeInfo!.Summary);
            }

            if (!string.IsNullOrEmpty(routeInfo.Tag))
            {
                builder.WithTags(routeInfo!.Tag);
            }

            return app;
        }
    }
}