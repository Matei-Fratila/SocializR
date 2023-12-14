namespace SocializR.SPA.Server.Controllers;

[ApiController]
[AllowAnonymous]
[Route("[controller]")]
public class AuthController(UserManager<User> _userManager,
    TokenService _tokenService) : ControllerBase
{
    [Authorize]
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto model)
    {
        var user = await _userManager.FindByNameAsync(model.UserName);

        if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
        {
            var roles = await _userManager.GetRolesAsync(user);
            var token = _tokenService.GenerateToken(user, roles);

            return Ok(new { Token = token });
        }

        return Unauthorized();
    }
}
