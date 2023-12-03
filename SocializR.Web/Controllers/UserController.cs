namespace SocializR.Web.Controllers;

[Authorize(Roles = "Administrator")]
public class UserController(IOptionsMonitor<AppSettings> _configuration, 
    IMapper _mapper, 
    IAdminService _userAdminService) : BaseController(_mapper)
{
    [HttpGet]
    public IActionResult Index(int? page)
    {
        var pageIndex = (page ?? 1) - 1;

        var users = _userAdminService.GetAllUsers(pageIndex, _configuration.CurrentValue.UsersPerPage, out int totalUserCount);
        var model = new StaticPagedList<UserViewModel>(users, pageIndex + 1, _configuration.CurrentValue.UsersPerPage, totalUserCount);

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteAsync(string id)
    {
        var result = await _userAdminService.DeleteUser(id);

        if (!result)
        {
            return ForbidView();
        }

        return RedirectToAction(nameof(Index), new { page = 1 });
    }

    [HttpPost]
    public async Task<IActionResult> Activate(string id)
    {
        var result = await _userAdminService.ActivateUser(id);

        if (!result)
        {
            return ForbidView();
        }

        return RedirectToAction(nameof(Index), new { page = 1 });
    }
}