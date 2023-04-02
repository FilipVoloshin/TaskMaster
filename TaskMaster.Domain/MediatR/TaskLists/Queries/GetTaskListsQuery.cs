using MediatR;
using TaskMaster.Application.MediatR.Base;
using TaskMaster.Application.ViewModels;

namespace TaskMaster.Application.MediatR.TaskLists.Queries
{
    public record GetTaskListsQuery : PaginationRequest, IRequest<IEnumerable<TaskListVm>> { }
}
