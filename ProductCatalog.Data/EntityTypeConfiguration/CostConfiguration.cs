using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Domain;

namespace ProductCatalog.Persistence.EntityTypeConfiguration
{
    internal class CostConfiguration : IEntityTypeConfiguration<Cost>
    {
        public void Configure(EntityTypeBuilder<Cost> builder)
        {
            builder.HasKey(x => x.CostId);
            builder.HasIndex(x => x.CostId).IsUnique();
            builder.Property(x => x.CostId).IsRequired();
            builder.Property(x => x.Price).IsRequired();
            builder.HasOne(x => x.ProductSale)
                .WithMany(y => y.Costs)
                .HasPrincipalKey(x => x.ProductSaleId)
                .HasForeignKey(x => x.ProductSaleId);
        }
    }
}
