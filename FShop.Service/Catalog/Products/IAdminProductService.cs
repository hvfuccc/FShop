using FShop.Service.Dto;
using FShop.Service.Dto.Products;
using FShop.Service.Dto.Products.Admin;

namespace FShop.Service.Catalog.Products
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
    }
}