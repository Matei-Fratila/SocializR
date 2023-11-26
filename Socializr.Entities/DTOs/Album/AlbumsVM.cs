using System;
using System.Collections.Generic;
using System.Text;

namespace SocializR.Entities.DTOs.Album
{
    public class AlbumsVM
    {
        public CreateAlbumVM NewAlbum { get; set; }

        public List<AlbumVM> Albums { get; set; }
    }
}
