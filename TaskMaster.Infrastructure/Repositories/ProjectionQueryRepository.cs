using Ardalis.Specification;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using TaskMaster.Infrastructure.Contexts;
using TaskMaster.Infrastructure.Entities.Base;
using TaskMaster.Infrastructure.Repositories.Abstractions;

namespace TaskMaster.Infrastructure.Repositories
{
    /// <summary>
    /// A repository class for TEntity that supports projection operations using AutoMapper.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity being managed by the repository.</typeparam>
    public class ProjectionQueryRepository<TEntity> : BaseRepository<TEntity>, IProjectionQueryRepository<TEntity>
        where TEntity : BaseEntity
    {
        private readonly IMapper _mapper;

        public ProjectionQueryRepository(TaskMasterDbContext context,
            ISpecificationEvaluator evaluator,
            IMapper mapper)
            : base(context, evaluator)
        {
            _mapper = mapper;
        }

        public Task<TProjection?> FirstOrDefaultAsync<TSpec, TProjection>(TSpec specification, CancellationToken cancellationToken = default)
            where TSpec : ISpecification<TEntity>, ISingleResultSpecification =>
            ApplySpecification(specification).ProjectTo<TProjection>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(cancellationToken);

        public async Task<IEnumerable<TProjection>> GetAsync<TProjection>(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
        {
            return await ApplySpecification(specification).ProjectTo<TProjection>(_mapper.ConfigurationProvider).ToArrayAsync(cancellationToken);
        }
    }
}
