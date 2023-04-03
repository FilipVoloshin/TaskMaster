using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskMaster.Infrastructure.Contexts;
using TaskMaster.Infrastructure.Entities.Base;
using TaskMaster.Infrastructure.Repositories.Abstractions;

namespace TaskMaster.Infrastructure.Repositories
{
    /// <summary>
    /// A read-only repository class for TEntity that provides basic read operations.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity being managed by the repository.</typeparam>
    public class QueryRepository<TEntity> : BaseRepository<TEntity>, IQueryRepository<TEntity>
        where TEntity : BaseEntity
    {
        public QueryRepository(TaskMasterDbContext dbContext,
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
            return await ApplySpecification(specification).ToArrayAsync(cancellationToken);
        }

        public async Task<List<TEntity>> GetListAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
        {
            return await ApplySpecification(specification).ToListAsync(cancellationToken);
        }
    }
}
