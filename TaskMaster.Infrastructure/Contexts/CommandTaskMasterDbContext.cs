using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace TaskMaster.Infrastructure.Contexts
{
    /// <summary>
    /// Represents the Command TaskMaster database context for write operations.
    /// </summary>
    public class CommandTaskMasterDbContext : BaseTaskMasterDbContext
    {
        public CommandTaskMasterDbContext(DbContextOptions<CommandTaskMasterDbContext> options) : base(options)
        {
            base.ChangeTracker.CascadeDeleteTiming = CascadeTiming.OnSaveChanges;
            base.ChangeTracker.DeleteOrphansTiming = CascadeTiming.OnSaveChanges;
        }
    }
}