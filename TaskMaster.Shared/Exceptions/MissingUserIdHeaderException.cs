namespace TaskMaster.Shared.Exceptions
{

    /// <summary>
    /// Represents an error that occurs when the User Id header is missing from the request.
    /// </summary>
    public class MissingUserIdHeaderException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MissingUserIdHeaderException"/> class
        /// with a default error message.
        /// </summary>
        public MissingUserIdHeaderException()
            : base("The TM-User-Id header is missing from the request.") { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MissingUserIdHeaderException"/> class
        /// with a specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public MissingUserIdHeaderException(string message)
            : base(message) { }
    }
}
