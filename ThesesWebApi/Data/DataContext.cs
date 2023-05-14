using Microsoft.EntityFrameworkCore;
using ThesesWebApi.Models;

namespace ThesesWebApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Thesis> Theses { get; set; } = null!;
    }
}
