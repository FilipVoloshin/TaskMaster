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
                    .GetRepository<IProjectionQueryRepository<TaskList>>()
                    .FirstOrDefaultAsync<TaskListByIdSpecification, TaskListVm>(new (request.Id, new(CurrentUserId)), cancellationToken);

            // TODO: think about SingleAsync + Sequence contains no elements
            // TODO2: thnk about Extension method, which will provide chain after Repository method and will check and throw exception, if needed
            if (result == null)
            {
                throw new NotFoundException();
            }

            return result;
        }
    }
}
