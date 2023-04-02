using TaskMaster.Infrastructure.Repositories.Abstractions;

namespace TaskMaster.Infrastructure.UnitsOfWork
{
    /// <summary>
    /// Provides a mechanism for encapsulating the transactions and coordinating the changes made to one or more repositories.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Gets the repository of the specified type.
        /// </summary>
        /// <typeparam name="TRepository">The type of the repository to get.</typeparam>
        /// <returns>An instance of the specified repository type.</returns>
        TRepository GetRepository<TRepository>() where TRepository : IRepository;

        /// <summary>
        /// Saves all changes made in this unit of work to the underlying data store.
        /// </summary>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task representing the asynchronous save operation.</returns>
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
