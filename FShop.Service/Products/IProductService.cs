using FShop.Dto.Common;
using FShop.Dto.Products;
using FShop.Dto.Products.Client;

namespace FShop.Service.Products
{
    public interface IProductService
    {
        Task<PageResult<ProductViewModel>> GetAllByCategoryId(ProductPagingRequest request);
        Task<List<ProductViewModel>> GetAll();
    }
}