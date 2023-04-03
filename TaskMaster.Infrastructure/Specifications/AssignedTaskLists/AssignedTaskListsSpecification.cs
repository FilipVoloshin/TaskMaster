using Ardalis.Specification;
using TaskMaster.Infrastructure.Entities;
using TaskMaster.Infrastructure.Filters;

namespace TaskMaster.Infrastructure.Specifications.AssignedTaskLists
{
    public class AssignedTaskListsSpecification : Specification<AssignedTaskList>
    {
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
