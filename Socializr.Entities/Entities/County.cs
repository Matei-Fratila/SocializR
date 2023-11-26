namespace SocializR.Entities;

public partial class County : IEntity
{
    public County()
    {
        Cities = new HashSet<City>();
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public string ShortName { get; set; }

    public virtual ICollection<City> Cities { get; set; }
}
