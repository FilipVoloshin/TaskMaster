namespace TaskMaster.Application.ViewModels
{
    //public record AssigneeVm(Guid UserId, string UserName);
    public class AssigneeVm
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; } = null!;
    }
}
