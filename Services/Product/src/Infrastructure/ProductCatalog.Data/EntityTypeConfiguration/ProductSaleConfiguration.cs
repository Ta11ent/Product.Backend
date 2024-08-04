using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductCatalog.Domain;
using System.Security.Cryptography.X509Certificates;

namespace ProductCatalog.Persistence.EntityTypeConfiguration
{
    internal class ProductSaleConfiguration : IEntityTypeConfiguration<ProductSale>
    {
        public void Configure(EntityTypeBuilder<ProductSale> builder)
        {
            builder.HasKey(x => x.ProductSaleId);
            builder.HasIndex(x => x.ProductSaleId).IsUnique();
            builder.HasOne(x => x.SubCategory)
                .WithMany(x => x.ProductsForSale)
                .HasPrincipalKey(x => x.SubCategoryId)
                .HasForeignKey(x => x.SubCategoryId);
            //builder.Property(x => x.CategoryId).IsRequired();
            builder.Property(x => x.SubCategoryId).IsRequired();
            builder.Property(x => x.ProductId).IsRequired();
            builder.Property(x => x.Available).IsRequired();
        }
    }
}
