using Socializr.Models.Entities.Base;

namespace SocializR.Models.Entities;

public partial class County : BaseEntity, IEntity
{
    public County()
    {
        Cities = new HashSet<City>();
    }

    public string Name { get; set; }
    public string ShortName { get; set; }

    public virtual ICollection<City> Cities { get; set; }
}
