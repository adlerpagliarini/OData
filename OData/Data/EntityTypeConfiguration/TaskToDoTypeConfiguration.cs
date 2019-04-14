using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OData.Domain;

namespace OData.Data.EntityTypeConfiguration
{
    public class TaskToDoTypeConfiguration : IEntityTypeConfiguration<TaskToDo>
    {
        public void Configure(EntityTypeBuilder<TaskToDo> builder)
        {
            builder.Property(p => p.Title).HasColumnType("varchar(50)").IsRequired();
            builder.Property(p => p.DeadLine).IsRequired();
            builder.Property(p => p.Status).IsRequired();
            builder.HasOne(u => u.User).WithMany(t => t.TasksToDo).HasForeignKey(k => k.UserId);
        }
    }
}
