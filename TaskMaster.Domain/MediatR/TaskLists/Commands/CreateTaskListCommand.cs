using FluentValidation;
using MediatR;

namespace TaskMaster.Application.MediatR.TaskLists.Commands
{
    public record CreateTaskListCommand(string Name) : IRequest<Guid>;

    public class CreateTaskListCommandValidator : AbstractValidator<CreateTaskListCommand>
    {
        public CreateTaskListCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(255);
        }
    }
}
