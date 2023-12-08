using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Domain;

namespace ProductCatalog.Persistence.EntityTypeConfiguration
{
    public class CostConfiguration : IEntityTypeConfiguration<Cost>
    {
        public void Configure(EntityTypeBuilder<Cost> builder)
        {
            builder.HasKey(x => x.PriceId);
            builder.HasIndex(x => x.PriceId).IsUnique();
            builder.Property(x => x.PriceId).IsRequired();
            builder.Property(x => x.Price).IsRequired();
        }
    }
}
