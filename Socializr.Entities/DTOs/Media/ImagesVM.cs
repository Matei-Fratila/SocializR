namespace SocializR.Entities.DTOs.Media;

public class ImagesVM
{
    public EditAlbumVM Album { get; set; }

    public List<IFormFile> Files { get; set; }
}
