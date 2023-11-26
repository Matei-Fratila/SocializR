namespace SocializR.Entities;

public partial class Friendship : IEntity
{
    public Guid FirstUserId { get; set; }
    public Guid SecondUserId { get; set; }
    public DateTime CreatedDate { get; set; }

    public virtual User FirstUser { get; set; }
    public virtual User SecondUser { get; set; }
}
