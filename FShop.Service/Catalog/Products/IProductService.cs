using FShop.Service.Dto;
using FShop.Service.Dto.Products;
using FShop.Service.Dto.Products.Client;

namespace FShop.Service.Catalog.Products
{
    public interface IProductService
    {
        Task<PageResult<ProductViewModel>> GetAllByCategoryId(ProductPagingRequest request);
    }
}