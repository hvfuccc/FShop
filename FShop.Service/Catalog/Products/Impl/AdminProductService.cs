using FShop.Data.Entities;
using FShop.Data.EntityFramework;
using FShop.Service.Dto;
using FShop.Service.Dto.Products;
using FShop.Service.Dto.Products.Admin;
using FShop.Utilities.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace FShop.Service.Catalog.Products.Impl
{
    public class AdminProductService(FShopDBContext _context) : IAdminProductService
    {
        //private readonly FShopDBContext _context = context;

        public async Task AddViewCount(int productId)
        {
            try
            {
                var product = await _context.Products.FindAsync(productId);
                product.ViewCount += 1;
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new FShopNotImplementedException();
            }
        }

        public async Task<int> Create(ProductCreateRequest request)
        {
            try
            {
                var product = new Product()
                {
                    Price = request.Price,
                    OriginalPrice = request.OriginalPrice,
                    Stock = request.Stock,
                    ViewCount = 0,
                    DateCreated = DateTime.Now,
                    ProductTranslations =
                    [
                        new ProductTranslation()
                        {
                            Name=request.Name,
                            Description=request.Description,
                            Details=request.Details,
                            SeoDescription=request.SeoDescription,
                            SeoTitle=request.SeoTitle,
                            SeoAlias=request.SeoAlias,
                            LanguageId=request.LanguageId
                        }
                    ]
                };
                _context.Products.Add(product);
                return await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new FShopNotImplementedException();
            }
        }

        public async Task<int> Delete(int productId)
        {
            try
            {
                var product = await _context.Products.FindAsync(productId) ?? throw new FShopException("Không tìm thấy sản phẩm");
                _context.Products.Remove(product);
                return await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new FShopNotImplementedException();
            }
        }

        public async Task<PageResult<ProductViewModel>> GetAllPaging(ProductPagingAdminRequest request)
        {
            try
            {
                var query = from p in _context.Products
                            join pt in _context.ProductTranslations on p.Id equals pt.ProductId
                            join pc in _context.ProductCategories on p.Id equals pc.ProductId
                            join c in _context.Categories on pc.CategoryId equals c.Id
                            select new { p, pt, pc };

                if (!string.IsNullOrEmpty(request.Keyword))
                    query = query.Where(x => x.pt.Name.Contains(request.Keyword));
                if (request.CategoryIds.Count > 0)
                    query = query.Where(p => request.CategoryIds.Contains(p.pc.CategoryId));

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
                        SeoTitle = x.pt.SeoTitle
                    }).ToListAsync();
                var pageResult = new PageResult<ProductViewModel>()
                {
                    TotalRecord = totalRow,
                    Items = data
                };
                return pageResult;
            }
            catch (Exception)
            {
                throw new FShopNotImplementedException();
            }
        }

        public async Task<int> Update(ProductUpdateRequest request)
        {
            try
            {
                var product = await _context.Products.FindAsync(request.Id);
                var productTranslation = await _context.ProductTranslations.FirstOrDefaultAsync(x => x.ProductId == request.Id && x.LanguageId == request.LanguageId);
                if (product == null || productTranslation == null) throw new FShopException("Không tìm thấy sản phẩm");
                productTranslation.Name = request.Name;
                productTranslation.Description = request.Description;
                productTranslation.Details = request.Details;
                productTranslation.SeoAlias = request.SeoAlias;
                productTranslation.SeoDescription = request.SeoDescription;
                productTranslation.SeoTitle = request.SeoTitle;
                return await _context.SaveChangesAsync();
            }
            catch
            {
                throw new FShopNotImplementedException();
            }
        }

       public async Task<bool> UpdatePrice(int productId, decimal newPrice, decimal newOriginalPrice)
        {
            try
            {
                var product = await _context.Products.FindAsync(productId) ?? throw new FShopException("Không tìm thấy sản phẩm");
                product.Price = newPrice;
                product.OriginalPrice = newOriginalPrice;
                return await _context.SaveChangesAsync() > 0;
            }
            catch
            {
                throw new FShopNotImplementedException();
            }
        }

        public async Task<bool> UpdateStock(int productId, int addQuantity)
        {
            try
            {
                var product = await _context.Products.FindAsync(productId) ?? throw new FShopException("Không tìm thấy sản phẩm");
                product.Stock += addQuantity;
                return await _context.SaveChangesAsync() > 0;
            }
            catch
            {
                throw new FShopNotImplementedException();
            }
        }
    }
}
