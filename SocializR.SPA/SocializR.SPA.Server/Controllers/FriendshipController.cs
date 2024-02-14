namespace SocializR.Web.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class FriendshipController(ApplicationUnitOfWork _unitOfWork,
    IOptionsMonitor<AppSettings> _configuration,
    CurrentUser _currentUser,
    IImageStorage _imageStorage,
    IFriendshipService _friendshipService, 
    IFriendRequestService _friendRequestService) : ControllerBase
{
    [HttpGet("{userId}")]
    [AllowAnonymous]
    public async Task<IResult> GetAllAsync([FromRoute] Guid userId)
    {
        var friends = await _friendshipService.GetAllAsync(userId);

        foreach (var friend in friends)
        {
            friend.ProfilePhoto = _imageStorage.UriFor(friend.ProfilePhoto ?? _configuration.CurrentValue.DefaultProfilePicture);
        }

        return Results.Ok(friends);
    }

    [HttpPost("{userId}")]
    public async Task<IResult> AddFriendAsync([FromRoute] Guid userId)
    {
        _friendshipService.Create(userId, _currentUser.Id);
        await _unitOfWork.SaveChangesAsync();

        return Results.Created();
    }

    [HttpDelete("{userId}")]
    public async Task<IResult> UnfriendAsync([FromRoute] Guid userId)
    {
        await _friendshipService.DeleteAsync(userId, _currentUser.Id);
        await _unitOfWork.SaveChangesAsync();

        return Results.NoContent();
    }

    [HttpPost("/Friendrequest/{userId}")]
    public async Task<IResult> CreateAsync([FromRoute] Guid userId)
    {
        _friendRequestService.Add(new FriendRequest
        {
            RequestedUserId = userId,
            RequesterUserId = _currentUser.Id
        });

        await _unitOfWork.SaveChangesAsync();

        return Results.Created();
    }

    [HttpDelete("/Friendrequest/{userId}")]
    public async Task<IResult> DeleteAsync([FromRoute] Guid userId)
    {
        _friendRequestService.Delete(_currentUser.Id, userId);
        await _unitOfWork.SaveChangesAsync();

        return Results.NoContent();
    }

}