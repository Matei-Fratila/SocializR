using Microsoft.AspNetCore.Http;
using SocializR.Entities.DTOs.Album;
using System.Collections.Generic;

namespace SocializR.Entities.DTOs.Media
{
    public class ImagesVM
    {
        public EditAlbumVM Album { get; set; }

        public List<IFormFile> Files { get; set; }
    }
}
