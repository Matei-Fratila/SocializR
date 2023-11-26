namespace SocializR.Web.Controllers;

[Authorize]
public class FriendRequestController : BaseController
{
    private readonly FriendshipService friendshipService;
    private readonly FriendRequestService friendRequestService;
    private readonly AppSettings configuration;

    public FriendRequestController(IOptions<AppSettings> configuration, FriendshipService friendshipService, FriendRequestService friendRequestService, IMapper mapper)
         : base(mapper)
    {
        this.friendshipService = friendshipService;
        this.friendRequestService = friendRequestService;
        this.configuration = configuration.Value;
    }

    [HttpGet]
    public IActionResult Index(int? page)
    {
        var pageIndex = (page ?? 1) - 1;
        var requests = friendRequestService.GetFriendRequests(pageIndex, configuration.UsersPerPage, out int totalUserCount);
        var model = new StaticPagedList<FriendrequestVM>(requests, pageIndex + 1, configuration.UsersPerPage, totalUserCount);

        return View(model);
    }

    [HttpPost]
    public IActionResult SendFriendRequest(string id)
    {
        var result = friendRequestService.SendFriendRequest(id);
        if (!result)
        {
            return InternalServerErrorView();
        }

        return RedirectToAction("Get", "Profile", new { id });
    }

    [HttpPost]
    public IActionResult DeleteFriendRequest(string id)
    {
        var result = friendRequestService.DeleteFriendRequest(id);
        if (!result)
        {
            return InternalServerErrorView();
        }

        return RedirectToAction("Get", "Profile", new { id });
    }
}