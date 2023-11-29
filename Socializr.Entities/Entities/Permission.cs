namespace SocializR.Models.Entities;

public partial class Permission : IEntity
{
    public Permission()
    {
        RolePermissions = new HashSet<RolePermission>();
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public virtual ICollection<RolePermission> RolePermissions { get; set; }
}
