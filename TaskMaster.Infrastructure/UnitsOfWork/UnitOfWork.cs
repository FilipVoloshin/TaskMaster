using Microsoft.EntityFrameworkCore;
using TaskMaster.Infrastructure.Contexts;
using TaskMaster.Infrastructure.Repositories.Abstractions;

namespace TaskMaster.Infrastructure.UnitsOfWork
{
    /// <summary>
    /// Implements the unit of work pattern for managing database transactions and coordinating changes made to one or more repositories.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CommandTaskMasterDbContext _context;
        private readonly IRepositoryFactory _repositoryFactory;
        public UnitOfWork(CommandTaskMasterDbContext context, IRepositoryFactory repositoryFactory)
        {
            _context = context;
            _repositoryFactory = repositoryFactory;
        }

        public TRepository Repository<TRepository>() where TRepository : IRepository =>
            _repositoryFactory.GetRepository<TRepository>();

        public async Task CompleteAsync(CancellationToken cancellationToken = default)
        {
            _context.ChangeTracker.DetectChanges();

            await _context.SaveChangesAsync(cancellationToken);

            foreach (var entity in _context.ChangeTracker.Entries())
            {
                entity.State = EntityState.Detached;
            }
        }
    }
}
