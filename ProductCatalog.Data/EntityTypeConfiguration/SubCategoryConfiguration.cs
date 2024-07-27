using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductCatalog.Domain;

namespace ProductCatalog.Persistence.EntityTypeConfiguration
{
    internal class SubCategoryConfiguration : IEntityTypeConfiguration<SubCategory>
    {
        public void Configure(EntityTypeBuilder<SubCategory> builder)
        {
            builder.HasKey(x => x.SubCategoryId);
            builder.HasIndex(x => x.SubCategoryId).IsUnique();
            builder.HasIndex(x => x.Name).IsUnique();
            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(300).IsRequired();
            builder.HasOne(x => x.Category)
                .WithMany(x => x.SubCategories)
                .HasPrincipalKey(x => x.CategoryId)
                .HasForeignKey(x => x.CategoryId);
        }
    }
}
