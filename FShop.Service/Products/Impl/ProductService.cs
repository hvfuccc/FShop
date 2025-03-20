using Azure.Core;
using FShop.Data.Entities;
using FShop.Data.EntityFramework;
using FShop.Dto.Common;
using FShop.Dto.Products;
using FShop.Dto.Products.Client;
using FShop.Utilities.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace FShop.Service.Products.Impl
{
    public class ProductService(FShopDBContext _context) : IProductService
    {
        public async Task<PageResult<ProductViewModel>> GetAllByCategoryId(string languageId, ProductPagingRequest request)
        {
            try
            {
                var query = from p in _context.Products
                            join pt in _context.ProductTranslations on p.Id equals pt.ProductId
                            join pc in _context.ProductCategories on p.Id equals pc.ProductId
                            join c in _context.Categories on pc.CategoryId equals c.Id
                            where pt.LanguageId == languageId
                            select new { p, pt, pc };

                if (request.CategoryId.HasValue && request.CategoryId.Value > 0)
                    query = query.Where(p => p.pc.CategoryId == request.CategoryId);

                int totalRow = await query.CountAsync();
                var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .Select(x => new ProductViewModel()
                    {
                        Id = x.p.Id,
                        Price = x.p.Price,
                        OriginalPrice = x.p.OriginalPrice,
                        Stock = x.p.Stock,
                        ViewCount = x.p.ViewCount,
                        DateCreated = x.p.DateCreated,
                        Name = x.pt.Name,
                        Description = x.pt.Description,
                        Details = x.pt.Details,
                        SeoDescription = x.pt.SeoDescription,
                        SeoAlias = x.pt.SeoAlias,
                        SeoTitle = x.pt.SeoTitle,
                        LanguageId = x.pt.LanguageId

                    }).ToListAsync();
                var pageResult = new PageResult<ProductViewModel>()
                {
                    TotalRecord = totalRow,
                    Items = data
                };
                return pageResult;
            }
            catch
            {
                throw new FShopNotImplementedException();
            }
        }
    }
}