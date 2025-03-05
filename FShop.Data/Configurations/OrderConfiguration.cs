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
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.OrderDate).HasDefaultValueSql("GETDATE()");
            builder.Property(x => x.ShipName).IsRequired().HasMaxLength(100);
            builder.Property(x => x.ShipAddress).IsRequired().HasColumnType("nvarchar(MAX)");
            builder.Property(x => x.ShipEmail).IsRequired().HasMaxLength(100).IsUnicode(false);
            builder.Property(x => x.ShipPhoneNumber).IsRequired().HasMaxLength(15).IsUnicode(false);
            builder.HasOne(x => x.User).WithMany(x => x.Orders).HasForeignKey(x => x.UserId);
        }
    }
}
