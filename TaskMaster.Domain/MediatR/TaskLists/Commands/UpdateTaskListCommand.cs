using FluentValidation;
using MediatR;

namespace TaskMaster.Application.MediatR.TaskLists.Commands
{
    /// <summary>
    /// A record representing a command to update a task list.
    /// </summary>
    public record UpdateTaskListCommand(Guid Id, string Name) : IRequest<Unit>;

    public class UpdateTaskListCommandValidator : AbstractValidator<UpdateTaskListCommand>
    {
        public UpdateTaskListCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(255);
        }
    }
}
