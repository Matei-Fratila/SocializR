namespace SocializR.Web.Controllers;

[Authorize(Roles = "Administrator")]
public class UserController(IOptionsMonitor<AppSettings> _configuration, 
    IMapper _mapper, 
    IAdminService _adminService) : BaseController(_mapper)
{
    [HttpGet]
    public async Task<IActionResult> IndexAsync(int? page)
    {
        var pageIndex = (page ?? 1) - 1;

        var totalUsersCount = await _adminService.GetUsersCountAsync();
        var users = await _adminService.GetPaginatedUsersAsync(pageIndex, _configuration.CurrentValue.UsersPerPage);
        var model = new StaticPagedList<UserViewModel>(users, pageIndex + 1, _configuration.CurrentValue.UsersPerPage, totalUsersCount);

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var result = await _adminService.DeleteUserAsync(id);

        if (!result)
        {
            return ForbidView();
        }

        return RedirectToAction(nameof(Index), new { page = 1 });
    }

    [HttpPost]
    public async Task<IActionResult> ActivateAsync(Guid id)
    {
        var result = await _adminService.ActivateUserAsync(id);

        if (!result)
        {
            return ForbidView();
        }

        return RedirectToAction(nameof(Index), new { page = 1 });
    }
}