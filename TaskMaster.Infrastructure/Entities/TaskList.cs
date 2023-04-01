using TaskMaster.Infrastructure.Entities.Abstractions;
using TaskMaster.Infrastructure.Entities.Base;

namespace TaskMaster.Infrastructure.Entities
{
    /// <summary>
    /// Represents a task list entity.
    /// </summary>
    public class TaskList : BaseEntity, IAuthorable
    {
        /// <summary>
        /// Name of the task list.
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// Identifier of <see cref="User"/> who created this task list
        /// </summary>
        public Guid AuthorId { get; set; }

        /// <summary>
        /// Created date and time of the task list in UTC
        /// </summary>
        public DateTimeOffset CreatedAtUtc { get; set; }

        #region Navigation properties

        public User Author { get; set; } = null!;
        public ICollection<AssignedTaskList> Assignees { get; set; } = new List<AssignedTaskList>();

        #endregion
    }
}
