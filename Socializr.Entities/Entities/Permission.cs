namespace SocializR.Entities;

public partial class Permission : IEntity
{
    public Permission()
    {
        RolePermissions = new HashSet<RolePermission>();
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public ICollection<RolePermission> RolePermissions { get; set; }
}
