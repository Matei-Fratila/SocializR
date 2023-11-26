using Microsoft.AspNetCore.Http;

namespace SocializR.Entities.DTOs.Media
{
    public class ImageVM
    {
        public string Caption { get; set; }

        public IFormFile Content { get; set; }
    }
}
