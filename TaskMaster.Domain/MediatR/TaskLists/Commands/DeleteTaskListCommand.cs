using MediatR;

namespace TaskMaster.Application.MediatR.TaskLists.Commands
{
    /// <summary>
    /// Command to delete a task list.
    /// </summary>
    public record DeleteTaskListCommand(Guid Id) : IRequest<Unit>;
}
