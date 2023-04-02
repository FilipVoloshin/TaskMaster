namespace TaskMaster.Infrastructure.Filters
{
    public record TaskListFilter(Guid? AuthorId = null)
    {
        public bool IncludeAssignees { get; init; }
        public bool IncludeAuthor { get; init; }
    }
}
