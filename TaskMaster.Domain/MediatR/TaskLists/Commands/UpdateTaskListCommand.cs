using FluentValidation;
using MediatR;

namespace TaskMaster.Application.MediatR.TaskLists.Commands
{
    public record UpdateTaskListCommand(string Name) : IRequest<Unit>
    {
        public Guid? Id { get; set; } = null;
    }

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
