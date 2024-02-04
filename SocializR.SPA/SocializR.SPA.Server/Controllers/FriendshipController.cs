using AutoMapper;
using Common.Interfaces;
using SocializR.Models.Entities;
using System.Net;

namespace SocializR.Web.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class FriendshipController(ApplicationUnitOfWork _unitOfWork,
    IOptionsMonitor<AppSettings> _configuration,
    CurrentUser _currentUser,
    IImageStorage _imageStorage,
    IFriendshipService _friendshipService, 
    IFriendRequestService _friendRequestService, 
    IMapper _mapper) : ControllerBase
{
    [HttpGet("{userId}")]
    [AllowAnonymous]
    public async Task<ActionResult<List<UserViewModel>>> GetAllAsync(Guid userId)
    {
        var friends = await _friendshipService.GetAllAsync(userId);

        foreach (var friend in friends)
        {
            friend.ProfilePhoto = _imageStorage.UriFor(friend.ProfilePhoto ?? _configuration.CurrentValue.DefaultProfilePicture);
        }

        return friends;
    }


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
            RequesterUserId = _currentUser.Id
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