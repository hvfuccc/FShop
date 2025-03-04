using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FShop.Data.EntityFramework
{
    public class FShopDBContextFactory : IDesignTimeDbContextFactory<FShopDBContext>
    {
        public FShopDBContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("FShopDatabase");

            var optionsBuilder = new DbContextOptionsBuilder<FShopDBContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new FShopDBContext(optionsBuilder.Options);
        }
    }
}
