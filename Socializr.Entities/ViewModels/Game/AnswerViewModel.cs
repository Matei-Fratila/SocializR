using Socializr.Models.Enums;

namespace Socializr.Models.ViewModels.Game;
public class AnswerViewModel
{
    public DifficultyTypes Difficulty {  get; set; }
    public bool IsCorrect { get; set; }
    public int Score { get; set; }
}
