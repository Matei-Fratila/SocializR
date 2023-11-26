namespace SocializR.Entities;

public partial class RolePermission : IEntity
{
    public Guid Id { get; set; }
    public Guid PermissionId { get; set; }

    public virtual Permission Permission { get; set; }
    public virtual Role Role { get; set; }
}
