using AutoMapper;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Socializr.Models.ViewModels.Auth;
using SocializR.Models.ViewModels.Account;

namespace SocializR.SPA.Server.Controllers;

[ApiController]
[AllowAnonymous]
[Route("[controller]")]
public class AuthController(UserManager<User> _userManager,
    TokenService _tokenService,
    IOptionsMonitor<AppSettings> _appSettings,
    IOptionsMonitor<JwtSettings> _jwtSettings,
    IMapper _mapper,
    IImageStorage _imageStorage,
    IAccountService _accountService) : ControllerBase
{
    private readonly string _defaultProfilePicture = _appSettings.CurrentValue.DefaultProfilePicture;
    private readonly int _refreshTokenValidityInDays = _jwtSettings.CurrentValue.RefreshTokenValidityInDays;

    [EnableRateLimiting("ShortLimit")]
    [HttpPost("login")]
    public async Task<IResult> LoginAsync([FromBody] LogInViewModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);

        if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
        {
            return Results.Unauthorized();
        }

        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.UserName),
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
        };

        var roles = await _userManager.GetRolesAsync(user);
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var accessToken = _tokenService.GenerateAccessToken(claims);
        var refreshToken = _tokenService.GenerateRefreshToken();

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.Now.AddDays(_refreshTokenValidityInDays);
        await _userManager.UpdateAsync(user);

        var currentUser = await _accountService.GetCurrentUser(user.Email);
        currentUser.ProfilePhoto = _imageStorage.UriFor(currentUser.ProfilePhoto ?? _defaultProfilePicture);

        Response.Cookies.Append("X-Refresh-Token", refreshToken, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });

        return Results.Ok(new 
        { 
            Token = accessToken, 
            CurrentUser = currentUser 
        });
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

    [HttpPost]
    [Route("refresh")]
    public async Task<IResult> RefreshAsync([FromBody] RefreshTokenModel model)
    {
        if (model.AccessToken.IsNullOrEmpty())
        {
            return Results.BadRequest("Invalid client request");
        }

        Request.Cookies.TryGetValue("X-Refresh-Token", out var refreshToken);

        var principal = _tokenService.GetPrincipalFromExpiredToken(model.AccessToken);
        var username = principal.Identity.Name; 

        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == username);

        if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
        {
            return Results.BadRequest("Invalid client request");
        }

        var newAccessToken = _tokenService.GenerateAccessToken(principal.Claims);
        var newRefreshToken = _tokenService.GenerateRefreshToken();

        //Once a refresh token is used then it should be disposed
        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExpiryTime = DateTime.Now.AddDays(_refreshTokenValidityInDays);

        await _userManager.UpdateAsync(user);

        Response.Cookies.Append("X-Refresh-Token", newRefreshToken, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });

        return Results.Ok(newAccessToken);
    }

    [HttpPost, Authorize]
    [Route("revoke")]
    public async Task<IResult> RevokeAsync()
    {
        var username = User.Identity.Name;
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == username);

        if (user == null)
        {
            return Results.BadRequest();
        }

        user.RefreshToken = null;
        await _userManager.UpdateAsync(user);

        return Results.NoContent();
    }
}
