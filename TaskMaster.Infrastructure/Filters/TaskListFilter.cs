namespace TaskMaster.Infrastructure.Filters
{
    /// <summary>
    /// Filter for querying task lists, specifying optional conditions and data to include in the query.
    /// </summary>
    public record TaskListFilter(Guid? CurrentUserId = null)
    {
        /// <summary>
        /// Specifies whether to include assignees for each task list in the result.
        /// </summary>
        public bool IncludeAssignees { get; init; }

        /// <summary>
        /// Specifies whether to include the author for each task list in the result.
        /// </summary>
        public bool IncludeAuthor { get; init; }

        /// <summary>
        /// Specifies whether to track changes to the entities returned by the query.
        /// </summary>
        public bool AsNoTracking { get; init; }

        /// <summary>
        /// The pagination options for the query.
        /// </summary>
        public PaginationFilter? Pagination { get; init; }
    }
}
