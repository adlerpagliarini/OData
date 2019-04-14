using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OData.Data.EntityTypeConfiguration;
using OData.Domain;
using System.IO;

namespace OData.Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Developer> Developer { get; set; }
        public DbSet<TaskToDo> TaskToDo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DeveloperTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TaskToDoTypeConfiguration());
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
        }
    }
}
