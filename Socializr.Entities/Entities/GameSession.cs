using Socializr.Models.Entities.Base;
using Socializr.Models.Enums;
using SocializR.Models.Entities;

namespace Socializr.Models.Entities;
public class GameSession : BaseEntity
{
    public GameTypes GameType { get; set; }
    public int Score { get; set; }

    public int NumberOfCorrectEasyQuestions { get; set; }
    public int NumberOfCorrectMediumQuestions { get; set; }
    public int NumberOfCorrectHardQuestions { get; set; }
    public int NumberOfCorrectExpertQuestions { get; set; }

    public int NumberOfIncorrectEasyQuestions { get; set; }
    public int NumberOfIncorrectMediumQuestions { get; set; }
    public int NumberOfIncorrectHardQuestions { get; set; }
    public int NumberOfIncorrectExpertQuestions { get; set; }

    public Guid UserId { get; set; }
    public virtual User User { get; set; }
}
