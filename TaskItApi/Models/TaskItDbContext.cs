using Microsoft.EntityFrameworkCore;
using TaskItApi.Entities;

namespace TaskItApi.Models
{
    public class TaskItDbContext : DbContext
    {
        public TaskItDbContext(DbContextOptions<TaskItDbContext> options)
            :base(options)
        {     
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Icon> Icons { get; set; }
        public DbSet<Color> Colors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            DbSeeder.SeedColors(modelBuilder);
            DbSeeder.SeedIcons(modelBuilder);
        }

     }
}
