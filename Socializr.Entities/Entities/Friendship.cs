namespace SocializR.Entities;

public partial class Friendship : IEntity
{
    public string FirstUserId { get; set; }
    public string SecondUserId { get; set; }
    public DateTime CreatedDate { get; set; }

    public User FirstUser { get; set; }
    public User SecondUser { get; set; }
}
