namespace TaskMaster.Shared.Exceptions
{
    /// <summary>
    /// Exception thrown when the current user does not have access to work with an entity.
    /// </summary>
    public class NotOwnedByYouException : Exception
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="NotOwnedByYouException"/> class with a default error message.
        /// </summary>       
        public NotOwnedByYouException()
            : base("You do not have access to work with this entity")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotOwnedByYouException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public NotOwnedByYouException(string message)
            : base(message)
        {
        }
    }
}
