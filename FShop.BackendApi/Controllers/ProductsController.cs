using FShop.Dto.Images;
using FShop.Dto.Products.Admin;
using FShop.Dto.Products.Client;
using FShop.Service.Products;
using FShop.Utilities.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FShop.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController(IProductService _productService, IAdminProductService _adminProductService) : ControllerBase
    {
        [HttpGet("{languageId}")]
        public async Task<IActionResult> GetAllPaging(string languageId, [FromQuery] ProductPagingRequest request)
        {
            var products = await _productService.GetAllByCategoryId(languageId, request);
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

        [HttpGet("{productId}/images/{imageId}")]
        public async Task<IActionResult> GetImageById(int productId, int imageId)
        {
            var image = await _adminProductService.GetImageById(imageId);
            if (image == null)
                return NotFound("Không tìm thấy ảnh");
            return Ok(image);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ProductCreateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var productId = await _adminProductService.Create(request);
            if (productId == 0)
                return BadRequest("Không thể tạo mới sản phẩm");
            var product = await _adminProductService.GetProductById(productId, request.LanguageId);
            return CreatedAtAction(nameof(GetProductById), new { id = productId }, product);
        }

        [HttpPost("{productId}/images")]
        public async Task<IActionResult> CreateImage(int productId, [FromForm] ImageCreateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var imageId = await _adminProductService.AddImage(productId, request);
            if (imageId == 0)
                return BadRequest("Không thể tạo mới ảnh");
            var image = await _adminProductService.GetImageById(imageId);
            return CreatedAtAction(nameof(GetImageById), new { id = imageId }, image);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] ProductUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _adminProductService.Update(request);
            if (result == 0)
                return BadRequest("Không thể cập nhật sản phẩm");
            return Ok("Cập nhật sản phẩm thành công");
        }

        [HttpPut("{productId}/images/{imageId}")]
        public async Task<IActionResult> UpdateImage(int imageId, [FromForm] ImageUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _adminProductService.UpdateImage(imageId, request);
            if (result == 0)
                return BadRequest("Không thể cập nhật ảnh");
            return Ok("Cập nhật ảnh thành công");
        }

        [HttpPatch("{productId}/{newPrice}/{newOriginalPrice}")]
        public async Task<IActionResult> UpdatePrice([FromRoute] int productId, decimal newPrice, decimal newOriginalPrice)
        {
            var isSuccess = await _adminProductService.UpdatePrice(productId, newPrice, newOriginalPrice);
            if (!isSuccess)
                return BadRequest("Không thể cập nhật giá sản phẩm");
            return Ok("Cập nhật giá thành công");
        }

        [HttpPatch("{productId}/{addQuantity}")]
        public async Task<IActionResult> UpdateStock([FromRoute] int productId, int addQuantity)
        {
            var isSuccess = await _adminProductService.UpdateStock(productId, addQuantity);
            if (!isSuccess)
                return BadRequest("Không thể cập nhật số lượng sản phẩm");
            return Ok("Cập nhật số lượng thành công");
        }

        [HttpPatch("{productId}")]
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

        [HttpDelete("{productId}/images/{imageId}")]
        public async Task<IActionResult> DeleteImage(int imageId)
        {
            var result = await _adminProductService.DeleteImage(imageId);
            if (result == 0)
            {
                return BadRequest("Không thể xóa ảnh");
            }
            return Ok("Xóa ảnh thành công");
        }
    }
}