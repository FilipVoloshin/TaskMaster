using TaskMaster.Infrastructure.Contexts;
using TaskMaster.Infrastructure.Repositories.Abstractions;

namespace TaskMaster.Infrastructure.UnitsOfWork
{
    /// <summary>
    /// Implements the unit of work pattern for managing database transactions and coordinating changes made to one or more repositories.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TaskMasterDbContext _context;
        private readonly IRepositoryFactory _repositoryFactory;
        public UnitOfWork(TaskMasterDbContext context, IRepositoryFactory repositoryFactory)
        {
            _context = context;
            _repositoryFactory = repositoryFactory;
        }

        public TRepository GetRepository<TRepository>() where TRepository : IRepository =>
            _repositoryFactory.GetRepository<TRepository>();

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
