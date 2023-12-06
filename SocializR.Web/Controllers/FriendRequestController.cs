namespace SocializR.Web.Controllers;

public class FriendRequestController(ApplicationUnitOfWork _unitOfWork,
    IOptionsMonitor<AppSettings> _appSettings,
    CurrentUser _currentUser,
    IFriendRequestService _friendRequestService, 
    IMapper _mapper) : BaseController(_mapper)
{
    private readonly int _usersPerPage = _appSettings.CurrentValue.UsersPerPage;

    [HttpGet]
    public async Task<IActionResult> IndexAsync(int? page)
    {
        var pageIndex = (page ?? 1) - 1;
        var totalUserCount = await _friendRequestService.GetCountAsync(_currentUser.Id);
        var requests = await _friendRequestService.GetPaginatedAsync(_currentUser.Id, pageIndex, _usersPerPage);
        var model = new StaticPagedList<FriendrequestViewModel>(requests, pageIndex + 1, _usersPerPage, totalUserCount);

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(Guid id)
    {
        _friendRequestService.Add(new FriendRequest
        {
            RequestedUserId = id,
            RequesterUserId = _currentUser.Id,
            CreatedOn = DateTime.UtcNow,
        });

        if (!await _unitOfWork.SaveChangesAsync())
        {
            return InternalServerErrorView();
        }

        return RedirectToAction(nameof(ProfileController.IndexAsync).RemoveAsyncSuffix(), 
            nameof(ProfileController).RemoveControllerSuffix(), 
            new { id });
    }

    [HttpPost]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        _friendRequestService.Delete(_currentUser.Id, id);

        if (!await _unitOfWork.SaveChangesAsync())
        {
            return InternalServerErrorView();
        }

        return RedirectToAction(nameof(ProfileController.IndexAsync).RemoveAsyncSuffix(),
            nameof(ProfileController).RemoveControllerSuffix(),
            new { id });
    }
}