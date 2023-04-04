namespace TaskMaster.Infrastructure.Filters
{

    /// <summary>
    /// Represents the pagination filter for query results.
    /// </summary>
    public record PaginationFilter(int PageNumber, int PageSize);
}
