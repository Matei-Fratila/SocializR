using Socializr.Models.Entities.Base;

namespace SocializR.Models.Entities;

public partial class Interest : BaseEntity, IEntity
{
    public Interest()
    {
        ChildInterests = new HashSet<Interest>();
        UserInterests = new HashSet<UserInterest>();
    }

    public Guid? ParentId { get; set; }
    public string Name { get; set; }

    public virtual Interest Parent { get; set; }
    public virtual ICollection<Interest> ChildInterests { get; set; }
    public virtual ICollection<UserInterest> UserInterests { get; set; }
}
