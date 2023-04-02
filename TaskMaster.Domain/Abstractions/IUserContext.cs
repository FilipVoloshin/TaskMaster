namespace TaskMaster.Application.Abstractions
{
    /// <summary>
    /// Defines the contract for managing user-related operations.
    /// </summary>
    public interface IUserContext
    {
        /// <summary>
        /// Gets the unique identifier of the current user.
        /// </summary>
        Guid UserId { get; }

        /// <summary>
        /// Sets the unique identifier for the current user.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        void SetUserId(Guid userId);
    }
}
