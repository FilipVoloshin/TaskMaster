using Ardalis.Specification;
using TaskMaster.Infrastructure.Entities.Base;

namespace TaskMaster.Infrastructure.Repositories.Abstractions
{

    /// <summary>
    /// Defines a projection repository interface that provides methods for fetching TEntity instances with
    /// specified projections based on the given specifications.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity being managed by the repository.</typeparam>
    public interface IProjectionQueryRepository<TEntity> : IRepository
        where TEntity : BaseEntity
    {
        /// <summary>
        /// Retrieves the first entity that matches the given specification and projects it to the specified type.
        /// If no entity matches the specification, returns null.
        /// </summary>
        /// <typeparam name="TSpec">The type of the specification, which must implement ISpecification<TEntity> and ISingleResultSpecification.</typeparam>
        /// <typeparam name="TProjection">The type to project the result to.</typeparam>
        /// <param name="specification">The specification to use for filtering and fetching the entity.</param>
        /// <param name="cancellationToken">A CancellationToken to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the projected entity, or null if no entity matches the specification.</returns>
        Task<TProjection?> FirstOrDefaultAsync<TSpec, TProjection>(TSpec specification, CancellationToken cancellationToken = default)
            where TSpec : ISpecification<TEntity>, ISingleResultSpecification;

        /// <summary>
        /// Fetches all entities matching the given specification and projects them to the specified type.
        /// </summary>
        /// <typeparam name="TProjection">The type of the projection to be returned.</typeparam>
        /// <param name="specification">The specification to apply.</param>
        /// <param name="cancellationToken">Optional. The cancellation token to cancel the operation.</param>
        /// <returns>A Task representing the asynchronous operation, with an IEnumerable of TProjection as the result.</returns>
        Task<IEnumerable<TProjection>> GetAsync<TProjection>(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);
    }
}
