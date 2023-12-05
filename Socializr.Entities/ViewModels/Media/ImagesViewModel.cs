namespace SocializR.Models.ViewModels.Media;

public class ImagesViewModel
{
    public AlbumViewModel Album { get; set; }

    public List<IFormFile> Files { get; set; }
}
