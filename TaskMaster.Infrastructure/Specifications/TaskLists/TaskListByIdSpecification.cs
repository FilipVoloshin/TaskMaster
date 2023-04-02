using Ardalis.Specification;
using TaskMaster.Infrastructure.Entities;
using TaskMaster.Infrastructure.Filters;

namespace TaskMaster.Infrastructure.Specifications.TaskLists
{
    /// <summary>
    /// Specifies a TaskList by its ID and applies optional filters.
    /// </summary>
    public class TaskListByIdSpecification : Specification<TaskList>, ISingleResultSpecification
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TaskListByIdSpecification"/> class.
        /// </summary>
        /// <param name="id">The identifier of the TaskList to be retrieved.</param>
        /// <param name="filter">The TaskList filter containing additional conditions and data to include.</param>
        public TaskListByIdSpecification(Guid id, TaskListFilter filter)
        {
            Query.Where(tl => tl.Id == id);

            if (filter.IncludeAuthor)
            {
                Query.Include(tl => tl.Author);
            }

            if (filter.IncludeAssignees)
            {
                Query.Include(tl => tl.Assignees).ThenInclude(a => a.Assignee);
            }

            if (filter.AuthorId.HasValue)
            {
                Query.Where(tl => tl.AuthorId == filter.AuthorId);
            }
        }

        public TaskListByIdSpecification(Guid id)
        {
            Query.Where(tl => tl.Id == id);
        }
    }
}