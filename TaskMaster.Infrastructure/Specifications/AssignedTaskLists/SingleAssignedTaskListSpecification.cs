using Ardalis.Specification;
using TaskMaster.Infrastructure.Entities;
using TaskMaster.Infrastructure.Filters;

namespace TaskMaster.Infrastructure.Specifications.AssignedTaskLists
{
    /// <summary>
    /// Specifies a AssignedTaskList by its assignee identifier and task list identifier and applies optional filters.
    /// </summary>
    public class SingleAssignedTaskListSpecification : Specification<AssignedTaskList>, ISingleResultSpecification
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SingleAssignedTaskListSpecification"/> class with an identifier and filter.
        /// </summary>
        /// <param name="id">The identifier of the <see cref="AssignedTaskList"/> to be retrieved.</param>
        /// <param name="filter">The AssignedTaskList filter containing additional conditions and data to include.</param>
        public SingleAssignedTaskListSpecification(Guid id, AssignedTaskListFilter filter)
        {
            ConfigureQueryByFilter(filter);
            ConfigureQueryByIdentifier(id);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SingleAssignedTaskListSpecification"/> class with an identifier.
        /// </summary>
        /// <param name="id">The identifier of the <see cref="AssignedTaskList"/> to be retrieved.</param>
        public SingleAssignedTaskListSpecification(Guid id)
        {
            ConfigureQueryByIdentifier(id);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SingleAssignedTaskListSpecification"/> class with a filter.
        /// </summary>
        /// <param name="filter">The AssignedTaskList filter containing additional conditions and data to include.</param>
        public SingleAssignedTaskListSpecification(AssignedTaskListFilter filter)
        {
            ConfigureQueryByFilter(filter);
        }

        private void ConfigureQueryByIdentifier(Guid id)
        {
            Query.Where(atl => atl.Id == id);
        }

        private void ConfigureQueryByFilter(AssignedTaskListFilter filter)
        {
            if (filter.IncludeAssignee)
            {
                Query.Include(atl => atl.Assignee);
            }

            if (filter.IncludeTaskList)
            {
                Query.Include(atl => atl.TaskList);

            }

            if (filter.IncludeAuthor)
            {
                Query.Include(atl => atl.Author);
            }

            if (filter.CurrentUserId.HasValue)
            {
                Query.Where(atl => atl.AssigneeId == filter.CurrentUserId ||
                                   atl.AuthorId == filter.CurrentUserId);
            }

            if (filter.AssigneeId.HasValue)
            {
                Query.Where(atl => atl.AssigneeId == filter.AssigneeId.Value);
            }

            if (filter.TaskListId.HasValue)
            {
                Query.Where(atl => atl.TaskListId == filter.TaskListId.Value);
            }
        }
    }
}
