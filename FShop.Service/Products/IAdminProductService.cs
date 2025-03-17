using FShop.Dto.Common;
using FShop.Dto.Products;
using FShop.Dto.Products.Admin;
using Microsoft.AspNetCore.Http;

namespace FShop.Service.Products
{
    public interface IAdminProductService
    {
        Task<int> Create(ProductCreateRequest request);
        Task<int> Update(ProductUpdateRequest request);
        Task<int> Delete(int productId);
        Task<bool> UpdatePrice(int productId, decimal newPrice, decimal newOriginalPrice);
        Task<bool> UpdateStock(int productId, int addQuantity);
        Task AddViewCount(int productId);
        Task<PageResult<ProductViewModel>> GetAllPaging(ProductPagingAdminRequest request);
        Task<int> AddImages(int productId, List<IFormFile> files);
        Task<int> UpdateImages(int imageId, string caption, bool isDefault);
        Task<int> DeleteImages(int imageId);
        Task<List<ImageViewModel>> GetAllImages(int productId);
    }
}