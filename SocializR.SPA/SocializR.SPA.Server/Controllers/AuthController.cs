﻿using SocializR.Models.ViewModels.Account;

namespace SocializR.SPA.Server.Controllers;

[ApiController]
[AllowAnonymous]
[Route("[controller]")]
public class AuthController(UserManager<User> _userManager,
    TokenService _tokenService,
    IAccountService _accountService) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login(LogInViewModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);

        if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
        {
            var roles = await _userManager.GetRolesAsync(user);
            var token = _tokenService.GenerateToken(user, roles);
            var currentUser = _accountService.GetCurrentUser(user.Email);

            return Ok(new { Token = token, CurrentUser = currentUser });
        }

        return Unauthorized();
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        var user = await _userManager.FindByNameAsync(model.Email);

        if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
        {
            var roles = await _userManager.GetRolesAsync(user);
            var token = _tokenService.GenerateToken(user, roles);
            var currentUser = _accountService.GetCurrentUser(user.Email);

            return Ok(new { Token = token, CurrentUser = currentUser });
        }

        return Unauthorized();
    }
}
