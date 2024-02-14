using Microsoft.AspNetCore.Http.HttpResults;
using SocializR.Models.Enums;
using SocializR.Models.ViewModels.Profile;

namespace SocializR.SPA.Server.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class ProfileController(CurrentUser _currentUser,
    UserManager<User> _userManager,
    IProfileService _profileService,
    IFriendshipService _friendshipService,
    IOptionsMonitor<AppSettings> _appSettings,
    IImageStorage _imageStorage) : ControllerBase
{
    private readonly string _defaultProfilePicture = _appSettings.CurrentValue.DefaultProfilePicture;
    private readonly string _defaultAlbumCover = _appSettings.CurrentValue.DefaultAlbumCover;

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<Results<NotFound, Ok<ProfileViewModel>>> GetAsync([FromRoute] Guid id)
    {
        var currentUser = await _userManager.GetUserAsync(User);

        if (id == Guid.Empty)
        {
            id = currentUser.Id;
        }

        var model = await _profileService.GetProfile(id);

        if (model == null)
        {
            return TypedResults.NotFound();
        }

        model.Avatar = _imageStorage.UriFor(model.Avatar ?? _defaultProfilePicture);
        foreach (var album in model.Albums)
        {
            album.CoverFilePath = _imageStorage.UriFor(album.CoverFilePath ?? _defaultAlbumCover);
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
        }

        return TypedResults.Ok(model);
    }

    [HttpPut]
    public async Task<IResult> UpdateAsync([FromForm] EditProfile model)
    {
        await _profileService.UpdateProfile(model);

        return Results.NoContent();
    }
}
