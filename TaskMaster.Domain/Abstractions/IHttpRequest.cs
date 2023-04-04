using MediatR;
using Microsoft.AspNetCore.Http;

namespace TaskMaster.Application.Abstractions
{
    /// <summary>
    /// Represents an interface for HTTP requests that return a result of type <see cref="IResult"/>.
    /// </summary>
    public interface IHttpRequest : IRequest<IResult> { }
}
