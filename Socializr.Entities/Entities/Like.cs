namespace SocializR.Entities;

public partial class Like : IEntity
{
    public string PostId { get; set; }
    public string UserId { get; set; }

    public Post Post { get; set; }
    public User User { get; set; }
}
