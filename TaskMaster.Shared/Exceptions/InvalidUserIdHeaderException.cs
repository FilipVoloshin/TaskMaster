using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMaster.Shared.Exceptions
{
    /// <summary>
    /// Represents an error that occurs when the provided User Id header is not in a valid format.
    /// </summary>
    public class InvalidUserIdHeaderException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidUserIdHeaderException"/> class
        /// with a default error message.
        /// </summary>
        public InvalidUserIdHeaderException()
            : base("The provided TM-User-Id header is not in a valid format.") { }


        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidUserIdHeaderException"/> class
        /// with a specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public InvalidUserIdHeaderException(string message)
            : base(message) { }
    }
}
