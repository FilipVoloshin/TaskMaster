using TaskMaster.Infrastructure.Entities.Base;
namespace TaskMaster.Infrastructure.Entities
{
    /// <summary>
    /// Represents the mapping between a <see cref="Entities.User"/> and a <see cref="Entities.TaskList"/>
    /// </summary>
    public class AssignedTaskList : BaseEntity
    {
        /// <summary>
        /// Unique identifier of <see cref="Entities.User"/>
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Unique idenitifier of <see cref="Entities.TaskList"/>
        /// </summary>
        public Guid TaskListId { get; set; }

        /// <summary>
        /// Unique identifier of <see cref="Entities.User"/>
        /// </summary>
        public Guid AuthorId { get; set; }


        #region Navigation properties

        public User User { get; set; } = null!;

        public User Author { get; set; } = null!;

        public TaskList TaskList { get; set; } = null!;

        #endregion

    }
}
