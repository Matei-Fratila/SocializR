using Common.Interfaces;

namespace SocializR.SPA.Server.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class UsersController(IAdminService _adminService,
    IImageStorage _imageStorage,
    IOptionsMonitor<AppSettings> _configuration) : ControllerBase
{
    [HttpGet, AllowAnonymous]
    public async Task<IResult> GetUsersAsync(string searchKey, int? pageIndex, int? pageSize)
    {
        var users = await _adminService.SearchUsersAsync(searchKey, pageIndex ?? 0, pageSize ?? 10);

        foreach (var user in users)
        {
            user.ProfilePhoto = _imageStorage.UriFor(user.ProfilePhoto ?? _configuration.CurrentValue.DefaultProfilePicture);
        }

        return Results.Ok(users);
    }
}
