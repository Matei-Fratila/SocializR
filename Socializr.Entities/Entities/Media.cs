namespace SocializR.Entities;

public partial class Media : IEntity
{
    public Media()
    {
        Users = new HashSet<User>();
    }

    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid AlbumId { get; set; }
    public Guid PostId { get; set; }
    public string Caption { get; set; }
    public string FilePath { get; set; }
    public MediaTypes Type { get; set; }

    public virtual Album Album { get; set; }
    public virtual Post Post { get; set; }
    public virtual User User { get; set; }
    public virtual ICollection<User> Users { get; set; }
}
