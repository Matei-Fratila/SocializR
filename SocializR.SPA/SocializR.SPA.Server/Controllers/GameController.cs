using Microsoft.EntityFrameworkCore;
using Socializr.Models.Entities;
using Socializr.Models.Enums;
using Socializr.Models.ViewModels.Game;

namespace SocializR.Web.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class GameController(IOptionsMonitor<AppSettings> _configuration,
    ApplicationUnitOfWork _unitOfWork) : ControllerBase()
{
    [HttpGet("start")]
    public async Task<IResult> StartGameAsync()
    {
        var currentUser = await _unitOfWork.Users.Query
            .Where(u => u.Email == User.Identity.Name)
            .Include(u => u.GameSessions)
            .Include(u => u.Game)
            .FirstOrDefaultAsync();

        if (currentUser.Game == null)
        {
            var newGame = new Game();
            newGame.XP = 0;
            newGame.NumberOfHearts = _configuration.CurrentValue.MaxNumberOfHearts;
            currentUser.Game = newGame;
        }

        var todaySession = currentUser.GameSessions.FirstOrDefault(s => s.CreatedDate.Date != DateTime.Today);

        if (todaySession == null)
        {
            todaySession = new GameSession();
            currentUser.GameSessions.Add(todaySession);
        }
        await _unitOfWork.SaveChangesAsync();

        return TypedResults.Ok(new StartGameViewModel
        {
            Score = todaySession.Score,
            MaxNumberOfHearts = _configuration.CurrentValue.MaxNumberOfHearts,
            UsersNumberOfHearts = currentUser.Game.NumberOfHearts,
        });
    }

    [HttpPost("submit-answer")]
    public async Task<IResult> SubmitAnswerAsync([FromBody] AnswerViewModel model)
    {
        var currentUser = await _unitOfWork.Users.Query
            .Where(u => u.Email == User.Identity.Name)
            .Include(u => u.GameSessions)
            .Include(u => u.Game)
            .FirstOrDefaultAsync();

        if (currentUser.Game.NumberOfHearts == 0)
        {
            return TypedResults.BadRequest("No more answers for today");
        }

        var session = currentUser.GameSessions.FirstOrDefault(s => s.CreatedDate.Date != DateTime.Today);
        if (session == null)
        {
            return TypedResults.BadRequest("Please start the game first");
        }

        session.Score += model.Score;
        currentUser.Game.XP += model.Score;

        if (!model.IsCorrect)
        {
            currentUser.Game.NumberOfHearts--;
        }

        switch (model.Difficulty)
        {
            case DifficultyTypes.Easy:
                {
                    if (model.IsCorrect)
                    {
                        session.NumberOfCorrectEasyQuestions++;
                    }
                    else
                    {
                        session.NumberOfIncorrectEasyQuestions++;
                    }
                    break;
                }
            case DifficultyTypes.Medium:
                {
                    if (model.IsCorrect)
                    {
                        session.NumberOfCorrectMediumQuestions++;
                    }
                    else
                    {
                        session.NumberOfIncorrectMediumQuestions++;
                    }
                    break;
                }
            case DifficultyTypes.Hard:
                {
                    if (model.IsCorrect)
                    {
                        session.NumberOfCorrectHardQuestions++;
                    }
                    else
                    {
                        session.NumberOfIncorrectHardQuestions++;
                    }
                    break;
                }
            case DifficultyTypes.Expert:
                {
                    if (model.IsCorrect)
                    {
                        session.NumberOfCorrectExpertQuestions++;
                    }
                    else
                    {
                        session.NumberOfIncorrectExpertQuestions++;
                    }
                    break;
                }
        };

        await _unitOfWork.SaveChangesAsync();

        // session.Score
        return TypedResults.Ok(currentUser.Game.NumberOfHearts);
    }
}
