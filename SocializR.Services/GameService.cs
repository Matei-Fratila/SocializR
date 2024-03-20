using Microsoft.Extensions.Options;

namespace SocializR.Services;
public class GameService(ApplicationUnitOfWork unitOfWork,
        IOptionsMonitor<AppSettings> _appSettings) : IGameService
{
    public async Task<int> ResetHearts()
    {
        var users = await unitOfWork.Users.Query
            .Where(u => u.Game.NumberOfHearts < _appSettings.CurrentValue.MaxNumberOfHearts)
            .ToListAsync();

        foreach (var user in users)
        {
            user.Game.NumberOfHearts = _appSettings.CurrentValue.MaxNumberOfHearts;
        }

        await unitOfWork.SaveChangesAsync();

        return users.Count;
    }
}
