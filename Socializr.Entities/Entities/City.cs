namespace SocializR.Models.Entities;

public partial class City : IEntity
{
    public City()
    {
        Users = new HashSet<User>();
    }

    public Guid Id { get; set; }
    public Guid CountyId { get; set; }
    public string Name { get; set; }

    public virtual County County { get; set; }
    public virtual ICollection<User> Users { get; set; }
}
