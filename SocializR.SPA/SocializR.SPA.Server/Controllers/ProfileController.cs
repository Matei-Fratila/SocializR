using AutoMapper;
using SocializR.Models.Enums;
using SocializR.Models.ViewModels.Profile;
using System.Net;
using Utils;

namespace SocializR.SPA.Server.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class ProfileController(CurrentUser _currentUser,
    UserManager<User> _userManager,
    ApplicationUnitOfWork _unitOfWork,
    IPostService _postService,
    IProfileService _profileService,
    IFriendshipService _friendshipService,
    IOptionsMonitor<AppSettings> _appSettings,
    IImageStorage _imageStorage,
    IMediaService _mediaService,
    IHostEnvironment _hostingEnvironment,
    IAlbumService _albumService,
    IMapper _mapper) : ControllerBase
{
    private readonly string _defaultProfilePicture = _appSettings.CurrentValue.DefaultProfilePicture;
    private readonly string _defaultAlbumCover = _appSettings.CurrentValue.DefaultAlbumCover;
    private readonly string _profilePicturesAlbumName = _appSettings.CurrentValue.ProfilePicturesAlbumName;
    private readonly string _fileUploadLocation = _appSettings.CurrentValue.FileUploadLocation;
    private readonly int _postsPerPage = _appSettings.CurrentValue.PostsPerPage;
    private readonly int _commentsPerFirstPage = _appSettings.CurrentValue.CommentsPerFirstPage;

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<ActionResult<ViewProfileViewModel>> GetAsync([FromRoute]Guid id)
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

    [HttpPost]
    public async Task<ActionResult<ViewProfileViewModel>> EditAsync([FromForm]ProfileViewModel model)
    {
        var currentUser = await _userManager.GetUserAsync(User);
        var result = await _profileService.UpdateUser(model);
        var file = model.File;

        if (file != null && result)
        {
            var uploads = Path.Combine(_hostingEnvironment.ContentRootPath, _fileUploadLocation);
            var type = file.ContentType.ToString().Split('/');

            if (file.Length > 0)
            {
                if (type[0] == "image")
                {
                    var album = await _albumService.GetAsync(_profilePicturesAlbumName, model.Id);
                    if (album == null)
                    {
                        album = new Album { UserId = model.Id, Name = _profilePicturesAlbumName };
                        _albumService.Add(album);
                    }

                    try
                    {
                        var imageName = await _imageStorage.SaveImage(file.OpenReadStream(), type[1]);
                        var image = _mediaService.Add(imageName, MediaTypes.Image, album);

                        if (image == null)
                        {
                            return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
                        }

                        var hasModified = await _profileService.ChangeProfilePhoto(image.Id, model.Id);

                        if (hasModified)
                        {
                            _postService.Add(new Post
                            {
                                UserId = model.Id,
                                Title = "added a new profile photo",
                                Body = "",
                                CreatedOn = DateTime.Now,
                                Media = [image]
                            });
                        }

                        _unitOfWork.SaveChanges();

                    }
                    catch (Exception e)
                    {

                    }
                }
            }
        }

        if (!result)
        {
            return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
        }

        return await _profileService.GetViewProfileVM(model.Id);
    }
}
