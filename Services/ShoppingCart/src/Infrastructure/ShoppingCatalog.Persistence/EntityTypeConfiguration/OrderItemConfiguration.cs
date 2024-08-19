using ShoppingCart.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ShoppingCart.Persistence.EntityTypeConfiguration
{
    internal class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(x => x.OrderItemId);
            builder.HasIndex(x => x.OrderItemId).IsUnique();
            builder.Property(x => x.ProductId).IsRequired();
            builder.Property(x => x.Count).IsRequired();
            builder.HasOne(x => x.Order)
                .WithMany(x => x.OrderItems)
                .HasPrincipalKey(x => x.OrderId)
                .HasForeignKey(x => x.OrderId);
        }
    }
}
