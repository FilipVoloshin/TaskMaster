using Microsoft.EntityFrameworkCore;
using TaskMaster.Infrastructure.Entities;

namespace TaskMaster.Infrastructure.Contexts
{
    public class TaskMasterDbContext : DbContext
    {
        public TaskMasterDbContext(DbContextOptions<TaskMasterDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<TaskList> TaskLists { get; set; } = null!;
        public DbSet<AssignedTaskList> AssignedTaskLists { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TaskMasterDbContext).Assembly);
        }
    }
}
