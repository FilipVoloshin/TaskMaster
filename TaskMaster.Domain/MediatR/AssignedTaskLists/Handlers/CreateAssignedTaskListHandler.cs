using TaskMaster.Application.MediatR.AssignedTaskLists.Command;
using TaskMaster.Application.MediatR.Base;
using TaskMaster.Infrastructure.Entities;
using TaskMaster.Infrastructure.Repositories.Abstractions;
using TaskMaster.Infrastructure.Specifications.TaskLists;
using TaskMaster.Shared.Exceptions;
using TaskMaster.Shared.Extensions;

namespace TaskMaster.Application.MediatR.AssignedTaskLists.Handlers
{
    public class CreateAssignedTaskListHandler : BaseRequestHandler<CreateAssignedTaskListCommand, Guid>
    {
        public CreateAssignedTaskListHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        protected override async Task<Guid> HandleAsync(CreateAssignedTaskListCommand request, CancellationToken cancellationToken = default)
        {
            var taskListWithAssignees = await UnitOfWork.Repository<IQueryRepository<TaskList>>()
               .FirstOrDefaultAsync(new SingleTaskListSpecification(request.TaskListId, new() { IncludeAssignees = true }), cancellationToken)
               .ThrowIfNullAsync<TaskList?, NoContentException>();

            if (taskListWithAssignees!.AuthorId != CurrentUserId &&
                !taskListWithAssignees.Assignees.Any(x => x.AssigneeId == CurrentUserId))
            {
                throw new NotOwnedByYouException();
            }

            var assignedTaskList = Mapper.Map<AssignedTaskList>(request);

            assignedTaskList.AuthorId = CurrentUserId;

            var id = await UnitOfWork.Repository<ICommandRepository<AssignedTaskList>>()
                .AddAsync(assignedTaskList, cancellationToken);

            await UnitOfWork.CompleteAsync(cancellationToken);

            return id;
        }
    }
}
