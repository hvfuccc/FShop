using FShop.Dto.Common;
using FShop.Dto.Images;
using FShop.Dto.Products;
using FShop.Dto.Products.Admin;

namespace FShop.Service.Products
{
    public interface IAdminProductService
    {
        Task<ProductViewModel> GetProductById(int productId, string languageId);

        Task<int> Create(ProductCreateRequest request);

        Task<int> Update(ProductUpdateRequest request);

        Task<int> Delete(int productId);

        Task<bool> UpdatePrice(int productId, decimal newPrice, decimal newOriginalPrice);

        Task<bool> UpdateStock(int productId, int addQuantity);

        Task AddViewCount(int productId);

        Task<PageResult<ProductViewModel>> GetAllPaging(ProductPagingAdminRequest request);

        Task<int> AddImage(int productId, ImageCreateRequest request);

        Task<int> UpdateImage(int imageId, ImageUpdateRequest request);

        Task<int> DeleteImage(int imageId);

        Task<ImageViewModel> GetImageById(int imageId);

        Task<List<ImageViewModel>> GetAllImages(int productId);
    }
}