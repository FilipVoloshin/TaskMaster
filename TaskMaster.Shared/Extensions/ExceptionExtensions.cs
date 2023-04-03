using TaskMaster.Shared.Exceptions;

namespace TaskMaster.Shared.Extensions
{
    /// <summary>
    /// Contains extension methods for throwing not found exceptions for null or empty results.
    /// </summary>
    public static class ExceptionExtensions
    {
        /// <summary>
        /// Throws an exception of the specified type if the result of the task is null.
        /// </summary>
        /// <typeparam name="TResult">The type of the result of the task.</typeparam>
        /// <typeparam name="TException">The type of the exception to throw.</typeparam>
        /// <param name="task">The task to check for null result.</param>
        /// <returns>The result of the task.</returns>
        /// <exception cref="TException">Thrown when the result of the task is null.</exception>
        public static async Task<TResult> ThrowIfNullAsync<TResult, TException>(this Task<TResult> task)
            where TResult : class?
            where TException : Exception, new()
        {
            var result = await task;

            if (result is null)
            {
                throw Activator.CreateInstance<TException>();
            }

            return result;
        }


        /// <summary>
        /// Throws an exception of the specified type if the result of the task is null or empty.
        /// </summary>
        /// <typeparam name="TResult">The type of the elements in the collection.</typeparam>
        /// <typeparam name="TException">The type of the exception to throw.</typeparam>
        /// <param name="task">The task to check for null or empty result.</param>
        /// <returns>The result of the task.</returns>
        /// <exception cref="TException">Thrown when the result of the task is null or empty.</exception>
        public static async Task<IEnumerable<TResult>> ThrowIfNullOrEmptyAsync<TResult, TException>(this Task<IEnumerable<TResult>> task)
            where TException : Exception, new()
        {
            var result = await task;

            if (result.IsNullOrEmpty())
            {
                throw Activator.CreateInstance<TException>();
            }

            return result;
        }

        /// <summary>
        /// Throws an exception of the specified type if the result of the task is null or empty.
        /// </summary>
        /// <typeparam name="TResult">The type of the elements in the collection.</typeparam>
        /// <typeparam name="TException">The type of the exception to throw.</typeparam>
        /// <param name="task">The task to check for null or empty result.</param>
        /// <returns>The result of the task.</returns>
        /// <exception cref="TException">Thrown when the result of the task is null or empty.</exception>
        public static async Task<List<TResult>> ThrowIfNullOrEmptyAsync<TResult, TException>(this Task<List<TResult>> task)
            where TException : Exception, new()
        {
            var result = await task;

            if (result == null || result.Count == 0)
            {
                throw Activator.CreateInstance<TException>();
            }

            return result;
        }

        /// <summary>
        /// Throws an instance of the specified exception if the task's result is not null.
        /// </summary>
        /// <typeparam name="TResult">The type of the task's result.</typeparam>
        /// <typeparam name="TException">The type of the exception to be thrown.</typeparam>
        /// <param name="task">The task to be checked.</param>
        /// <returns>The task's result.</returns>
        /// <exception cref="TException">Thrown if the task's result is not null.</exception>
        public static async Task<TResult> ThrowIfNotNullAsync<TResult, TException>(this Task<TResult> task)
             where TResult : class?
             where TException : Exception, new()
        {
            var result = await task;

            if (result is not null)
            {
                throw Activator.CreateInstance<TException>();
            }

            return result;
        }
    }
}
