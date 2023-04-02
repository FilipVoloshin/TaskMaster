using FluentValidation;
using TaskMaster.Application.MediatR.TaskLists.Queries;

namespace TaskMaster.Application.MediatR.TaskLists.Validations
{
    public class GetSingleTaskListQueryValidator: AbstractValidator<GetSingleTaskListQuery>
    {
        public GetSingleTaskListQueryValidator()
        {
            RuleFor(query => query.Id).NotEmpty().WithMessage("Identifier can not be empty");
        }
    }
}
