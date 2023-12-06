namespace SocializR.Web.Controllers;

public class FriendshipController(ApplicationUnitOfWork _unitOfWork,
    IOptionsMonitor<AppSettings> _configuration,
    CurrentUser _currentUser,
    IFriendshipService _friendshipService, 
    IFriendRequestService _friendRequestService, 
    IMapper _mapper) : BaseController(_mapper)
{
    [HttpGet]
    public async Task<IActionResult> IndexAsync(int? page)
    {
        var pageIndex = (page ?? 1) - 1;
        var totalUserCount = await _friendshipService.GetCountAsync(_currentUser.Id);
        var friends = await _friendshipService.GetPaginatedAsync(_currentUser.Id, pageIndex, _configuration.CurrentValue.UsersPerPage);
        var model = new StaticPagedList<UserViewModel>(friends, pageIndex + 1, _configuration.CurrentValue.UsersPerPage, totalUserCount);

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> AddFriendAsync(Guid id)
    {
        _friendshipService.Create(id, _currentUser.Id);

        if (!await _unitOfWork.SaveChangesAsync())
        {
            _friendRequestService.Delete(id, _currentUser.Id);
        }

        return RedirectToAction(nameof(ProfileController.IndexAsync).RemoveAsyncSuffix(),
            nameof(ProfileController).RemoveControllerSuffix(),
            new { id });
    }

    [HttpPost]
    public async Task<IActionResult> UnfriendAsync(Guid id)
    {
        await _friendshipService.DeleteAsync(id, _currentUser.Id);

        if (!await _unitOfWork.SaveChangesAsync())
        {
            return InternalServerErrorView();
        }

        return RedirectToAction(nameof(ProfileController.IndexAsync).RemoveAsyncSuffix(),
            nameof(ProfileController).RemoveControllerSuffix(),
            new { id });
    }
}