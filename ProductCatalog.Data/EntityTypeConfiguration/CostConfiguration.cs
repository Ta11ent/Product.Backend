using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Domain;

namespace ProductCatalog.Persistence.EntityTypeConfiguration
{
    internal class CostConfiguration : IEntityTypeConfiguration<Cost>
    {
        public void Configure(EntityTypeBuilder<Cost> builder)
        {
            builder.HasKey(x => x.PriceId);
            builder.HasIndex(x => x.PriceId).IsUnique();
            builder.Property(x => x.PriceId).IsRequired();
            builder.Property(x => x.Price).IsRequired();
            builder.HasOne(x => x.Product)
                .WithMany(y => y.Costs)
                .HasPrincipalKey(x => x.ProductId)
                .HasForeignKey(x => x.ProductId);
        }
    }
}
