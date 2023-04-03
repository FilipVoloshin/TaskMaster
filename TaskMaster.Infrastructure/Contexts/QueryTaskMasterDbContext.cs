using Microsoft.EntityFrameworkCore;

namespace TaskMaster.Infrastructure.Contexts
{
    /// <summary>
    /// Represents the Query TaskMaster database context for read operations.
    /// </summary>
    public class QueryTaskMasterDbContext : BaseTaskMasterDbContext
    {
        public QueryTaskMasterDbContext(DbContextOptions<QueryTaskMasterDbContext> options) : base(options)
        {
            base.ChangeTracker.AutoDetectChangesEnabled = false;
        }
    }
}