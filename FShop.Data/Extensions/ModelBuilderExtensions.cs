using FShop.Data.Entities;
using FShop.Data.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FShop.Data.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppConfig>().HasData(
                new AppConfig()
                {
                    Key = "HomeTiTle",
                    Value = "This is home page of FShop"
                },
                new AppConfig()
                {
                    Key = "HomeKeyword",
                    Value = "This is keyword of FShop"
                },
                new AppConfig()
                {
                    Key = "HomeDescription",
                    Value = "This is description of FShop"
                }
                );
            modelBuilder.Entity<Language>().HasData(
                new Language() 
                { 
                    Id = "vi-VN", 
                    Name = "Tiếng Việt", 
                    IsDefault = true 
                },
                new Language() 
                { 
                    Id = "en-VN", 
                    Name = "English", 
                    IsDefault = false 
                }
                );
            modelBuilder.Entity<Category>().HasData(
                new Category()
                {
                    Id = 1,
                    IsShowOnHome = true,
                    ParentId = null,
                    SortOrder = 1,
                    Status = Status.Active
                },
                new Category()
                {
                    Id = 2,
                    IsShowOnHome = true,
                    ParentId = null,
                    SortOrder = 2,
                    Status = Status.Active
                }
                );
            modelBuilder.Entity<CategoryTranslation>().HasData(
                new CategoryTranslation()
                {
                    Id = 1,
                    CategoryId = 1,
                    Name = "Ô tô",
                    LanguageId = "vi-VN",
                    SeoAlias = "o-to",
                    SeoDescription = "Ô tô Vinfast",
                    SeoTitle = "Ô tô Vinfast"
                },
                new CategoryTranslation()
                {
                    Id = 2,
                    CategoryId = 1,
                    Name = "Car",
                    LanguageId = "en-VN",
                    SeoAlias = "car",
                    SeoDescription = "Vinfast Car",
                    SeoTitle = "Vinfast Car"
                },
                new CategoryTranslation()
                {
                    Id = 3,
                    CategoryId = 2,
                    Name = "Xe máy điện",
                    LanguageId = "vi-VN",
                    SeoAlias = "xe-may-dien",
                    SeoDescription = "Xe máy điện Vinfast",
                    SeoTitle = "Xe máy điện Vinfast"
                },
                new CategoryTranslation()
                {
                    Id = 4,
                    CategoryId = 2,
                    Name = "E-Scooters",
                    LanguageId = "en-VN",
                    SeoAlias = "e-scooters",
                    SeoDescription = "Vinfast E-Scooters",
                    SeoTitle = "Vinfast E-Scooters"
                }
                );
            modelBuilder.Entity<Product>().HasData(
                new Product()
                {
                    Id = 1,
                    Price = 1499000000,
                    OriginalPrice = 1399000000,
                    Stock = 0,
                    ViewCount = 0
                },
                new Product()
                {
                    Id = 2,
                    Price = 1699000000,
                    OriginalPrice = 1599000000,
                    Stock = 0,
                    ViewCount = 0
                }
                );
            modelBuilder.Entity<ProductTranslation>().HasData(
                new ProductTranslation()
                {
                    Id = 1,
                    ProductId = 1,
                    Name = "VF 9 Eco",
                    LanguageId = "vi-VN",
                    SeoAlias = "vf-9-eco",
                    SeoDescription = "VF 9 là mẫu xe SUV 7 chỗ hàng đầu của VinFast. Với kiểu dáng tinh tế, công nghệ tiên tiến nhất và sự tỉ mỉ trong từng chi tiết, VF 9 mang đến trải nghiệm đặc biệt cao cấp cho Người sở hữu.",
                    SeoTitle = "Hạng sang",
                    Description = "Nâng tầm thời thượng",
                    Details = "Mạnh mẽ, bề thế"
                },
                new ProductTranslation()
                {
                    Id = 2,
                    ProductId = 1,
                    Name = "VF 9 Eco",
                    LanguageId = "en-VN",
                    SeoAlias = "vf-9-eco",
                    SeoDescription = "VF 9 is VinFast's leading 7-seat SUV. With sophisticated design, the most advanced technology and meticulous attention to detail, VF 9 offers experience especially premium for Owners.",
                    SeoTitle = "Luxury",
                    Description = "Level Uptrendy",
                    Details = "Strong, imposing"
                },
                new ProductTranslation()
                {
                    Id = 3,
                    ProductId = 2,
                    Name = "VF 9 Plus",
                    LanguageId = "vi-VN",
                    SeoAlias = "vf-9-plus",
                    SeoDescription = "VF 9 là mẫu xe SUV 7 chỗ hàng đầu của VinFast. Với kiểu dáng tinh tế, công nghệ tiên tiến nhất và sự tỉ mỉ trong từng chi tiết, VF 9 mang đến trải nghiệm đặc biệt cao cấp cho Người sở hữu.",
                    SeoTitle = "Hạng sang",
                    Description = "Nâng tầm thời thượng",
                    Details = "Mạnh mẽ, bề thế"
                },
                new ProductTranslation()
                {
                    Id = 4,
                    ProductId = 2,
                    Name = "VF 9 Plus",
                    LanguageId = "en-VN",
                    SeoAlias = "vf-9-plus",
                    SeoDescription = "VF 9 is VinFast's leading 7-seat SUV. With sophisticated design, the most advanced technology and meticulous attention to detail, VF 9 offers experience especially premium for Owners.",
                    SeoTitle = "Luxury",
                    Description = "Level Uptrendy",
                    Details = "Strong, imposing"
                }
                );
            modelBuilder.Entity<ProductCategory>().HasData(
                new ProductCategory()
                {
                    ProductId = 1,
                    CategoryId = 1
                },
                new ProductCategory()
                {
                    ProductId = 2,
                    CategoryId = 1
                }
                );
        }
    }
}
