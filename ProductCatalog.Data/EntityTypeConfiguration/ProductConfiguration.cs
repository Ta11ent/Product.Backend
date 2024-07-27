using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductCatalog.Domain;

namespace ProductCatalog.Data.EntityTypeConfiguration
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.ProductId);
            builder.HasIndex(x => x.ProductId).IsUnique();
            builder.HasIndex(x => x.Name).IsUnique();
            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(300).IsRequired();
            builder.HasOne(x => x.SubCategory)
                .WithMany(x => x.Products)
                .HasPrincipalKey(x => x.SubCategoryId)
                .HasForeignKey(x => x.SubCategoryId);
            builder.HasOne(x => x.Manufacturer)
                .WithMany(x => x.Products)
                .HasPrincipalKey(x => x.ManufacturerId)
                .HasForeignKey(x => x.ManufacturerId);
        }
    }
}
