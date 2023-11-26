namespace SocializR.Entities.DTOs.Feed;

public class AddPostModel
{
    public string Title { get; set; }
    public string Body { get; set; }
    public List<IFormFile> Media { get; set; }
}
