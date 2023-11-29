namespace SocializR.Web.Controllers;

[Authorize]
public class FriendRequestController(IOptionsMonitor<AppSettings> _configuration,
    FriendRequestService _friendRequestService, 
    IMapper _mapper) : BaseController(_mapper)
{
    [HttpGet]
    public IActionResult Index(int? page)
    {
        var pageIndex = (page ?? 1) - 1;
        var requests = _friendRequestService.GetFriendRequests(pageIndex, _configuration.CurrentValue.UsersPerPage, out int totalUserCount);
        var model = new StaticPagedList<FriendrequestViewModel>(requests, pageIndex + 1, _configuration.CurrentValue.UsersPerPage, totalUserCount);

        return View(model);
    }

    [HttpPost]
    public IActionResult SendFriendRequest(Guid id)
    {
        var result = _friendRequestService.SendFriendRequest(id);
        if (!result)
        {
            return InternalServerErrorView();
        }

        return RedirectToAction("Get", "Profile", new { id });
    }

    [HttpPost]
    public IActionResult DeleteFriendRequest(Guid id)
    {
        var result = _friendRequestService.DeleteFriendRequest(id);
        if (!result)
        {
            return InternalServerErrorView();
        }

        return RedirectToAction("Get", "Profile", new { id });
    }
}