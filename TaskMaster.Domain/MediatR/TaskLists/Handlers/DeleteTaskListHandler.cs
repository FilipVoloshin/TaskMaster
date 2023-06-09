﻿using MediatR;
using TaskMaster.Application.MediatR.Base;
using TaskMaster.Application.MediatR.TaskLists.Commands;
using TaskMaster.Infrastructure.Entities;
using TaskMaster.Infrastructure.Repositories.Abstractions;
using TaskMaster.Infrastructure.Specifications.TaskLists;
using TaskMaster.Shared.Exceptions;
using TaskMaster.Shared.Extensions;

namespace TaskMaster.Application.MediatR.TaskLists.Handlers
{
    internal class DeleteTaskListHandler : BaseRequestHandler<DeleteTaskListCommand, Unit>
    {
        public DeleteTaskListHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        protected override async Task<Unit> HandleAsync(DeleteTaskListCommand request, CancellationToken cancellationToken = default)
        {
            var currentTaskList = await UnitOfWork.Repository<IQueryRepository<TaskList>>()
                .FirstOrDefaultAsync(new SingleTaskListSpecification(request.Id), cancellationToken)
                .ThrowIfNullAsync<TaskList?, NoContentException>();

            if (currentTaskList!.AuthorId != CurrentUserId)
            {
                throw new NotOwnedByYouException();
            }

            UnitOfWork.Repository<ICommandRepository<TaskList>>().Remove(currentTaskList);
            await UnitOfWork.CompleteAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
