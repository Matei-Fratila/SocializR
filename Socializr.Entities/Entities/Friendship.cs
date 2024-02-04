using Socializr.Models.Entities.Base;

namespace SocializR.Models.Entities;

public partial class Friendship : BaseEntity, IEntity
{
    public Guid FirstUserId { get; set; }
    public Guid SecondUserId { get; set; }

    public virtual User FirstUser { get; set; }
    public virtual User SecondUser { get; set; }
}
