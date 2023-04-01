using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskMaster.Infrastructure.Contexts;
using TaskMaster.Infrastructure.Entities.Base;
using TaskMaster.Infrastructure.Repositories.Abstracts;

namespace TaskMaster.Infrastructure.Repositories
{
    /// <summary>
    /// A read-only repository class for TEntity that provides basic read operations.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity being managed by the repository.</typeparam>
    public class ReadOnlyRepository<TEntity> : BaseRepository<TEntity>, IReadOnlyRepository<TEntity> 
        where TEntity : BaseEntity
    {
        public ReadOnlyRepository(ReadonlyTaskMasterDbContext dbContext,
            ISpecificationEvaluator specificationEvaluator)
            : base(dbContext, specificationEvaluator) { }

        public Task<bool> AnyAsync<TSpec>(TSpec specification, CancellationToken cancellationToken = default)
            where TSpec : ISpecification<TEntity> =>
            ApplySpecification(specification).AnyAsync(cancellationToken);


        public Task<int> CountAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default) =>
            ApplySpecification(specification).CountAsync(cancellationToken);

        public Task<TEntity?> FirstOrDefaultAsync<TSpec>(TSpec specification, CancellationToken cancellationToken = default)
            where TSpec : ISpecification<TEntity>, ISingleResultSpecification =>
            ApplySpecification(specification).FirstOrDefaultAsync(cancellationToken);

        public Task<TResult?> FirstOrDefaultAsync<TResult>(ISpecification<TEntity, TResult?> specification, CancellationToken cancellationToken = default) =>
            ApplySpecification(specification).FirstOrDefaultAsync(cancellationToken);

        public async Task<IEnumerable<TEntity>> GetAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
        {
            var queryResult = await ApplySpecification(specification).ToArrayAsync(cancellationToken);

            return specification.PostProcessingAction is not null
                ? specification.PostProcessingAction(queryResult)
                : queryResult;
        }

        public async Task<IEnumerable<TResult?>> GetAsync<TResult>(ISpecification<TEntity, TResult?> specification, CancellationToken cancellationToken = default)
        {
            var queryResult = await ApplySpecification(specification).ToListAsync(cancellationToken);

            return specification.PostProcessingAction is not null
                ? specification.PostProcessingAction(queryResult)
                : queryResult;
        }
    }
}
