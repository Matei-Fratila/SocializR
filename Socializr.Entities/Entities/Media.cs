namespace SocializR.Entities;

public partial class Media : IEntity
{
    public Media()
    {
        Users = new HashSet<User>();
    }

    public string Id { get; set; }
    public string UserId { get; set; }
    public string AlbumId { get; set; }
    public string PostId { get; set; }
    public string Caption { get; set; }
    public string FilePath { get; set; }

    public Album Album { get; set; }
    public MediaTypes Type { get; set; }
    public Post Post { get; set; }
    public User User { get; set; }
    public ICollection<User> Users { get; set; }
}
