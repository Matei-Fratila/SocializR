using AutoMapper;
using System.Net;

namespace SocializR.Web.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class FriendshipController(ApplicationUnitOfWork _unitOfWork,
    IOptionsMonitor<AppSettings> _configuration,
    CurrentUser _currentUser,
    IFriendshipService _friendshipService, 
    IFriendRequestService _friendRequestService, 
    IMapper _mapper) : ControllerBase
{

    [HttpPost("{userId}")]
    public async Task<IActionResult> AddFriendAsync([FromRoute]Guid userId)
    {
        _friendshipService.Create(userId, _currentUser.Id);

        if (!await _unitOfWork.SaveChangesAsync())
        {
            _friendRequestService.Delete(userId, _currentUser.Id);
        }

        return Ok();
    }

    [HttpDelete("{userId}")]
    public async Task<IActionResult> UnfriendAsync([FromRoute]Guid userId)
    {
        await _friendshipService.DeleteAsync(userId, _currentUser.Id);

        if (!await _unitOfWork.SaveChangesAsync())
        {
            return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
        }

        return Ok();
    }

    [HttpPost("/Friendrequest/{userId}")]
    public async Task<IActionResult> CreateAsync(Guid userId)
    {
        _friendRequestService.Add(new FriendRequest
        {
            RequestedUserId = userId,
            RequesterUserId = _currentUser.Id,
            CreatedOn = DateTime.UtcNow,
        });

        if (!await _unitOfWork.SaveChangesAsync())
        {
            return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
        }

        return Ok();
    }

    [HttpDelete("/Friendrequest/{userId}")]
    public async Task<IActionResult> DeleteAsync(Guid userId)
    {
        _friendRequestService.Delete(_currentUser.Id, userId);

        if (!await _unitOfWork.SaveChangesAsync())
        {
            return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
        }

        return Ok();
    }

}