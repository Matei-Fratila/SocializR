namespace SocializR.Models.Entities;

public partial class Role : IdentityRole<Guid>, IEntity
{
    public Role()
    {
    }

    public string Name { get; set; }
    public string Description { get; set; }
}
