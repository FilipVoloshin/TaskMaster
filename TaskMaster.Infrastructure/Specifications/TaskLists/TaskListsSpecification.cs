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
        /// <summary>
        /// Initializes a new instance of the <see cref="TaskListsSpecification"/> class.
        /// </summary>
        /// <param name="filter">The TaskList filter containing additional conditions and data to include.</param>
        public TaskListsSpecification(TaskListFilter filter)
        {
            if (filter.IncludeAuthor)
            {
                Query.Include(query => query.Author);
            }

            if (filter.IncludeAssignees)
            {
                Query.Include(query => query.Assignees).ThenInclude(a => a.Assignee);
            }

            Query.Where(query => query.AuthorId == filter.CurrentUserId || query.Assignees.Any(a => a.AssigneeId == filter.CurrentUserId));

            if (filter.AsNoTracking)
            {
                Query.AsNoTracking();
            }

            if (filter.Pagination is not null)
            {
                Query.Skip((filter.Pagination.PageNumber - 1) * filter.Pagination.PageSize)
                    .Take(filter.Pagination.PageSize);
            }

            Query.OrderByDescending(a => a.CreatedAtUtc);
        }
    }
}