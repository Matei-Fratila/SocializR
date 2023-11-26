namespace SocializR.Entities;

public partial class Role : IdentityRole, IEntity
{
    public Role()
    {
    }

    public string Description { get; set; }
}
