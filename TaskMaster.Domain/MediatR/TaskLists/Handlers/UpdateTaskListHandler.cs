using MediatR;
using TaskMaster.Application.MediatR.Base;
using TaskMaster.Application.MediatR.TaskLists.Commands;
using TaskMaster.Infrastructure.Entities;
using TaskMaster.Infrastructure.Repositories.Abstractions;
using TaskMaster.Infrastructure.Specifications.TaskLists;
using TaskMaster.Shared.Exceptions;
using TaskMaster.Shared.Extensions;

namespace TaskMaster.Application.MediatR.TaskLists.Handlers
{
    public class UpdateTaskListHandler : BaseRequestHandler<UpdateTaskListCommand, Unit>
    {
        public UpdateTaskListHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        protected override async Task<Unit> HandleAsync(UpdateTaskListCommand request, CancellationToken cancellationToken = default)
        {
            var taskListWithAssignees = await UnitOfWork.Repository<IQueryRepository<TaskList>>()
                .FirstOrDefaultAsync(new SingleTaskListSpecification(request.Id, new() { IncludeAssignees = true }), cancellationToken)
                .ThrowIfNullAsync<TaskList?, NoContentException>();

            if (taskListWithAssignees!.AuthorId != CurrentUserId &&
                !taskListWithAssignees.Assignees.Any(x => x.AssigneeId == CurrentUserId))
            {
                throw new NotOwnedByYouException();
            }

            var updatedTaskList = Mapper.Map(request, taskListWithAssignees);

            UnitOfWork.Repository<ICommandRepository<TaskList>>().Update(taskListWithAssignees, updatedTaskList);
            await UnitOfWork.CompleteAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
