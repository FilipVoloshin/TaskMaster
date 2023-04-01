using Microsoft.EntityFrameworkCore;
using TaskMaster.Infrastructure.Entities;

namespace TaskMaster.Infrastructure.Contexts
{
    internal class TaskMasterDbContext : DbContext
    {
        internal TaskMasterDbContext(DbContextOptions<TaskMasterDbContext> options) : base(options)
        {
        }

        internal DbSet<User> Users { get; set; } = null!;
        internal DbSet<TaskList> TaskLists { get; set; } = null!;
        internal DbSet<AssignedTaskList> AssignedTaskLists { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TaskMasterDbContext).Assembly);
        }
    }
}
