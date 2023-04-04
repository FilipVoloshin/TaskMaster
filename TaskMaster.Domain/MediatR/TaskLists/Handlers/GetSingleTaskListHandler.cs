using TaskMaster.Application.MediatR.Base;
using TaskMaster.Application.MediatR.TaskLists.Queries;
using TaskMaster.Application.ViewModels;
using TaskMaster.Infrastructure.Entities;
using TaskMaster.Infrastructure.Repositories.Abstractions;
using TaskMaster.Infrastructure.Specifications.TaskLists;
using TaskMaster.Shared.Exceptions;
using TaskMaster.Shared.Extensions;

namespace TaskMaster.Application.MediatR.TaskLists.Handlers
{
    public class GetSingleTaskListHandler : BaseRequestHandler<GetSingleTaskListQuery, TaskListVm>
    {
        public GetSingleTaskListHandler(IServiceProvider serviceProvider) : base(serviceProvider) { }

        protected override async Task<TaskListVm> HandleAsync(GetSingleTaskListQuery request, CancellationToken cancellationToken = default)
        {
            var result = await UnitOfWork
                    .Repository<IProjectionQueryRepository<TaskList>>()
                    .FirstOrDefaultAsync<SingleTaskListSpecification, TaskListVm>(new(request.Id, new(CurrentUserId) { IncludeAssignees = true }),
                        cancellationToken)
                    .ThrowIfNullAsync<TaskListVm?, NoContentException>();

            return result!;
        }
    }
}
