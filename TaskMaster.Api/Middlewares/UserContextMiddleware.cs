using TaskMaster.Application.Abstractions;
using TaskMaster.Shared.Exceptions;
using static TaskMaster.Shared.Constants;

namespace TaskMaster.Api.Middlewares
{
    /// <summary>
    /// Middleware for setting user context based on the TM-User-Id header.
    /// </summary>
    public class UserContextMiddleware
    {
        private readonly RequestDelegate _next;

        public UserContextMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Sets the user context based on the TM-User-Id header and proceeds to the next middleware in the pipeline.
        /// </summary>
        /// <param name="context">The HttpContext for the current request.</param>
        /// <param name="userContext">The IUserContext instance to set the user ID in.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        /// <exception cref="MissingUserIdHeaderException">Thrown when the User ID header is missing from the request.</exception>
        /// <exception cref="InvalidUserIdHeaderException">Thrown when the provided User ID header is not in a valid format.</exception>
        public async Task InvokeAsync(HttpContext context, IUserContext userContext)
        {
            if (context.Request.Headers.TryGetValue(HttpHeaders.UserIdHeader, out var userIdString))
            {
                if (Guid.TryParse(userIdString, out var userId))
                {
                    userContext.SetUserId(userId);
                }
                else
                {
                    throw new InvalidUserIdHeaderException();
                }
            }
            else
            {
                throw new MissingUserIdHeaderException();
            }

            await _next(context);
        }
    }
}
