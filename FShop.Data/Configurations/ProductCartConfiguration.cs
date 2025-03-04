using FShop.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FShop.Data.Configurations
{
    public class ProductCartConfiguration : IEntityTypeConfiguration<ProductCart>
    {
        public void Configure(EntityTypeBuilder<ProductCart> builder)
        {
            builder.ToTable("ProductCarts");
            builder.HasKey(x => new { x.CartId, x.ProductId });
            builder.HasOne(pc => pc.Product).WithMany(p => p.ProductCarts).HasForeignKey(pc => pc.ProductId);
            builder.HasOne(pc => pc.Cart).WithMany(c => c.ProductCarts).HasForeignKey(pc => pc.CartId);
        }
    }
}
