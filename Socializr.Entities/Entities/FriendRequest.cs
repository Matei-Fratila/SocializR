using Socializr.Models.Entities.Base;

namespace SocializR.Models.Entities;

public partial class FriendRequest : BaseEntity, IEntity
{
    public Guid RequesterUserId { get; set; }
    public Guid RequestedUserId { get; set; }

    public virtual User RequestedUser { get; set; }
    public virtual User RequesterUser { get; set; }
}
