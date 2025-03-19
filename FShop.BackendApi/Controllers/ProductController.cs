using FShop.Dto.Products.Admin;
using FShop.Dto.Products.Client;
using FShop.Service.Products;
using FShop.Utilities.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace FShop.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IProductService _productService, IAdminProductService _adminProductService) : ControllerBase
    {
        [HttpGet("{languageId}")]
        public async Task<IActionResult> Get(string languageId)
        {
            var products = await _productService.GetAll(languageId);
            return Ok(products);
        }

        [HttpGet("product-paging/{languageId}")]
        public async Task<IActionResult> Get([FromQuery] ProductPagingRequest request)
        {
            var products = await _productService.GetAllByCategoryId(request);
            return Ok(products);
        }

        [HttpGet("{productId}/{languageId}")]
        public async Task<IActionResult> GetProductById(int productId, string languageId)
        {
            var product = await _adminProductService.GetProductById(productId, languageId);
            if (product == null)
                return NotFound("Không tìm thấy sản phẩm");
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ProductCreateRequest request)
        {
            var productId = await _adminProductService.Create(request);
            if (productId == 0)
                return BadRequest("Không thể tạo mới sản phẩm");
            var product = await _adminProductService.GetProductById(productId, request.LanguageId);
            return CreatedAtAction(nameof(GetProductById), new { id = productId }, product);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] ProductUpdateRequest request)
        {
            var result = await _adminProductService.Update(request);
            if (result == 0)
                return BadRequest("Không thể cập nhật sản phẩm");
            return Ok("Cập nhật sản phẩm thành công");
        }

        [HttpPut("price/{productId}/{newPrice}/{newOriginalPrice}")]
        public async Task<IActionResult> UpdatePrice([FromRoute] int productId, decimal newPrice, decimal newOriginalPrice)
        {
            var isSuccess = await _adminProductService.UpdatePrice(productId, newPrice, newOriginalPrice);
            if (!isSuccess)
                return BadRequest("Không thể cập nhật giá sản phẩm");
            return Ok("Cập nhật giá thành công");
        }

        [HttpPut("stock/{productId}/{addQuantity}")]
        public async Task<IActionResult> UpdateStock([FromRoute] int productId, int addQuantity)
        {
            var isSuccess = await _adminProductService.UpdateStock(productId, addQuantity);
            if (!isSuccess)
                return BadRequest("Không thể cập nhật số lượng sản phẩm");
            return Ok("Cập nhật số lượng thành công");
        }

        [HttpPut("viewcount/{productId}")]
        public async Task<IActionResult> AddViewCount([FromRoute] int productId)
        {
            try
            {
                await _adminProductService.AddViewCount(productId);
                return Ok("Tăng lượt view thành công");
            }
            catch (FShopException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Xảy ra lỗi khi cập nhật lượt xem");
            }
        }


        [HttpDelete("{productId}")]
        public async Task<IActionResult> Delete(int productId)
        {
            var result = await _adminProductService.Delete(productId);
            if (result == 0)
            {
                return BadRequest("Không thể xóa sản phẩm");
            }
            return Ok("Xóa sản phẩm thành công");
        }


    }
}
