namespace SocializR.Entities;

public partial class UserInterest : IEntity
{
    public string UserId { get; set; }
    public string InterestId { get; set; }

    public Interest Interest { get; set; }
    public User User { get; set; }
}
