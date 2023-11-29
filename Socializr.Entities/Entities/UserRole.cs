namespace SocializR.Models.Entities;

public partial class UserRole : IEntity
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }

    public virtual Role Role { get; set; }
    public virtual User User { get; set; }
}
