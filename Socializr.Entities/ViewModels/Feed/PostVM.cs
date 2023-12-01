namespace SocializR.Models.ViewModels.Feed;

public class PostVM
{
    public bool IsLikedByCurrentUser { get; set; }
    public bool IsCurrentUserPost { get; set; }

    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string UserPhoto { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public string CreatedOn { get; set; }
    public int NumberOfLikes { get; set; }
    public int NumberOfComments { get; set; }

    public List<MediaViewModel> Media { get; set; }
    public List<CommentViewModel> Comments { get; set; }
    public List<LikeViewModel> Likes { get; set; }
}
