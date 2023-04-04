using FluentValidation;
using MediatR;
using TaskMaster.Application.Abstractions;
using TaskMaster.Application.MediatR.Base;
using TaskMaster.Application.ViewModels;
using static TaskMaster.Shared.Constants;

namespace TaskMaster.Application.MediatR.TaskLists.Queries
{
    /// <summary>
    /// A query to get a paginated list of <see cref="TaskListVm"/> items.
    /// </summary>
    public record GetTaskListsQuery : PaginationRequest, IHttpRequest;

    public class GetTaskListsQueryValidator : AbstractValidator<GetTaskListsQuery>
    {
        public GetTaskListsQueryValidator()
        {
            RuleFor(x => x.PageNumber).GreaterThanOrEqualTo(Pagination.DefaultPageNumber);
            RuleFor(x => x.PageSize).GreaterThanOrEqualTo(Pagination.DefaultPageSize);
        }
    }
}
