namespace SocializR.Web.Controllers;

public class ProfileController(CurrentUser _currentUser,
    UserManager<User> _userManager,
    ApplicationUnitOfWork _unitOfWork,
    IProfileService _profileService,
    ICountyService _countyService,
    ICityService _cityService,
    IInterestService _interestService,
    IMediaService _mediaService,
    IAlbumService _albumService,
    IFriendshipService _friendshipService,
    IPostService _postService,
    IOptionsMonitor<AppSettings> _appSettings,
    IHostEnvironment _hostingEnvironment,
    IImageStorage _imageStorage,
    IMapper _mapper) : BaseController(_mapper)
{
    private readonly string _profilePicturesAlbumName = _appSettings.CurrentValue.ProfilePicturesAlbumName;
    private readonly string _defaultProfilePicture = _appSettings.CurrentValue.DefaultProfilePicture;
    private readonly string _defaultAlbumCover = _appSettings.CurrentValue.DefaultAlbumCover;
    private readonly string _fileUploadLocation = _appSettings.CurrentValue.FileUploadLocation;
    private readonly int _postsPerPage = _appSettings.CurrentValue.PostsPerPage;
    private readonly int _commentsPerFirstPage = _appSettings.CurrentValue.CommentsPerFirstPage;

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> IndexAsync(Guid id)
    {
        var currentUser = await _userManager.GetUserAsync(User);

        if (id == Guid.Empty)
        {
            id = currentUser.Id;
        }

        var model = await _profileService.GetViewProfileVM(id);

        model.FilePath = _imageStorage.UriFor(model.FilePath ?? _defaultProfilePicture);
        foreach (var album in model.Albums)
        {
            album.CoverFilePath = _imageStorage.UriFor(album.CoverFilePath ?? _defaultAlbumCover);
        }

        model.Posts = await _postService.GetPaginatedAsync(id, 0, _postsPerPage, _commentsPerFirstPage, _defaultProfilePicture);

        foreach (var post in model.Posts)
        {
            post.UserPhoto = _imageStorage.UriFor(post.UserPhoto ?? _defaultProfilePicture);
        }

        foreach (var media in model.Posts.SelectMany(p => p.Media))
        {
            media.FilePath = _imageStorage.UriFor(media.FilePath);
        }

        if (model == null)
        {
            return UserNotFoundView();
        }

        if (id != currentUser?.Id)
        {
            model.MutualFriends = await _friendshipService.GetMutualFriendsCountAsync(id, _currentUser.Id);
        }

        var selectedInterests = await _interestService.GetSelectedSelectListAsync(model.Interests);
        ViewData["Interests"] = selectedInterests;

        model.RelationToCurrentUser = _profileService.GetRelationToCurrentUser(currentUser?.Id, id);

        if (model.IsPrivate
            && model.RelationToCurrentUser == RelationTypes.Strangers
            && await _userManager.IsInRoleAsync(currentUser, "Administrator") == false)
        {
            model.Albums = null;
            model.Interests = null;
            return View(model);
        }

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> EditAsync(Guid id)
    {
        ProfileViewModel model = null;
        var currentUser = await _userManager.GetUserAsync(User);

        if (id == Guid.Empty)
        {
            id = currentUser.Id;
        }
        else
        {
            if (id == currentUser.Id)
            {
                model = _profileService.GetEditProfileVM(id);
            }
            else
            {
                if (await _userManager.IsInRoleAsync(currentUser, "Administrator"))
                {
                    model = _profileService.GetEditProfileVM(id);
                }
                else
                {
                    return ForbidView();
                }
            }
        }

        if (model == null)
        {
            return UserNotFoundView();
        }

        ViewData["Counties"] = new SelectList(_countyService.GetAll(), nameof(County.Id), nameof(County.Name));
        ViewData["Cities"] = new SelectList(await _cityService.GetAllAsync(model.CountyId.Value), nameof(City.Id), nameof(City.Name));

        model.Interests = await _interestService.GetByUserAsync(id);
        ViewData["Interests"] = await _interestService.GetSelectedSelectListAsync(model.Interests);

        model.FilePath = _imageStorage.UriFor(model.FilePath);

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditAsync(ProfileViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ViewData["Counties"] = await _countyService.GetSelectListAsync();
            ViewData["Cities"] = await _cityService.GetAllAsync(model.CountyId.Value);

            model.Interests = await _interestService.GetByUserAsync(model.Id);
            ViewData["Interests"] = await _interestService.GetSelectedSelectListAsync(model.Interests);

            return View(model);
        }

        var currentUser = await _userManager.GetUserAsync(User);
        var result = await _profileService.UpdateUser(model);
        var file = model.ProfilePhoto;

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
                            return InternalServerErrorView();
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
            return InternalServerErrorView();
        }

        if (await _userManager.IsInRoleAsync(currentUser, "Administrator") && model.Id != currentUser.Id)
        {
            return RedirectToAction(nameof(UserController.IndexAsync).RemoveAsyncSuffix(),
                nameof(UserController).RemoveControllerSuffix());
        }

        return RedirectToAction(nameof(Index), new { id = currentUser.Id });
    }

    [HttpGet]
    [AllowAnonymous]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult RenderProfilePicture(string id)
    {
        var media = _profileService.GetUserPhoto(id);

        if (media == null)
        {
            return Ok(_imageStorage.UriFor(_defaultProfilePicture));
        }

        return Ok(_imageStorage.UriFor(media));
    }
}