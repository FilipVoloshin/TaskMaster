﻿using Ardalis.Specification;
using TaskMaster.Infrastructure.Contexts;
using TaskMaster.Infrastructure.Entities.Base;
using TaskMaster.Infrastructure.Repositories.Abstractions;

namespace TaskMaster.Infrastructure.Repositories
{
    public class CommandRepository<TEntity> : BaseRepository<TEntity>, ICommandRepository<TEntity>
        where TEntity : BaseEntity
    {
        // TODO: in future replace the QueryTaskMasterDbContext with CommandTraskMasterContext (to separete write and read operations)
        public CommandRepository(TaskMasterDbContext context,
            ISpecificationEvaluator evaluator)
            : base(context, evaluator)
        {

        }

        public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await DbSet.AddAsync(entity, cancellationToken);
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
        }
    }
}