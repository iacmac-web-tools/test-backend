using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThesesDomain.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace DAL
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=thesis;Username=postgres;Password=wasdwasd543210");
        }
        public DbSet<PersonResource> person{ get; set; } = null!;
        public DbSet<ThesisForm> form  { get; set; } = null!;
        public DbSet<ThesisTableItemResource> tableItemResource  { get; set; } = null!;
        public DbSet<ThesisTableItemResourceDataTableResult> dataTableResults  { get; set; } = null!;
        public DbSet<ThesisResource> theses { get; set; } = null!;
    }
}
