namespace SocializR.Models.ViewModels.Media;

public class MediaViewModel
{
    public MediaTypes Type { get; set; }
    public string Caption { get; set; }
    public Guid Id { get; set; }
    public string FileName { get; set; }
    public DateTime CreatedDate { get; set; }
    public bool IsCoverPhoto { get; set; }
}
