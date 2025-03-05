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
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.Property(x => x.Firstname).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Lastname).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Dob).IsRequired();
        }
    }
}
