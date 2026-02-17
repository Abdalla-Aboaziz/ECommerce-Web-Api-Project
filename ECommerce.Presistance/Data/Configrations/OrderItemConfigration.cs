using ECommerce.Domain.Entities.OrderModules;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Presistance.Data.Configrations
{
    public class OrderItemConfigration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.Property(x => x.Price).HasPrecision(8, 2);
            builder.OwnsOne(p => p.Product, OEntity =>
            {
                OEntity.Property(x => x.ProductName).HasMaxLength(100);
                OEntity.Property(x => x.PictureUrl).HasMaxLength(200);

            });
        }
    }
}
