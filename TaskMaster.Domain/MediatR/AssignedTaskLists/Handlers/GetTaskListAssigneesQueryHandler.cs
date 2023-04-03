using TaskMaster.Application.MediatR.AssignedTaskLists.Queries;
using TaskMaster.Application.MediatR.Base;
using TaskMaster.Application.ViewModels;
using TaskMaster.Infrastructure.Entities;
using TaskMaster.Infrastructure.Repositories.Abstractions;
using TaskMaster.Infrastructure.Specifications.TaskLists;
using TaskMaster.Shared.Exceptions;
using TaskMaster.Shared.Extensions;

namespace TaskMaster.Application.MediatR.AssignedTaskLists.Handlers
{
    public class GetTaskListAssigneesQueryHandler : BaseRequestHandler<GetTaskListAssigneesQuery, TaskListAssigneeVm>
    {
        public GetTaskListAssigneesQueryHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        protected override async Task<TaskListAssigneeVm> HandleAsync(GetTaskListAssigneesQuery request, CancellationToken cancellationToken = default)
        {
            var specification = new SingleTaskListSpecification(request.TaskListId, new()
            {
                AsNoTracking = true,
                CurrentUserId = CurrentUserId,
                IncludeAssignees = true
            });

            var assignedTaskList = await UnitOfWork
                .Repository<IQueryRepository<TaskList>>()
                .FirstOrDefaultAsync(specification, cancellationToken)
                .ThrowIfNullAsync<TaskList?, NotFoundException>();

            if (assignedTaskList!.Assignees.IsNullOrEmpty())
            {
                throw new NotFoundException();
            }

            var taskListName = assignedTaskList.Name;
            var assignees = Mapper.Map<IEnumerable<AssigneeVm>>(assignedTaskList.Assignees);

            return new(taskListName, assignees);
        }
    }
}
