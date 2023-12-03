using Utils;

namespace SocializR.Web.Controllers;

[Authorize]
public class FriendshipController(IOptionsMonitor<AppSettings> _configuration,
    IFriendshipService _friendshipService, 
    IFriendRequestService _friendRequestService, 
    IMapper _mapper) : BaseController(_mapper)
{
    [HttpGet]
    public IActionResult Index(int? page)
    {
        var pageIndex = (page ?? 1) - 1;
        var friends = _friendshipService.GetFriends(pageIndex, _configuration.CurrentValue.UsersPerPage, out int totalUserCount);
        var model = new StaticPagedList<UserViewModel>(friends, pageIndex + 1, _configuration.CurrentValue.UsersPerPage, totalUserCount);

        return View(model);
    }

    [HttpPost]
    public IActionResult AddFriend(Guid id)
    {
        var result = _friendshipService.AddFriend(id);

        if (result)
        {
            _friendRequestService.DeleteFriendRequest(id);
        }

        return RedirectToAction(nameof(ProfileController.Index),
            nameof(ProfileController).RemoveControllerSuffix(),
            new { id });
    }

    [HttpPost]
    public IActionResult Unfriend(string id)
    {
        var result = _friendshipService.Unfriend(id);
        if (!result)
        {
            return InternalServerErrorView();
        }

        return RedirectToAction(nameof(ProfileController.Index),
            nameof(ProfileController).RemoveControllerSuffix(),
            new { id });
    }
}