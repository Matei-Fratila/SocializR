using Socializr.Models.Entities.Base;

namespace SocializR.Models.Entities;

public partial class Media : BaseEntity, IEntity
{
    public Media()
    {
        Users = new HashSet<User>();
    }

    public Guid UserId { get; set; }
    public Guid AlbumId { get; set; }
    public Guid? PostId { get; set; }
    public string Caption { get; set; }
    public string FileName { get; set; }
    public MediaTypes Type { get; set; }

    public virtual Album Album { get; set; }
    public virtual Post? Post { get; set; }
    public virtual User User { get; set; }
    public virtual ICollection<User> Users { get; set; }
}
