namespace SocializR.Entities;

public partial class Role : IdentityRole<Guid>, IEntity
{
    public Role()
    {
    }

    public string Description { get; set; }
}
