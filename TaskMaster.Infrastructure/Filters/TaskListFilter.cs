using TaskMaster.Infrastructure.Filters.Abstractions;

namespace TaskMaster.Infrastructure.Filters
{
    public record TaskListFilter(Guid AuthorId) : IAuthorableFilter
    {
        public bool IncludeAssignees { get; init; }
        public bool IncludeAuthor { get; init; }
    }
}
