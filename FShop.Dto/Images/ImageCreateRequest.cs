using Microsoft.AspNetCore.Http;

namespace FShop.Dto.Images
{
    public class ImageCreateRequest
    {
        public string Caption { get; set; }
        public bool IsDefault { get; set; }
        public int SortOrder { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}