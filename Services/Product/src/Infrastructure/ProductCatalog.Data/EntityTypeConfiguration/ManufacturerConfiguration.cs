using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductCatalog.Domain;

namespace ProductCatalog.Persistence.EntityTypeConfiguration
{
    internal class ManufacturerConfiguration : IEntityTypeConfiguration<Domain.Manufacturer>
    {
        public void Configure(EntityTypeBuilder<Manufacturer> builder)
        {
            builder.HasKey(x => x.ManufacturerId);
            builder.HasIndex(x => x.ManufacturerId).IsUnique();
            builder.HasIndex(x => x.Name).IsUnique();
            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(300).IsRequired();
        }
    }
}
