using SocializR.Models.ViewModels.Album;

namespace SocializR.Models.ViewModels.Media;

public class ImagesViewModel
{
    public EditAlbumViewModel Album { get; set; }

    public List<IFormFile> Files { get; set; }
}
