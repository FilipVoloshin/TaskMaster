
namespace TaskMaster.Infrastructure.Filters
{
    public record AssignedTaskListFilter(Guid AuthorId)
    {
        public bool IncludeAssignee { get; set; }
        public bool IncludeAuthor { get; set; }
        public bool IncludeTaskList { get; set; }
    }
}
