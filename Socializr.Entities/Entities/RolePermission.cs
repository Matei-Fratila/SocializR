using Socializr.Models.Entities.Base;

namespace SocializR.Models.Entities;

public partial class RolePermission : BaseEntity, IEntity
{
    public Guid PermissionId { get; set; }

    public virtual Permission Permission { get; set; }
    public virtual Role Role { get; set; }
}
