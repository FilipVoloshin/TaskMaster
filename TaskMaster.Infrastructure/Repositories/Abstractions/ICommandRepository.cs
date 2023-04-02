using TaskMaster.Infrastructure.Entities.Base;

namespace TaskMaster.Infrastructure.Repositories.Abstractions
{
    /// <summary>
    /// Provides interface for repositories that perform write operations (Add, Update, Remove) for a given entity type.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity managed by the repository.</typeparam>
    public interface ICommandRepository<TEntity> : IRepository
        where TEntity : BaseEntity
    {
        /// <summary>
        /// Asynchronously adds a new entity to the repository.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
        Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates an existing entity with the provided updated entity values.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <param name="updatedEntity">The updated values to apply to the entity.</param>
        void Update(TEntity entity, TEntity updatedEntity);

        /// <summary>
        /// Removes an existing entity from the repository.
        /// </summary>
        /// <param name="entity">The entity to remove.</param>
        void Remove(TEntity entity);

        /// <summary>
        /// Attaches an existing entity to the repository.
        /// </summary>
        /// <param name="entity">The entity to attach.</param>
        void Attach(TEntity entity);
    }
}