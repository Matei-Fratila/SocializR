using SocializR.Entities.DTOs.Media;
using System.Collections.Generic;

namespace SocializR.Entities.DTOs.Album
{
    public class AlbumVM
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string CoverId { get; set; }

        public int NrOfImages { get; set; }
    }
}
