using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using TaskMaster.Infrastructure.Contexts;
using TaskMaster.Infrastructure.Entities.Base;
using TaskMaster.Infrastructure.Repositories.Abstractions;

namespace TaskMaster.Infrastructure.Repositories
{
    /// <summary>
    /// Represents a repository for write-only operations for entities of type TEntity.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity for the repository.</typeparam>
    public class CommandRepository<TEntity> : BaseRepository<TEntity>, ICommandRepository<TEntity>
        where TEntity : BaseEntity
    {
        /// Initializes a new instance of the <see cref="CommandRepository{TEntity}"/> class.
        /// </summary>
        /// <param name="context">The <see cref="CommandTaskMasterDbContext"/> to be used by the repository.</param>
        /// <param name="evaluator">The <see cref="ISpecificationEvaluator"/> to be used by the repository.</param>
        public CommandRepository(CommandTaskMasterDbContext context,
            ISpecificationEvaluator evaluator)
            : base(context, evaluator)
        {

        }

        public async Task<Guid> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            if (entity.Id == Guid.Empty)
            {
                entity.Id = Guid.NewGuid();
            }

            await DbSet.AddAsync(entity, cancellationToken);

            return entity.Id;
        }

        public void Attach(TEntity entity)
        {
            DbSet.Attach(entity);
        }

        public void Remove(TEntity entity)
        {
            DbSet.Remove(entity);
        }

        public void Update(TEntity entity, TEntity updatedEntity)
        {
            Context.Entry(entity).CurrentValues.SetValues(updatedEntity);
            Context.Entry(entity).State = EntityState.Modified;
        }
    }
}
