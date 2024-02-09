namespace SocializR.Models.Entities;

public partial class Role : IdentityRole<Guid>, IEntity
{
    public Role()
    {
    }

    public string Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? LastModifiedDate { get; set; }
    public bool IsDeleted { get; set; }
    public byte[] TimeStamp { get; set; }
}
