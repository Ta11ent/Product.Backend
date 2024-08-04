using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductCatalog.Domain;

namespace ProductCatalog.Persistence.EntityTypeConfiguration
{
    internal class CurrencyConfiguration : IEntityTypeConfiguration<Domain.Currency>
    {
        public void Configure(EntityTypeBuilder<Currency> builder)
        {
            builder.HasKey(x => x.CurrencyId);
            builder.HasIndex(x => x.CurrencyId).IsUnique();
            builder.HasIndex(x => x.Code).IsUnique();
            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Code).HasMaxLength(5).IsRequired();
        }
    }
}
