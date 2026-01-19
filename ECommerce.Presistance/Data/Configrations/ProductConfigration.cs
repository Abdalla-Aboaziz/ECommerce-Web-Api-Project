using ECommerce.Domain.Entities.ProductModules;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Presistance.Data.Configrations
{
    public class ProductConfigration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Name).HasMaxLength(100);
            builder.Property(p=>p.Description).HasMaxLength(500);
            builder.Property(p=>p.PictureUrl).HasMaxLength(200);
            builder.Property(p => p.Price).HasPrecision(18, 2);

            builder.HasOne(x => x.ProductBrand).WithMany().HasForeignKey(x => x.BrandId);
            builder.HasOne(x => x.ProductType).WithMany().HasForeignKey(x => x.TypeId);
        }
    }
}
