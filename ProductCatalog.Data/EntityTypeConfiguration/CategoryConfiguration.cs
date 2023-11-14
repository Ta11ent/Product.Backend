using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductCatalog.Domain;

namespace ProductCatalog.Data.EntityTypeConfiguration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x => x.CategoryId);
            builder.HasIndex(x => x.CategoryId).IsUnique();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Description).HasMaxLength(300);

        }
    }
}
