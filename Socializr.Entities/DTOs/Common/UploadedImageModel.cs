namespace SocializR.Entities.DTOs.Common;

public class UploadedImageModel
{
    public string Caption { get; set; }

    public IFormFile File { get; set; }
}
