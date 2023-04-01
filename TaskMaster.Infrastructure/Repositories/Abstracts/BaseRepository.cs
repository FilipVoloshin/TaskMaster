using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using TaskMaster.Infrastructure.Entities.Base;

namespace TaskMaster.Infrastructure.Repositories.Abstracts
{
    /// <summary>
    /// The BaseRepository is an abstract class that provides the core functionality for working with the
    /// database context, DbSet, and applying specifications to entities in derived repositories.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity being managed by the repository.</typeparam>
    public abstract class BaseRepository<TEntity>
        where TEntity : BaseEntity
    {
        protected DbContext Context { get; }
        protected DbSet<TEntity> DbSet { get; }

        private readonly ISpecificationEvaluator _specificationEvaluator;

        protected BaseRepository(DbContext context,
            ISpecificationEvaluator specificationEvaluator)
        {
            Context = context;
            DbSet = context.Set<TEntity>();
            _specificationEvaluator = specificationEvaluator;
        }


        /// <summary>
        /// Applies the given specification to the TEntity query.
        /// </summary>
        /// <param name="specification">The specification to apply.</param>
        /// <param name="evaluateCriteriaOnly">Optional. If true, only applies the criteria part of the specification.</param>
        /// <returns>An IQueryable of TEntity with the specification applied.</returns>
        protected IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> specification, bool evaluateCriteriaOnly = false)
        {
            return _specificationEvaluator.GetQuery(DbSet.AsQueryable(), specification, evaluateCriteriaOnly);
        }

        /// <summary>
        /// Applies the given specification to the TEntity query and returns the specified TResult type.
        /// </summary>
        /// <typeparam name="TResult">The type of the result to be returned.</typeparam>
        /// <param name="specification">The specification to apply.</param>
        /// <returns>An IQueryable of TResult with the specification applied.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the specification is null.</exception>
        /// <exception cref="SelectorNotFoundException">Thrown if the specification's selector is null.</exception>
        protected IQueryable<TResult> ApplySpecification<TResult>(ISpecification<TEntity, TResult> specification)
        {
            ArgumentNullException.ThrowIfNull(nameof(specification));

            if (specification.Selector is null)
            {
                throw new SelectorNotFoundException();
            }

            return _specificationEvaluator.GetQuery(DbSet.AsQueryable(), specification);
        }
    }
}
