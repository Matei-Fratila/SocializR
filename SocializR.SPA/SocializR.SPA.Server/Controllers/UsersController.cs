namespace SocializR.SPA.Server.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class UsersController(IAdminService _adminService) : ControllerBase
{
    [HttpGet, AllowAnonymous]
    public async Task<IResult> GetUsersAsync(string searchKey, int? pageIndex, int? pageSize)
    {
        var usersResult = await _adminService.SearchUsersAsync(searchKey, pageIndex ?? 0, pageSize ?? 10);

        return Results.Ok(usersResult);
    }
}
