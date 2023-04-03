using FluentValidation;
using MediatR;
using TaskMaster.Application.MediatR.Base;
using TaskMaster.Application.ViewModels;
using static TaskMaster.Shared.Constants;

namespace TaskMaster.Application.MediatR.TaskLists.Queries
{
    public class GetTaskListsQuery : PaginationRequest, IRequest<IEnumerable<TaskListVm>> { }

    public class GetTaskListsQueryValidator : AbstractValidator<GetTaskListsQuery>
    {
        public GetTaskListsQueryValidator()
        {
            RuleFor(x => x.PageNumber).GreaterThanOrEqualTo(Pagination.DefaultPageNumber);
            RuleFor(x => x.PageSize).GreaterThanOrEqualTo(Pagination.DefaultPageSize);
        }
    }
}
