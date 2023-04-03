using Ardalis.Specification;
using TaskMaster.Infrastructure.Entities.Base;

namespace TaskMaster.Infrastructure.Repositories.Abstractions
{
    /// <summary>
    /// Defines a repository for performing queries on entities of type TEntity.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity that this repository manages.</typeparam>
    public interface IQueryRepository<TEntity> : IRepository
        where TEntity : BaseEntity
    {
        /// <summary>
        /// Determines whether any entities match the given specification.
        /// </summary>
        /// <typeparam name="TSpec">The type of the specification, which must implement ISpecification contract.</typeparam>
        /// <param name="specification">The specification to use for filtering entities.</param>
        /// <param name="cancellationToken">A CancellationToken to observe while waiting for the task to complete</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean value that is true if any entities match the specification, otherwise false.</returns>
        Task<bool> AnyAsync<TSpec>(TSpec specification, CancellationToken cancellationToken = default) where TSpec : ISpecification<TEntity>;

        /// <summary>
        /// Gets the number of entities that match the given specification.
        /// </summary>
        /// <param name="specification">The specification to use for filtering entities.</param>
        /// <param name="cancellationToken">A CancellationToken to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the number of entities that match the specification.</returns>
        Task<int> CountAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves the first entity that matches the given specification.
        /// If no entity matches the specification, returns null.
        /// </summary>
        /// <typeparam name="TSpec">The type of the specification, which must implement ISpecification contract and ISingleResultSpecification.</typeparam>
        /// <param name="specification">The specification to use for filtering and fetching the entity.</param>
        /// <param name="cancellationToken">A CancellationToken to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the entity, or null if no entity matches the specification.</returns>
        Task<TEntity?> FirstOrDefaultAsync<TSpec>(TSpec specification, CancellationToken cancellationToken = default)
            where TSpec : ISpecification<TEntity>, ISingleResultSpecification;

        /// <summary>
        /// Retrieves the first entity that matches the given specification and projects it to the specified TResult type.
        /// If no entity matches the specification, returns null.
        /// </summary>
        /// <typeparam name="TResult">The type to project the result to.</typeparam>
        /// <param name="specification">The specification to use for filtering and fetching the entity.</param>
        /// <param name="cancellationToken">A CancellationToken to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the projected entity, or null if no entity matches the specification.</returns>
        Task<TResult?> FirstOrDefaultAsync<TResult>(ISpecification<TEntity, TResult?> specification, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves a collection of TEntity objects based on the provided specification.
        /// </summary>
        /// <param name="specification">The specification to apply to the query.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A task representing an asynchronous operation, with the result being an enumerable of TEntity objects.</returns>
        Task<IEnumerable<TEntity>> GetAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves a List of TEntity objects based on the provided specification.
        /// </summary>
        /// <param name="specification">The specification to apply to the query.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A task representing an asynchronous operation, with the result being a list of TEntity objects.</returns>
        Task<List<TEntity>> GetListAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);
    }
}