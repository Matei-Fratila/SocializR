using Socializr.Models.Entities.Base;

namespace SocializR.Models.Entities;

public partial class City : BaseEntity, IEntity
{
    public City()
    {
        Users = new HashSet<User>();
    }

    public Guid CountyId { get; set; }
    public string Name { get; set; }

    public virtual County County { get; set; }
    public virtual ICollection<User> Users { get; set; }
}
