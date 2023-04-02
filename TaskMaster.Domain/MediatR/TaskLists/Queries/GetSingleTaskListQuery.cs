using FluentValidation;
using MediatR;
using TaskMaster.Application.ViewModels;

namespace TaskMaster.Application.MediatR.TaskLists.Queries
{
    public record GetSingleTaskListQuery(Guid Id): IRequest<TaskListVm> { };

    public class GetSingleTaskListQueryValidator : AbstractValidator<GetSingleTaskListQuery>
    {
        public GetSingleTaskListQueryValidator()
        {
            RuleFor(query => query.Id).NotEmpty().WithMessage("Identifier can not be empty");
        }
    }
}
