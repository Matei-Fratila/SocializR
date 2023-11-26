namespace SocializR.Entities;

public partial class RolePermission : IEntity
{
    public int Id { get; set; }
    public int PermissionId { get; set; }

    public Permission Permission { get; set; }
    public Role Role { get; set; }
}
