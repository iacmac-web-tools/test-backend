using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Theses.Domain.Entities;

namespace Theses.Infrastructure.Persistence.Configuration;

public class ThesisConfiguration : IEntityTypeConfiguration<Thesis>
{
    public void Configure(EntityTypeBuilder<Thesis> builder)
    {
        builder.HasIndex(x => x.Id);
        builder.Property(x => x.ContactEmail).IsRequired();
        builder.Property(x => x.Topic).IsRequired().HasMaxLength(500);
        builder.Property(x => x.Content).IsRequired().HasMaxLength(5000);
    }
}
