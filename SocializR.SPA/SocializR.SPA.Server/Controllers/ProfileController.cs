using AutoMapper;
using SocializR.Models.Enums;
using SocializR.Models.ViewModels.Profile;
using System.Net;

namespace SocializR.SPA.Server.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class ProfileController(CurrentUser _currentUser,
    UserManager<User> _userManager,
    IProfileService _profileService,
    IInterestService _interestService,
    IFriendshipService _friendshipService,
    IOptionsMonitor<AppSettings> _appSettings,
    IImageStorage _imageStorage,
    IMapper _mapper) : ControllerBase
{
    private readonly string _defaultProfilePicture = _appSettings.CurrentValue.DefaultProfilePicture;
    private readonly string _defaultAlbumCover = _appSettings.CurrentValue.DefaultAlbumCover;

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<ActionResult<ViewProfileViewModel>> IndexAsync([FromRoute]Guid id)
    {
        var currentUser = await _userManager.GetUserAsync(User);

        if (id == Guid.Empty)
        {
            id = currentUser.Id;
        }

        var model = await _profileService.GetViewProfileVM(id);

        model.UserPhoto = _imageStorage.UriFor(model.UserPhoto ?? _defaultProfilePicture);
        foreach (var album in model.Albums)
        {
            album.CoverFilePath = _imageStorage.UriFor(album.CoverFilePath ?? _defaultAlbumCover);
        }

        foreach (var media in model.Posts.SelectMany(p => p.Media))
        {
            media.FileName = _imageStorage.UriFor(media.FileName);
        }
        if (model == null)
        {
            return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
        }

        if (id != currentUser?.Id)
        {
            model.MutualFriends = await _friendshipService.GetMutualFriendsCountAsync(id, _currentUser.Id);
        }

        model.RelationToCurrentUser = _profileService.GetRelationToCurrentUser(currentUser?.Id, id);
        if (model.IsPrivate
            && model.RelationToCurrentUser == RelationTypes.Strangers
            && await _userManager.IsInRoleAsync(currentUser, "Administrator") == false)
        {
            model.Albums = null;
            model.Interests = null;
            return model;
        }

        return model;
    }
}
