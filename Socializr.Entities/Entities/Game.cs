namespace SocializR.Models.Entities;
public class Game 
{
    public int NumberOfHearts { get; set; }
    public long XP { get; set; }

    public virtual User User { get; set; }
}
