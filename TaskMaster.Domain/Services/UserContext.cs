using TaskMaster.Domain.Abstractions;

namespace TaskMaster.Domain.Services
{
    /// <summary>
    /// Provides an implementation of IUserContext for storing and accessing user-related information.
    /// </summary>
    public class UserContext : IUserContext
    {
        public Guid UserId { get; private set; }

        public void SetUserId(Guid userId)
        {
            UserId = userId;
        }
    }
}
