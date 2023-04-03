using Ardalis.Specification;
using TaskMaster.Infrastructure.Entities;
using TaskMaster.Infrastructure.Filters;

namespace TaskMaster.Infrastructure.Specifications.TaskLists
{
    /// <summary>
    /// Specifies a TaskList by its ID and applies optional filters.
    /// </summary>
    public class SingleTaskListSpecification : Specification<TaskList>, ISingleResultSpecification
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SingleTaskListSpecification"/> class.
        /// </summary>
        /// <param name="id">The identifier of the TaskList to be retrieved.</param>
        /// <param name="filter">The TaskList filter containing additional conditions and data to include.</param>
        public SingleTaskListSpecification(Guid id, TaskListFilter filter)
        {
            ConfigureQueryByIdentifier(id);
            ConfigureQueryByFilter(filter);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SingleTaskListSpecification"/> class.
        /// </summary>
        /// <param name="id">The identifier of the TaskList to be retrieved.</param>
        public SingleTaskListSpecification(Guid id)
        {
            ConfigureQueryByIdentifier(id);
        }

        private void ConfigureQueryByIdentifier(Guid id)
        {
            Query.Where(tl => tl.Id == id);
        }

        private void ConfigureQueryByFilter(TaskListFilter filter)
        {
            if (filter.IncludeAuthor)
            {
                Query.Include(tl => tl.Author);
            }

            if (filter.IncludeAssignees)
            {
                Query.Include(tl => tl.Assignees).ThenInclude(a => a.Assignee);
            }

            if (filter.CurrentUserId.HasValue)
            {
                Query.Where(tl => tl.AuthorId == filter.CurrentUserId || tl.Assignees.Any(a => a.AssigneeId == filter.CurrentUserId));
            }
        }
    }
}