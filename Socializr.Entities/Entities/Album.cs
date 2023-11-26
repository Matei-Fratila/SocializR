namespace SocializR.Entities;

public partial class Album : IEntity
{
    public Album()
    {
        Media = new HashSet<Media>();
    }

    public string Id { get; set; }
    public string UserId { get; set; }
    public string Name { get; set; }

    public User User { get; set; }
    public ICollection<Media> Media { get; set; }
}
