using SocializR.Services.Interfaces;

namespace SocializR.SPA.Server.Controllers;

[Authorize]
[ApiController]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly IAccountService _accountService;

    public UserController(ILogger<UserController> logger, IAccountService accountService)
    {
        _logger = logger;
        _accountService = accountService;
    }
}
