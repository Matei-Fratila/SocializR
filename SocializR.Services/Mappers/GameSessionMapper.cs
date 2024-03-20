using Socializr.Models.Entities;
using Socializr.Models.ViewModels.Game;

namespace SocializR.Services.Mappers;
public class GameSessionMapper : Profile
{
    public GameSessionMapper()
    {
        CreateMap<AnswerViewModel, GameSession>();
    }
}
