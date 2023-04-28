using Microsoft.EntityFrameworkCore;
using Theses.Domain.Entities;

namespace Theses.Application.Common.Interfaces;

public interface IApplicationContext
{
    DbSet<Person> Persons { get; }
    DbSet<Thesis> Theses { get; }

    Task<int> SaveChangesAsync(CancellationToken ct = default);
}
