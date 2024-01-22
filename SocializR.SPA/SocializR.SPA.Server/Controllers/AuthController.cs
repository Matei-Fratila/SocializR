using AutoMapper;
using SocializR.Models.ViewModels.Account;

namespace SocializR.SPA.Server.Controllers;

[ApiController]
[AllowAnonymous]
[Route("[controller]")]
public class AuthController(UserManager<User> _userManager,
    TokenService _tokenService,
    IMapper _mapper,
    IAccountService _accountService) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login(LogInViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = await _userManager.FindByEmailAsync(model.Email);

        if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
        {
            return Unauthorized();
        }

        var roles = await _userManager.GetRolesAsync(user);
        var token = _tokenService.GenerateToken(user, roles);
        var currentUser = _accountService.GetCurrentUser(user.Email);

        return Ok(new { Token = token, CurrentUser = currentUser });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = _mapper.Map<User>(model);
        var result = await _userManager.CreateAsync(user, model.Password);

        if(result.Succeeded)
        {
            return Ok();
        }

        return BadRequest(result.Errors);
    }
}
