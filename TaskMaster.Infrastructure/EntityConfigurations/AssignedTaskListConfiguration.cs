using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskMaster.Infrastructure.Entities;

namespace TaskMaster.Infrastructure.EntityConfigurations
{
    internal class AssignedTaskListConfiguration : IEntityTypeConfiguration<AssignedTaskList>
    {
        public void Configure(EntityTypeBuilder<AssignedTaskList> builder)
        {
            builder.HasKey(utl => new { utl.UserId, utl.TaskListId });

            builder.HasOne(utl => utl.User)
                .WithMany(u => u.AssignedTaskLists)
                .HasForeignKey(utl => utl.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(utl => utl.Author)
                .WithMany(o => o.OwnedTaskLists)
                .HasForeignKey(utl => utl.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(utl => utl.TaskList)
                .WithMany(t => t.Assignees)
                .HasForeignKey(utl => utl.TaskListId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
