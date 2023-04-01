using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskMaster.Infrastructure.Entities;

namespace TaskMaster.Infrastructure.EntityConfigurations
{
    public class AssignedTaskListConfiguration : IEntityTypeConfiguration<AssignedTaskList>
    {
        public void Configure(EntityTypeBuilder<AssignedTaskList> builder)
        {
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

            builder.HasIndex(utl => new { utl.UserId, utl.TaskListId }).IsUnique();

        }
    }
}
