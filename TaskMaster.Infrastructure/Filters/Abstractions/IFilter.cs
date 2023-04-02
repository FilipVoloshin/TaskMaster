namespace TaskMaster.Infrastructure.Filters.Abstractions
{
    public interface IFilter
    {
    }

    public interface IAuthorableFilter : IFilter
    {
        Guid AuthorId { get; init; }
    }
}
