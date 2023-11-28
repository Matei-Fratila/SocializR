namespace SocializR.Web.Controllers;

[Authorize]
public class ProfileController : BaseController
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly ProfileService _profileService;
    private readonly CountyService _countyService;
    private readonly CityService _cityService;
    private readonly InterestService _interestService;
    private readonly CurrentUser _currentUser;
    private readonly MediaService _mediaService;
    private readonly AlbumService _albumService;
    private readonly FriendshipService _friendshipService;
    private readonly PostService _postService;
    private readonly IHostEnvironment _hostingEnvironment;
    private readonly IImageStorage _imageStorage;

    public ProfileController(IHostEnvironment hostingEnvironment, 
        PostService postService, 
        CurrentUser currentUser,
        FriendshipService friendshipService, 
        AlbumService albumService, 
        MediaService mediaService, 
        ProfileService profileService,
        CountyService countyService, 
        CityService cityService, 
        InterestService interestService, 
        IMapper mapper,
        UserManager<User> userManager, 
        SignInManager<User> signInManager, 
        IImageStorage imageStorage)
        : base(mapper)
    {
        _imageStorage = imageStorage;
        _userManager = userManager;
        _signInManager = signInManager;
        _friendshipService = friendshipService;
        _albumService = albumService;
        _mediaService = mediaService;
        _hostingEnvironment = hostingEnvironment;
        _currentUser = currentUser;
        _profileService = profileService;
        _countyService = countyService;
        _cityService = cityService;
        _interestService = interestService;
        _postService = postService;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Index(Guid id)
    {
        ViewProfileVM model = null;
        var currentUser = await _userManager.GetUserAsync(User);

        if (id == Guid.Empty)
        {
            id = currentUser.Id;
        }

        model = _profileService.GetViewProfileVM(id);
        model.FilePath = _imageStorage.UriFor(model.FilePath);

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
        ProfileVM model = null;
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
    public async Task<IActionResult> Edit(ProfileVM model)
    {
        if (!ModelState.IsValid)
        {
            ViewData["Counties"] = _countyService.GetSelectCounties();
            ViewData["Cities"] = _cityService.GetCities(model.CountyId);

            model.Interests = _interestService.GetByUser(model.Id);
            ViewData["Interests"] = _interestService.GetAllWithSelected(model.Interests);

            return View(model);
        }

        var currentUser = await _userManager.GetUserAsync(User);
        var result = _profileService.UpdateUser(model);
        var file = model.ProfilePhoto;

        if (file != null && result)
        {
            var uploads = Path.Combine(_hostingEnvironment.ContentRootPath, @"wwwroot\images\uploads");
            var type = file.ContentType.ToString().Split('/');

            if (file.Length > 0)
            {
                if (type[0] == "image")
                {
                    var albumId = _albumService.GetIdByUserId(model.Id);
                    if (albumId == Guid.Empty)
                    {
                        albumId = _albumService.Create(model.Id);
                    }

                    try
                    {
                        var imageName = await _imageStorage.SaveImage(file.OpenReadStream(), type[1]);
                        var image = _mediaService.Add(albumId, imageName, MediaTypes.Image);

                        if (image == null)
                        {
                            return InternalServerErrorView();
                        }

                        var hasModified = _profileService.ChangeProfilePhoto(image.Id, model.Id);

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
            return RedirectToAction("Index", "User");
        }

        return RedirectToAction("Index", "Profile", new { id = currentUser.Id });
    }

    [HttpGet]
    [AllowAnonymous]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult RenderProfilePicture(string id)
    {
        var media = _profileService.GetUserPhoto(id);

        if (media == null)
        {
            return Ok(_imageStorage.UriFor("noprofile.png"));
        }

        return Ok(_imageStorage.UriFor(media));
    }
}