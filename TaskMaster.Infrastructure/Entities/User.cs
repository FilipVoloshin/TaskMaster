using TaskMaster.Infrastructure.Entities.Base;

namespace TaskMaster.Infrastructure.Entities
{
    /// <summary>
    /// Represents a user (assignee or owner) entity.
    /// </summary>
    internal class User : BaseEntity
    {
        /// <summary>
        /// The name of the user 
        /// </summary>
        public string Name { get; set; } = null!;

        #region Navigation properties

        public ICollection<AssignedTaskList> AssignedTaskLists { get; set; } = new List<AssignedTaskList>();
        public ICollection<AssignedTaskList> OwnedTaskLists { get; set; } = new List<AssignedTaskList>();
        public ICollection<TaskList> CreatedTaskLists { get; set; } = new List<TaskList>();

        #endregion

    }
}
