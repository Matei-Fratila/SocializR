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

    [HttpGet("{id}"), Authorize]
    public async Task<Results<NotFound, Ok<ProfileViewModel>>> GetAsync([FromRoute] Guid id)
    {
        var authorizedUserId = new Guid(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

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

        if (id != authorizedUserId)
        {
            model.MutualFriends = await _friendshipService.GetMutualFriendsCountAsync(id, _currentUser.Id);
        }

        model.RelationToCurrentUser = _profileService.GetRelationToCurrentUser(authorizedUserId, id);

        if (model.IsPrivate
            && model.RelationToCurrentUser == RelationTypes.Strangers
            && await _userManager.IsInRoleAsync(await _userManager.FindByIdAsync(authorizedUserId.ToString()), "Administrator") == false)
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

    [HttpGet("avatar/{id}")]
    [AllowAnonymous]
    public async Task<IResult> GetAvatar([FromRoute] Guid id)
    {
        var profile = await _profileService.GetProfile(id);
        var result = _imageStorage.UriFor(profile?.Avatar);

        return Results.Ok(result);
    }
}
