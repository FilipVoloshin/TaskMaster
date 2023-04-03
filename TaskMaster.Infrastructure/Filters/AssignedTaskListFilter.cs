
namespace TaskMaster.Infrastructure.Filters
{
    public record AssignedTaskListFilter(Guid? CurrentUserId = null)
    {
        public bool IncludeAssignee { get; set; }
        public bool IncludeAuthor { get; set; }
        public bool IncludeTaskList { get; set; }

        public Guid? AssigneeId { get; set; }
        public Guid? TaskListId { get; set; }

        public PaginationFilter? Pagination { get; set; }
        public bool AsNoTracking { get; init; }

    }
}
