using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskMaster.Infrastructure.Entities;
using static TaskMaster.Shared.Constants;

namespace TaskMaster.Infrastructure.EntityConfigurations
{
    public class TaskListConfiguration : IEntityTypeConfiguration<TaskList>
    {
        public void Configure(EntityTypeBuilder<TaskList> builder)
        {
            builder.Property(t => t.Name)
            .IsRequired()
            .HasMaxLength(255);

            builder.Property(t => t.CreatedAtUtc)
                .HasColumnType(PostgresDataTypes.DateTimeType);

            builder.HasOne(t => t.Author)
                .WithMany(o => o.CreatedTaskLists)
                .HasForeignKey(t => t.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(t => t.CreatedAtUtc);
            builder.HasIndex(t => t.AuthorId);
        }
    }
}
