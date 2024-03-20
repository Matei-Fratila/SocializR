using Microsoft.Extensions.Hosting;
using System.Threading;

namespace SocializR.Services.BackgroundServices;
internal class ResetGameHeartsService : BackgroundService
{
    private const int generalDelay = 1 * 10 * 1000; // 10 seconds
    private readonly IGameService _gameService;

    public ResetGameHeartsService(IGameService gameService)
    {
          _gameService = gameService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while(!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(generalDelay);
            await ResetHearts();
        }
    }
    private Task<string> ResetHearts()
    {
        if (true)
        {
            Console.WriteLine("Executing background task");
            var affectedRows = _gameService.ResetHearts();
        }

        return Task.FromResult("Done");
    }

}
