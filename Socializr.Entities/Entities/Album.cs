using Socializr.Models.Entities.Base;

namespace SocializR.Models.Entities;

public partial class Album : BaseEntity, IEntity
{
    public Album()
    {
        Media = new HashSet<Media>();
    }

    public Guid UserId { get; set; }
    public string Name { get; set; }
    public string Caption { get; set; }

    public virtual User User { get; set; }
    public virtual ICollection<Media> Media { get; set; }
}
