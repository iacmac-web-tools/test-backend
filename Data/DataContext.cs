using BackTask.Models;
using Microsoft.EntityFrameworkCore;

namespace BackTask.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) :base(options) {}

        DbSet<DataContext> Theses { get; set; } = null!;
    }
}