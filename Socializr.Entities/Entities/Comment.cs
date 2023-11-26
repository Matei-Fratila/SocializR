namespace SocializR.Entities;

public partial class Comment : IEntity
{
    public string Id { get; set; }
    public string PostId { get; set; }
    public string UserId { get; set; }
    public string Body { get; set; }
    public DateTime CreatedOn { get; set; }

    public Post Post { get; set; }
    public User User { get; set; }
}
