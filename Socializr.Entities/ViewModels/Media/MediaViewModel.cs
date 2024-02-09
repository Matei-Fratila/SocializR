namespace SocializR.Models.ViewModels.Media;

public class MediaViewModel
{
    public MediaTypes Type { get; set; }
    public string Caption { get; set; }
    public Guid Id { get; set; }
    public Guid AlbumId { get; set; }
    public string FileName { get; set; }
    public string CreatedDate { get; set; }
    public string LastModifiedDate { get; set; }
    public bool IsCoverPhoto { get; set; }
}
