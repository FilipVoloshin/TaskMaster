using TaskMaster.Application.MediatR.Base;
using TaskMaster.Application.MediatR.TaskLists.Queries;
using TaskMaster.Application.ViewModels;
using TaskMaster.Infrastructure.Entities;
using TaskMaster.Infrastructure.Repositories.Abstractions;
using TaskMaster.Infrastructure.Specifications.TaskLists;
using TaskMaster.Shared.Exceptions;

namespace TaskMaster.Application.MediatR.TaskLists.Handlers
{
    public class GetSingleTaskListHandler : BaseRequestHandler<GetSingleTaskListQuery, TaskListVm>
    {
        public GetSingleTaskListHandler(IServiceProvider serviceProvider) : base(serviceProvider) { }

        protected override async Task<TaskListVm> HandleAsync(GetSingleTaskListQuery request, CancellationToken cancellationToken = default)
        {
            var result = await UnitOfWork
                    .Repository<IProjectionQueryRepository<TaskList>>()
                    .FirstOrDefaultAsync<TaskListByIdSpecification, TaskListVm>(new(request.Id, new(CurrentUserId) { IncludeAssignees = true }), 
                        cancellationToken);

            if (result == null)
            {
                throw new NotFoundException();
            }

            return result;
        }
    }
}
