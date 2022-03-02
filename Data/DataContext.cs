using BackTask.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace BackTask.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) :base(options) {}

        public DbSet<ThesisResource> theses { get; set; } = null!;
    }
}