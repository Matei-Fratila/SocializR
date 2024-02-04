using Socializr.Models.Entities.Base;

namespace SocializR.Models.Entities;

public partial class Permission : BaseEntity, IEntity
{
    public Permission()
    {
        RolePermissions = new HashSet<RolePermission>();
    }

    public string Name { get; set; }
    public string Description { get; set; }

    public virtual ICollection<RolePermission> RolePermissions { get; set; }
}
