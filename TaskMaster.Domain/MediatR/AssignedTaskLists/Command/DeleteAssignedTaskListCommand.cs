using FluentValidation;
using MediatR;

namespace TaskMaster.Application.MediatR.AssignedTaskLists.Command
{
    public class DeleteAssignedTaskListCommand : IRequest<Unit>
    {
        public Guid TaskListId { get; set; }
        public Guid AssigneeId { get; set; }
    }

    public class DeleteAssignedTaskListCommandValidator : AbstractValidator<DeleteAssignedTaskListCommand>
    {
        public DeleteAssignedTaskListCommandValidator()
        {
            RuleFor(x => x.AssigneeId).Must(id => id != Guid.Empty).WithMessage("Value of assignee identifier can't be an empty GUID");
            RuleFor(x => x.TaskListId).Must(id => id != Guid.Empty).WithMessage("Value of task list identifier can't be an empty GUID");
        }
    }
}
