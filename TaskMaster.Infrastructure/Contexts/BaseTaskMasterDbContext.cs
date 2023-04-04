using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TaskMaster.Infrastructure.Entities;

namespace TaskMaster.Infrastructure.Contexts
{
    /// <summary>
    /// Base DbContext for TaskMaster application.
    /// </summary>
    public abstract class BaseTaskMasterDbContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<TaskList> TaskLists { get; set; } = null!;
        public DbSet<AssignedTaskList> AssignedTaskLists { get; set; } = null!;

        public BaseTaskMasterDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}
