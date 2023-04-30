using Microsoft.EntityFrameworkCore;

namespace Server.Models
{
    public class DataBaseContext : DbContext
    {
        public DbSet<Tesis> Tesises { get; set; } = null!;

        public DataBaseContext() {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost;user=root;password=SomePassword123;database=tesisesDb;",
                new MySqlServerVersion(new Version(8, 0, 33)));
        }
    }
}
