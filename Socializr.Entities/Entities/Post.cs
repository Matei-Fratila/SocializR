namespace SocializR.Entities;

public partial class Post : IEntity
{
    public Post()
    {
        Comments = new HashSet<Comment>();
        Likes = new HashSet<Like>();
        Media = new HashSet<Media>();
    }

    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public DateTime CreatedOn { get; set; }
    public Guid UserId { get; set; }

    public virtual User User { get; set; }
    public virtual ICollection<Comment> Comments { get; set; }
    public virtual ICollection<Like> Likes { get; set; }
    public virtual ICollection<Media> Media { get; set; }
}
