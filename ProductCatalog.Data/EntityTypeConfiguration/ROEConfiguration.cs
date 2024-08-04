using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductCatalog.Domain;

namespace ProductCatalog.Persistence.EntityTypeConfiguration
{
    internal class ROEConfiguration : IEntityTypeConfiguration<ROE>
    {
        public void Configure(EntityTypeBuilder<ROE> builder)
        {
            builder.HasKey(x => x.ROEId);
            builder.HasIndex(x => x.ROEId).IsUnique();
            builder.HasOne(x => x.Currency)
              .WithMany(x => x.ROEs)
              .HasPrincipalKey(x => x.CurrencyId)
              .HasForeignKey(x => x.CurrecnyId);
            builder.Property(x => x.Rate).HasPrecision(18, 8);
            builder.Property(x => x.DateFrom).IsRequired();
        }
    }
}
