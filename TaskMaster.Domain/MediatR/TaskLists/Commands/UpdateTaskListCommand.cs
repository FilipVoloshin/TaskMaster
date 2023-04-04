using FluentValidation;
using MediatR;
using TaskMaster.Application.Abstractions;

namespace TaskMaster.Application.MediatR.TaskLists.Commands
{
    /// <summary>
    /// A record representing a command to update a task list.
    /// </summary>
    public record UpdateTaskListCommand(Guid Id, string Name) : IHttpRequest;

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
