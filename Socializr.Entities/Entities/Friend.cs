namespace SocializR.Entities;

public partial class Friend : IEntity
{
    public string SenderId { get; set; }
    public string ReceiverId { get; set; }
    public DateTime CreatedDate { get; set; }

    public User Sender{ get; set; }
    public User Receiver { get; set; }
}
