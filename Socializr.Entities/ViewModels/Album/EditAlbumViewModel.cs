using SocializR.Models.ViewModels.Media;

namespace SocializR.Models.ViewModels.Album;

public class EditAlbumViewModel 
{
    public bool IsDataInvalid { get; set; }

    public List<MediaViewModel> Media { get; set; }

    public string Id { get; set; }

    public string UserId { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
}
