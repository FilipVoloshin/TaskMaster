namespace TaskMaster.Application.ViewModels
{
    /// <summary>
    /// View model representing an assignee user with their ID and name.
    /// </summary>
    public class AssigneeVm
    {
        /// <summary>
        /// The ID of the assignee user.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// The name of the assignee user.
        /// </summary>
        public string UserName { get; set; } = null!;
    }
}
