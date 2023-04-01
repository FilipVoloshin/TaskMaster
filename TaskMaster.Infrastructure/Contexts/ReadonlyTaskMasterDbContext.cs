using Microsoft.EntityFrameworkCore;
using TaskMaster.Infrastructure.Entities;

namespace TaskMaster.Infrastructure.Contexts
{
    public class ReadonlyTaskMasterDbContext : BaseTaskMasterDbContext
    {
        public ReadonlyTaskMasterDbContext(DbContextOptions<ReadonlyTaskMasterDbContext> options) : base(options)
        {
        }
    }
}
