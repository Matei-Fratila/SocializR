namespace SocializR.Models.Entities;

public partial class UserInterest : IEntity
{
    public Guid UserId { get; set; }
    public Guid InterestId { get; set; }

    public virtual Interest Interest { get; set; }
    public virtual User User { get; set; }
}
