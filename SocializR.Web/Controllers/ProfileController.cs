namespace SocializR.Web.Controllers;

[Authorize]
public class ProfileController(UserManager<User> _userManager,
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

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Index(Guid id)
    {
        ViewProfileViewModel model = null;
        var currentUser = await _userManager.GetUserAsync(User);

        if (id == Guid.Empty)
        {
            id = currentUser.Id;
        }

        model = _profileService.GetViewProfileVM(id);

        model.FilePath = _imageStorage.UriFor(model.FilePath ?? _defaultProfilePicture);
        foreach (var album in model.Albums)
        {
            album.CoverFilePath = _imageStorage.UriFor(album.CoverFilePath ?? _defaultAlbumCover);
        }

        if (model == null)
        {
            return UserNotFoundView();
        }

        if (id != currentUser.Id)
        {
            model.MutualFriends = _friendshipService.CountMutualFriends(id);
        }

        List<SelectListItem> selectedInterests = _interestService.GetAll();

        if (model.Interests.Any())
        {
            foreach (var interest in model.Interests)
            {
                selectedInterests.Single(i => i.Value == interest.ToString()).Selected = true;
            }
        }
        else
        {
            selectedInterests.First().Selected = true;
        }

        ViewData["Interests"] = selectedInterests;

        model.RelationToCurrentUser = _profileService.GetRelationToCurrentUser(currentUser.Id.ToString(), id.ToString());

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
    public async Task<IActionResult> Edit(Guid id)
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
        ViewData["Cities"] = new SelectList(_cityService.GetAll(model.CountyId), nameof(City.Id), nameof(City.Name));

        model.Interests = _interestService.GetByUser(id);
        ViewData["Interests"] = _interestService.GetAllWithSelected(model.Interests);

        model.FilePath = _imageStorage.UriFor(model.FilePath);

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(ProfileViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ViewData["Counties"] = _countyService.GetSelectCounties();
            ViewData["Cities"] = _cityService.GetAllByCounty(model.CountyId);

            model.Interests = _interestService.GetByUser(model.Id);
            ViewData["Interests"] = _interestService.GetAllWithSelected(model.Interests);

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
                    var album = _albumService.Get(_profilePicturesAlbumName, model.Id);
                    if (album == null)
                    {
                        album = new Album { UserId = model.Id, Name = _profilePicturesAlbumName };
                        _albumService.Add(album);
                    }

                    try
                    {
                        var imageName = await _imageStorage.SaveImage(file.OpenReadStream(), type[1]);
                        var image = _mediaService.Add(album, imageName, MediaTypes.Image);
                        _unitOfWork.SaveChanges();

                        if (image == null)
                        {
                            return InternalServerErrorView();
                        }

                        var hasModified = await _profileService.ChangeProfilePhoto(image.Id, model.Id);

                        if (hasModified)
                        {
                            _postService.NotifyProfilePhotoChanged(image, model.Id);
                        }

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
            return RedirectToAction(nameof(UserController.Index), nameof(UserController).RemoveControllerSuffix());
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