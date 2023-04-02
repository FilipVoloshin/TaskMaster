namespace TaskMaster.Infrastructure.Filters
{
    public record TaskListFilter(Guid? CurrentUserId = null)
    {
        public bool IncludeAssignees { get; init; }
        public bool IncludeAuthor { get; init; }
    }
}
