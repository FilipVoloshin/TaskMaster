using FluentValidation;
using MediatR;

namespace TaskMaster.Application.MediatR.AssignedTaskLists.Command
{
    /// <summary>
    /// Represents a command to delete an assigned task list.
    /// </summary>
    public record DeleteAssignedTaskListCommand(Guid TaskListId, Guid AssigneeId) : IRequest<Unit>;
    public class DeleteAssignedTaskListCommandValidator : AbstractValidator<DeleteAssignedTaskListCommand>
    {
        public DeleteAssignedTaskListCommandValidator()
        {
            RuleFor(x => x.AssigneeId).Must(id => id != Guid.Empty).WithMessage("Value of assignee identifier can't be an empty GUID");
            RuleFor(x => x.TaskListId).Must(id => id != Guid.Empty).WithMessage("Value of task list identifier can't be an empty GUID");
        }
    }
}
