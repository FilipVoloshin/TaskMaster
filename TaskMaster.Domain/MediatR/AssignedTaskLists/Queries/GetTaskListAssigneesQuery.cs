using MediatR;
using TaskMaster.Application.ViewModels;

namespace TaskMaster.Application.MediatR.AssignedTaskLists.Queries
{
    public class GetTaskListAssigneesQuery : IRequest<TaskListAssigneeVm>
    {
        public Guid TaskListId { get; set; }
    }
}
