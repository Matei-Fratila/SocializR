namespace SocializR.Entities;

public partial class City : IEntity
{
    public City()
    {
        Users = new HashSet<User>();
    }

    public string Id { get; set; }
    public string CountyId { get; set; }
    public string Name { get; set; }

    public County County { get; set; }
    public ICollection<User> Users { get; set; }
}
