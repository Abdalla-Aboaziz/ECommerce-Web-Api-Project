using ECommerce.Domain.Entities.OrderModules;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Presistance.Data.Configrations
{
    public class OrderConfigration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(x => x.Subtotal).HasPrecision(8, 2);
            builder.OwnsOne(x => x.Address, OEntity =>
            {
                OEntity.Property(x => x.FirstName).HasMaxLength(50);
                OEntity.Property(x => x.LastName).HasMaxLength(50);
                OEntity.Property(x => x.Street).HasMaxLength(50);
                OEntity.Property(x => x.Country).HasMaxLength(50);
                OEntity.Property(x => x.City).HasMaxLength(50);


            });
        }
    }
}
