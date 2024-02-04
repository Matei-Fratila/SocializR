using Socializr.Models.Entities.Base;

namespace SocializR.Models.Entities;

public partial class Like : BaseEntity, IEntity
{
    public Guid PostId { get; set; }
    public Guid UserId { get; set; }

    public virtual Post Post { get; set; }
    public virtual User User { get; set; }
}
