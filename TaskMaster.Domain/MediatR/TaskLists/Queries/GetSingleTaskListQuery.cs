using MediatR;
using TaskMaster.Application.Abstractions;
using TaskMaster.Application.ViewModels;

namespace TaskMaster.Application.MediatR.TaskLists.Queries
{
    /// <summary>
    /// A record representing a query to retrieve a single TaskList.
    /// </summary>
    public record GetSingleTaskListQuery(Guid Id) : IHttpRequest;
}
