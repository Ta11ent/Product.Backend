using ShoppingCart.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ShoppingCart.Persistence.EntityTypeConfiguration
{
    internal class ProductRangeConfiguration : IEntityTypeConfiguration<ProductRange>
    {
        public void Configure(EntityTypeBuilder<ProductRange> builder)
        {
            builder.HasKey(x => x.ProductRangeId);
            builder.HasIndex(x => x.ProductRangeId).IsUnique();
            builder.Property(x => x.ProductId).IsRequired();
            builder.Property(x => x.Count).IsRequired();
        }
    }
}
