using FluentValidation;
using MediatR;
using TaskMaster.Application.Abstractions;

namespace TaskMaster.Application.MediatR.AssignedTaskLists.Command
{
    /// <summary>
    /// Represents a command to create an assigned task list for a specific assignee and task list.
    /// </summary>
    public record CreateAssignedTaskListCommand(Guid AssigneeId, Guid TaskListId) : IHttpRequest;

    public class CreateAssignedTaskListCommandValidator : AbstractValidator<CreateAssignedTaskListCommand>
    {
        public CreateAssignedTaskListCommandValidator()
        {
            RuleFor(x => x.AssigneeId).Must(id => id != Guid.Empty).WithMessage("Value of assignee identifier can't be an empty GUID");
            RuleFor(x => x.TaskListId).Must(id => id != Guid.Empty).WithMessage("Value of task list identifier can't be an empty GUID");
        }
    }
}
