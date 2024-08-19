using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoppingCart.Domain;

namespace ShoppingCart.Persistence.EntityTypeConfiguration
{
    internal class StatusConfiguration : IEntityTypeConfiguration<Status>
    {
        public void Configure(EntityTypeBuilder<Status> builder)
        {
            builder.HasKey(x => x.StatusId);
            builder.HasIndex(x => x.StatusId).IsUnique();
            builder.Property(x => x.StatusDate).IsRequired();
            builder.Property(x => x.OrderId).IsRequired();
            builder.Property(x => x.StatusId).IsRequired();
            builder.HasOne(x => x.Order)
                .WithMany(x => x.Statuses)
                .HasPrincipalKey(x => x.OrderId)
                .HasForeignKey(x => x.OrderId);
        }
    }
}
