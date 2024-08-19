using ShoppingCart.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ShoppingCart.Persistence.EntityTypeConfiguration
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.OrderId);
            builder.HasIndex(x => x.OrderId).IsUnique();
            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.Number).UseIdentityColumn(seed: 1, increment: 1);
        }
    }
}
