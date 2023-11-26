namespace SocializR.Entities;

public partial class Friend : IEntity
{
    public Guid SenderId { get; set; }
    public Guid ReceiverId { get; set; }
    public DateTime CreatedDate { get; set; }

    public virtual User Sender{ get; set; }
    public virtual User Receiver { get; set; }
}
