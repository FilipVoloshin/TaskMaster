namespace TaskMaster.Application.ViewModels
{
    /// <summary>
    /// Represents a view model for a TaskList with its assigned users.
    /// </summary>
    public record TaskListAssigneeVm(string TaskListName, IEnumerable<AssigneeVm> Assignees);
}
