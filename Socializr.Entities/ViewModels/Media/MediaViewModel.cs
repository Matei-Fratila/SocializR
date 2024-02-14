namespace SocializR.Models.ViewModels.Media;

public class MediaViewModel
{
    public MediaTypes Type { get; set; }
    public string Caption { get; set; }
    public Guid Id { get; set; }
    public Guid AlbumId { get; set; }
    public string FileName { get; set; }
    public DateTime CreatedDate { get; set; }
    public string CreatedDateDisplay { get; set; }
    public DateTime LastModifiedDate { get; set; }
    public string LastModifiedDateDisplay { get; set; }
}
