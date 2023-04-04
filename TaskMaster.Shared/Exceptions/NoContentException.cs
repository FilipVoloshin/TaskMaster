namespace TaskMaster.Shared.Exceptions
{
    /// <summary>
    /// The exception that is thrown when a requested resource ha no content.
    /// </summary>
    public class NoContentException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NoContentException"/> class.
        /// </summary>
        public NoContentException()
            :base("The requested resource has no content to show")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NoContentException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public NoContentException(string message)
            : base(message)
        {
        }
    }
}
