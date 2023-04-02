using Microsoft.EntityFrameworkCore;
using TaskMaster.Infrastructure.Entities;

namespace TaskMaster.Infrastructure.Contexts
{
    public class TaskMasterDbContext : BaseTaskMasterDbContext
    {
        public TaskMasterDbContext(DbContextOptions<TaskMasterDbContext> options) : base(options)
        {
        }
    }
}