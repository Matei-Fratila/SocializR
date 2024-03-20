using Quartz;

namespace SocializR.SPA.Server.Jobs;
public class ResetHeartsJob(IGameService gameService) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        await gameService.ResetHearts();
    }
}
