using MediatR;

namespace TaskMaster.Application.MediatR.TaskLists.Commands
{
    public record DeleteTaskListCommand(Guid Id) : IRequest<Unit>;
}
