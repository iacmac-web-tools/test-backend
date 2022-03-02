using BackTask.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace BackTask.Data
{
    #pragma warning disable CS1591
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) :base(options) {}

        public DbSet<ThesisResource> theses { get; set; } = null!;
        // public DbSet<PersonResource> persons { get; set; } = null!;
    }
    #pragma warning restore CS1591
}