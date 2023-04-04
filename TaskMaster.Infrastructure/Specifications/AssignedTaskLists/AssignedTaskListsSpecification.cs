using Ardalis.Specification;
using TaskMaster.Infrastructure.Entities;
using TaskMaster.Infrastructure.Filters;

namespace TaskMaster.Infrastructure.Specifications.AssignedTaskLists
{
    /// <summary>
    /// A specification for retrieving a list of assigned tasks based on the specified filters, including optional data for author, task list, and assignee, as well as pagination and tracking options.
    /// </summary>
    public class AssignedTaskListsSpecification : Specification<AssignedTaskList>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AssignedTaskListsSpecification"/> class.
        /// </summary>
        /// <param name="filter">The filter containing the conditions and data to include.</param>
        public AssignedTaskListsSpecification(AssignedTaskListFilter filter)
        {
            if (filter.IncludeAuthor)
            {
                Query.Include(query => query.Author);
            }

            if (filter.IncludeTaskList)
            {
                Query.Include(query => query.TaskList)
                    .ThenInclude(query => query.Assignees);
            }

            if (filter.IncludeAssignee)
            {
                Query.Include(query => query.Assignee).ThenInclude(query => query.AssignedTaskLists);
            }

            if (filter.CurrentUserId.HasValue)
            {
                Query.Where(query => query.AuthorId == filter.CurrentUserId || query.Assignee.AssignedTaskLists.Any(a => a.AssigneeId == filter.CurrentUserId));
            }

            if (filter.AsNoTracking)
            {
                Query.AsNoTracking();
            }

            if (filter.TaskListId.HasValue)
            {
                Query.Where(query => query.TaskListId == filter.TaskListId);
            }

            if (filter.AssigneeId.HasValue)
            {
                Query.Where(query => query.AssigneeId == filter.AssigneeId);
            }

            if (filter.Pagination is not null)
            {
                Query.Skip((filter.Pagination.PageNumber - 1) * filter.Pagination.PageSize)
                    .Take(filter.Pagination.PageSize);
            }
        }
    }
}
