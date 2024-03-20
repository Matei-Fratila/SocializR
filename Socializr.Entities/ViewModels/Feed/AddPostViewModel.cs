namespace SocializR.Models.ViewModels.Feed;

public class AddPostViewModel
{
    public string Title { get; set; }
    public string Body { get; set; }
    public IFormFile[] Media { get; set; }
}
