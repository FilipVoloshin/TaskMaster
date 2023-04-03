using MediatR;
using TaskMaster.Application.MediatR.AssignedTaskLists.Command;
using TaskMaster.Application.MediatR.Base;
using TaskMaster.Infrastructure.Entities;
using TaskMaster.Infrastructure.Filters;
using TaskMaster.Infrastructure.Repositories.Abstractions;
using TaskMaster.Infrastructure.Specifications.AssignedTaskLists;
using TaskMaster.Shared.Exceptions;
using TaskMaster.Shared.Extensions;

namespace TaskMaster.Application.MediatR.AssignedTaskLists.Handlers
{
    public class DeleteAssignedTaskListHandler : BaseRequestHandler<DeleteAssignedTaskListCommand, Unit>
    {
        public DeleteAssignedTaskListHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        protected override async Task<Unit> HandleAsync(DeleteAssignedTaskListCommand request, CancellationToken cancellationToken = default)
        {
            var assignedTaskListSpecification = new SingleAssignedTaskListSpecification(new AssignedTaskListFilter()
            {
                TaskListId = request.TaskListId,
                AssigneeId = request.AssigneeId,
                IncludeTaskList = true
            });

            var assignedTaskList = await UnitOfWork.Repository<IQueryRepository<AssignedTaskList>>()
                .FirstOrDefaultAsync(assignedTaskListSpecification, cancellationToken)
                .ThrowIfNullAsync<AssignedTaskList?, NotFoundException>();

            if (assignedTaskList!.AuthorId != CurrentUserId &&
               !assignedTaskList!.TaskList!.Assignees.Any(x => x.AssigneeId == CurrentUserId))
            {
                throw new NotOwnedByYouException();
            }

            UnitOfWork.Repository<ICommandRepository<AssignedTaskList>>()
                .Remove(assignedTaskList);

            await UnitOfWork.SaveChangesAsync(cancellationToken);

             return Unit.Value;
        }
    }
}
