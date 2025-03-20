using FShop.Data.Entities;
using FShop.Data.EntityFramework;
using FShop.Dto.Common;
using FShop.Dto.Images;
using FShop.Dto.Products;
using FShop.Dto.Products.Admin;
using FShop.Service.Images;
using FShop.Utilities.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;

namespace FShop.Service.Products.Impl
{
    public class AdminProductService(FShopDBContext _context, IStorageService _storageService) : IAdminProductService
    {
        public async Task<int> AddImage(int productId, ImageCreateRequest request)
        {
            var productImage = new Image()
            {
                Caption = request.Caption,
                DateCreated = DateTime.Now,
                IsDefault = request.IsDefault,
                SortOrder = request.SortOrder,
                ProductId = productId
            };
            if (request.ImageFile != null)
            {
                productImage.ImagePath = await this.SaveFile(request.ImageFile);
                productImage.FileSize = request.ImageFile.Length;
            }
            _context.Images.Add(productImage);
            await _context.SaveChangesAsync();
            return productImage.Id;
        }

        public async Task AddViewCount(int productId)
        {
            try
            {
                var product = await _context.Products.FindAsync(productId) ?? throw new FShopException("Không tìm thấy sản phẩm");
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
                if (request.ThumbnailImage != null)
                {
                    product.Images = new List<Image>()
                    {
                        new Image()
                        {
                            Caption = "Thumbnail image",
                            DateCreated = DateTime.Now,
                            FileSize = request.ThumbnailImage.Length,
                            ImagePath = await this.SaveFile(request.ThumbnailImage),
                            IsDefault = true,
                            SortOrder = 1
                        }
                    };
                }
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return product.Id;
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
                var images = _context.Images.Where(i => i.ProductId == productId);
                foreach (var image in images)
                {
                    await _storageService.DeleteFileAsync(image.ImagePath);
                }
                _context.Products.Remove(product);
                return await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new FShopNotImplementedException();
            }
        }

        public async Task<int> DeleteImage(int imageId)
        {
            var productImage = await _context.Images.FindAsync(imageId) ?? throw new FShopException("Không tìm thấy ảnh");
            _context.Images.Remove(productImage);
            return await _context.SaveChangesAsync();
        }

        public async Task<List<ImageViewModel>> GetAllImages(int productId)
        {
            return await _context.Images.Where(x => x.ProductId == productId)
                .Select(i => new ImageViewModel()
                {
                    Caption = i.Caption,
                    DateCreated = i.DateCreated,
                    FileSize = i.FileSize,
                    Id = i.Id,
                    ImagePath = i.ImagePath,
                    IsDefault = i.IsDefault,
                    ProductId = i.ProductId,
                    SortOrder = i.SortOrder,
                }).ToListAsync();
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

        public async Task<ImageViewModel> GetImageById(int imageId)
        {
            var image = await _context.Images.FindAsync(imageId) ?? throw new FShopException("Không tìm thấy ảnh");
            var viewModel = new ImageViewModel()
            {
                Id = image.Id,
                Caption = image.Caption,
                DateCreated = image.DateCreated,
                FileSize = image.FileSize,
                ImagePath = image.ImagePath,
                SortOrder = image.SortOrder,
                IsDefault = image.IsDefault,
                ProductId = image.ProductId,
            };
            return viewModel;
        }

        public async Task<ProductViewModel> GetProductById(int productId, string languageId)
        {
            try
            {
                var product = await _context.Products.FindAsync(productId);
                var productTranslation = await _context.ProductTranslations.FirstOrDefaultAsync(x => x.ProductId == productId && x.LanguageId == languageId);
                var productViewModel = new ProductViewModel()
                {
                    Id = product.Id,
                    Price = product.Price,
                    OriginalPrice = product.OriginalPrice,
                    DateCreated = product.DateCreated,
                    Stock = product.Stock,
                    ViewCount = product.ViewCount,
                    Description = productTranslation != null ? productTranslation.Description : null,
                    Details = productTranslation != null ? productTranslation.Details : null,
                    Name = productTranslation != null ? productTranslation.Name : null,
                    LanguageId = productTranslation.LanguageId,
                    SeoAlias = productTranslation != null ? productTranslation.SeoAlias : null,
                    SeoDescription = productTranslation != null ? productTranslation.SeoDescription : null,
                    SeoTitle = productTranslation != null ? productTranslation.SeoTitle : null
                };
                return productViewModel;
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
                if (request.ThumbnailImage != null)
                {
                    var thumbnailImage = await _context.Images.FirstOrDefaultAsync(i => i.IsDefault == true && i.ProductId == request.Id);
                    if (thumbnailImage != null)
                    {
                        thumbnailImage.FileSize = request.ThumbnailImage.Length;
                        thumbnailImage.ImagePath = await this.SaveFile(request.ThumbnailImage);
                        _context.Images.Update(thumbnailImage);
                    }
                }
                return await _context.SaveChangesAsync();
            }
            catch
            {
                throw new FShopNotImplementedException();
            }
        }

        public async Task<int> UpdateImage(int imageId, ImageUpdateRequest request)
        {
            var productImage = await _context.Images.FindAsync(imageId) ?? throw new FShopException("Không tìm thấy ảnh như yêu cầu");
            if (request.ImageFile != null)
            {
                productImage.ImagePath = await this.SaveFile(request.ImageFile);
                productImage.FileSize = request.ImageFile.Length;
            }
            _context.Images.Update(productImage);
            return await _context.SaveChangesAsync();
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

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }
    }
}