namespace TaskMaster.Application.ViewModels
{
    public record TaskListAssigneeVm(string TaskListName, IEnumerable<AssigneeVm> Assignees);
}
