namespace TaskMaster.Shared.Exceptions
{
    /// <summary>
    /// Exception thrown when an operation could not be completed due to a conflict with the existing state of the resource.
    /// </summary>
    public class ResourceConflictException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceConflictException"/> class with a default message.
        /// </summary>
        public ResourceConflictException()
            : base("Operation could not be completed due to a conflict with the existing state of the resource")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceConflictException"/> class with the specified message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ResourceConflictException(string message)
        : base(message)
        {
        }
    }
}
