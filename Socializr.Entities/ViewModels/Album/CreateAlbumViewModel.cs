namespace Socializr.Models.ViewModels.Album;
public class CreateAlbumViewModel
{
    [Required]
    public string Name { get; set; }
    public string[] Captions { get; set; }
    public IFormFile[] Files { get; set; }
}
