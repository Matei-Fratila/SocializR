using SocializR.Entities.DTOs.Media;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SocializR.Entities.DTOs.Album
{
    public class EditAlbumVM 
    {
        public bool IsDataInvalid { get; set; }

        public List<MediaModel> Media { get; set; }

        public string Id { get; set; }

        public string UserId { get; set; }

        [Required(ErrorMessage = "Campul este obligatoriu!")]
        [MaxLength(100, ErrorMessage = "Campul trebuie sa aiba maximum 100 de caracatere")]
        public string Name { get; set; }
    }
}
