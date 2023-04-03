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
    public class GetTaskListsHandler : BaseRequestHandler<GetTaskListsQuery, IEnumerable<TaskListVm>>
    {
        public GetTaskListsHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        protected override async Task<IEnumerable<TaskListVm>> HandleAsync(GetTaskListsQuery request, CancellationToken cancellationToken = default)
        {
            var taskListsSpecification = new TaskListsSpecification(new(CurrentUserId)
            {
                AsNoTracking = true,
                Pagination = new(request.PageNumber, request.PageSize),
                IncludeAssignees = true
            });

            var pagedTaskLists = await UnitOfWork.Repository<IProjectionQueryRepository<TaskList>>()
                .GetAsync<TaskListVm>(taskListsSpecification, cancellationToken)
                .ThrowIfNullOrEmptyAsync<TaskListVm, NotFoundException>();

            return pagedTaskLists;
        }
    }
}