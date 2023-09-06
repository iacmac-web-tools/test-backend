using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThesesDomain.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }
    public DbSet<PersonResource> person { get; set; } = null!;
    public DbSet<ThesisForm> form { get; set; } = null!;
    public DbSet<ThesisTableItemResource> tableItemResource { get; set; } = null!;
    public DbSet<ThesisTableItemResourceDataTableResult> dataTableResults { get; set; } = null!;
    public DbSet<ThesisResource> theses { get; set; } = null!;
}