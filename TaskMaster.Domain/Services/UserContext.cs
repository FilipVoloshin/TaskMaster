using TaskMaster.Domain.Abstractions;

namespace TaskMaster.Domain.Services
{
    public class UserContext : IUserContext
    {
        public Guid UserId { get; private set; }

        public void SetUserId(Guid userId)
        {
            UserId = userId;
        }
    }
}
