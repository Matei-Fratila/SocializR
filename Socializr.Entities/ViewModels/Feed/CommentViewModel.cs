namespace SocializR.Models.ViewModels.Feed;

public class CommentViewModel
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public bool IsCurrentUserComment { get; set; }
    public string UserPhoto { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Body { get; set; }
    public string CreatedOn { get; set; }
}
