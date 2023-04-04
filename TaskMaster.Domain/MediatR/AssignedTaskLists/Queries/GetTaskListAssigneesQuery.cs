using TaskMaster.Application.Abstractions;

namespace TaskMaster.Application.MediatR.AssignedTaskLists.Queries
{
    /// <summary>
    /// Represents a query to get a list of assignees for a given task list.
    /// </summary>
    public record GetTaskListAssigneesQuery(Guid TaskListId) : IHttpRequest;
}
