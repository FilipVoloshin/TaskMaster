namespace TaskMaster.Application.ViewModels
{
    public record TaskListVm(Guid Id, string Name, DateTimeOffset CreatedAtUtc);
}
