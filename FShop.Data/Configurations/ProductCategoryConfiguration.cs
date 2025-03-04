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
    class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.ToTable("ProductCategories");
            builder.HasKey(x => new { x.CategoryId, x.ProductId });
            builder.HasOne(pc => pc.Product).WithMany(p => p.ProductCategories).HasForeignKey(pc => pc.ProductId);
            builder.HasOne(pc => pc.Category).WithMany(c => c.ProductCategories).HasForeignKey(pc => pc.CategoryId);
        }
    }
}
