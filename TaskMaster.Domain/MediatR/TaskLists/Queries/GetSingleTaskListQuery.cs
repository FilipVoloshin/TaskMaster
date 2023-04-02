using MediatR;
using TaskMaster.Application.ViewModels;

namespace TaskMaster.Application.MediatR.TaskLists.Queries
{
    public record GetSingleTaskListQuery(Guid Id): IRequest<TaskListVm> { };
}
