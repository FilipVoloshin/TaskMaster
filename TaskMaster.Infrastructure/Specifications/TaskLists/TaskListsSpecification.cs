using Ardalis.Specification;
using TaskMaster.Infrastructure.Entities;
using TaskMaster.Infrastructure.Filters;

namespace TaskMaster.Infrastructure.Specifications.TaskLists
{
    /// <summary>
    /// Specifies a set of TaskLists based on the provided filter.
    /// </summary>
    public class TaskListsSpecification : Specification<TaskList>
    {
        // <summary>
        /// Initializes a new instance of the <see cref="TaskListsSpecification"/> class.
        /// </summary>
        /// <param name="filter">The TaskList filter containing additional conditions and data to include.</param>
        public TaskListsSpecification(TaskListFilter filter)
        {
            if (filter.IncludeAuthor)
            {
                Query.Include(tl => tl.Author);
            }

            if (filter.IncludeAssignees)
            {
                Query.Include(tl => tl.Assignees).ThenInclude(a => a.Assignee);
            }

            Query.Where(query => query.AuthorId == filter.CurrentUserId || query.Assignees.Any(a => a.AssigneeId == filter.CurrentUserId));
        }
    }
}