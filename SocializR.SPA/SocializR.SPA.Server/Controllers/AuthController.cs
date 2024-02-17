﻿using AutoMapper;
using Microsoft.AspNetCore.RateLimiting;
using SocializR.Models.ViewModels.Account;

namespace SocializR.SPA.Server.Controllers;

[ApiController]
[AllowAnonymous]
[Route("[controller]")]
public class AuthController(UserManager<User> _userManager,
    TokenService _tokenService,
    IOptionsMonitor<AppSettings> _appSettings,
    IMapper _mapper,
    IImageStorage _imageStorage,
    IAccountService _accountService) : ControllerBase
{
    private readonly string _defaultProfilePicture = _appSettings.CurrentValue.DefaultProfilePicture;

    [EnableRateLimiting("ShortLimit")]
    [HttpPost("login")]
    public async Task<IResult> LoginAsync([FromBody] LogInViewModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);

        if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
        {
            return Results.Unauthorized();
        }

        var roles = await _userManager.GetRolesAsync(user);
        var token = _tokenService.GenerateToken(user, roles);
        var currentUser = await _accountService.GetCurrentUser(user.Email);

        currentUser.ProfilePhoto = _imageStorage.UriFor(currentUser.ProfilePhoto ?? _defaultProfilePicture);

        return Results.Ok(new { Token = token, CurrentUser = currentUser });
    }

    [HttpPost("register")]
    public async Task<IResult> RegisterAsync([FromBody] RegisterViewModel model)
    {
        var user = _mapper.Map<User>(model);
        var result = await _userManager.CreateAsync(user, model.Password);

        if(result.Succeeded)
        {
            return Results.Ok();
        }

        return Results.BadRequest(result.Errors);
    }
}
