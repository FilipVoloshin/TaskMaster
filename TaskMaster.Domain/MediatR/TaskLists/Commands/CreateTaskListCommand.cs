using FluentValidation;
using MediatR;

namespace TaskMaster.Application.MediatR.TaskLists.Commands
{
    /// <summary>
    /// A command to create a new task list with the specified name.
    /// </summary>
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
