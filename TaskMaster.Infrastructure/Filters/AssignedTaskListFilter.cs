
namespace TaskMaster.Infrastructure.Filters
{
    /// <summary>
    /// Represents a filter for querying assigned task lists, including optional filters and data to include.
    /// </summary>
    public record AssignedTaskListFilter(Guid? CurrentUserId = null)
    {
        /// <summary>
        /// Gets or sets a value indicating whether to include the assignee in the results.
        /// </summary>
        public bool IncludeAssignee { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to include the author in the results.
        /// </summary>
        public bool IncludeAuthor { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to include the task list in the results.
        /// </summary>
        public bool IncludeTaskList { get; set; }

        /// <summary>
        /// Gets or sets the ID of the assignee to filter by.
        /// </summary>
        public Guid? AssigneeId { get; set; }

        /// <summary>
        /// Gets or sets the ID of the task list to filter by.
        /// </summary>
        public Guid? TaskListId { get; set; }

        /// <summary>
        /// Gets or sets the pagination settings for the query results.
        /// </summary>
        public PaginationFilter? Pagination { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to apply 'no tracking' to the query.
        /// </summary>
        public bool AsNoTracking { get; init; }

    }
}
