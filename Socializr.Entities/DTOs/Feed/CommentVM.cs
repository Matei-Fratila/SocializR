namespace SocializR.Entities.DTOs.Feed;

public class CommentVM
{
    public string Id { get; set; }
    public string UserId { get; set; }
    public bool IsCurrentUserComment { get; set; }
    public string UserPhoto { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Body { get; set; }
    public DateTime CreatedOn { get; set; }
}
