using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OData.Domain;

namespace OData.Data.EntityTypeConfiguration
{
    public class DeveloperTypeConfiguration : IEntityTypeConfiguration<Developer>
    {
        public void Configure(EntityTypeBuilder<Developer> builder)
        {
            builder.Property(p => p.Name).HasColumnType("varchar(50)").IsRequired();
            builder.HasMany(t => t.TasksToDo).WithOne(u => u.User).HasForeignKey(k => k.UserId);
        }
    }
}
