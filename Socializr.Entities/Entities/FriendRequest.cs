namespace SocializR.Models.Entities;

public partial class FriendRequest : IEntity
{
    public Guid RequesterUserId { get; set; }
    public Guid RequestedUserId { get; set; }
    public string RequestMessage { get; set; }
    public DateTime CreatedOn { get; set; }

    public virtual User RequestedUser { get; set; }
    public virtual User RequesterUser { get; set; }
}
