using FluentValidation;
using MediatR;
using TaskMaster.Application.Abstractions;

namespace TaskMaster.Application.MediatR.TaskLists.Commands
{
    /// <summary>
    /// A command to create a new task list with the specified name.
    /// </summary>
    public record CreateTaskListCommand(string Name) : IHttpRequest;

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
