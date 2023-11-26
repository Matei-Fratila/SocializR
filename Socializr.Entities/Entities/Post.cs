namespace SocializR.Entities;

public partial class Post : IEntity
{
    public Post()
    {
        Comments = new HashSet<Comment>();
        Likes = new HashSet<Like>();
        Media = new HashSet<Media>();
    }

    public string Id { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public DateTime CreatedOn { get; set; }
    public string UserId { get; set; }

    public User User { get; set; }
    public ICollection<Comment> Comments { get; set; }
    public ICollection<Like> Likes { get; set; }
    public ICollection<Media> Media { get; set; }
}
