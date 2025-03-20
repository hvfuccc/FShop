using Microsoft.AspNetCore.Http;

namespace FShop.Dto.Images
{
    public class ImageUpdateRequest
    {
        public int Id { get; set; }
        public string Caption { get; set; }
        public bool IsDefault { get; set; }
        public int SortOrder { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}