namespace SocializR.Web.Controllers;

[Authorize]
public class MediaController : BaseController
{
    private readonly UserManager<User> userManager;
    private readonly SignInManager<User> signInManager;
    private readonly ProfileService ProfileService;
    private readonly MediaService mediaService;
    private readonly AlbumService albumService;
    private readonly IImageStorage imageStorage;
    private readonly IHostEnvironment hostingEnvironment;

    public MediaController(CurrentUser currentUser, IMapper mapper, AlbumService albumService,
        ProfileService ProfileService, MediaService mediaService, UserManager<User> userManager, SignInManager<User> signInManager,
        IImageStorage imageStorage, IHostEnvironment hostingEnvironment)
        : base(mapper)
    {
        this.hostingEnvironment = hostingEnvironment;
        this.imageStorage = imageStorage;
        this.userManager = userManager;
        this.signInManager = signInManager;
        this.albumService = albumService;
        this.ProfileService = ProfileService;
        this.mediaService = mediaService;
    }


    [HttpGet]
    public async Task<IActionResult> Index(string id)
    {
        var currentUser = await userManager.GetUserAsync(User);
        var album = albumService.GetEditAlbumVM(id);

        if (album == null)
        {
            return NotFoundView();
        }

        if (album.UserId.ToString() != currentUser.Id.ToString() && await userManager.IsInRoleAsync(currentUser, "Administrator") == false)
        {
            return ForbidView();
        }

        return View(new ImagesVM { Album = album });
    }

    [HttpPost]
    public IActionResult Edit(EditedMediaModel model)
    {
        mediaService.Update(model);

        return Ok();
    }

    [HttpPost]
    public IActionResult Delete(string id)
    {
        var result = mediaService.Delete(id);

        if (result == false)
        {
            return InternalServerErrorView();
        }

        return Ok();
    }

    //[HttpGet]
    //public ActionResult GetMediaWidget()
    //{
    //    return PartialView("Views/Media/_Media.cshtml", new MediaModel() { Id = null, Caption = "" });
    //}

    [HttpGet]
    public ActionResult GetImageWidget()
    {
        return PartialView("Views/Media/_Media.cshtml", new MediaModel() { Id = null, Caption = "" });
    }

    [HttpGet]
    public ActionResult GetVideoWidget()
    {
        return PartialView("Views/Media/_Media.cshtml", new MediaModel() { Id = null, Caption = "" });
    }

    [HttpGet]
    public IActionResult GetGallery(string id)
    {
        var model = mediaService.GetAll(id);
        return PartialView("Views/Media/_Gallery.cshtml", model);
    }

    [HttpPost]
    public async Task<JsonResult> Upload(List<IFormFile> media, string id)
    {
        var ids = new List<string>();

        foreach (var file in media)
        {
            if (file.Length > 0)
            {
                var type = file.ContentType.ToString().Split('/');

                if (type[0] == "image" || type[0] == "video")
                {
                    try
                    {
                        var imageId = await imageStorage.SaveImage(file.OpenReadStream(), type[1]);

                        var result = mediaService.Add(id, imageId, type[0] == "image" ? MediaTypes.Image : MediaTypes.Video);

                        if (result == null)
                        {
                            return Json(false);
                        }

                        ids.Add(result.Id.ToString());
                    }
                    catch(Exception e)
                    {
                        
                    }

                }
                else
                {
                    ids.Add("null");
                }
            }
        }

        return Json(ids);
    }

    [HttpGet]
    public async Task<string> Render(string id)
    {

        if (id == null)
        {
            return imageStorage.UriFor("default-image.jpg");
        }
        var currentUser = await userManager.GetUserAsync(User);

        if (mediaService.IsAllowed(await userManager.IsInRoleAsync(currentUser, "Administrator"), id) == false)
        {
            return imageStorage.UriFor("default-image.jpg");
        }

        var media = mediaService.Get(id);

        if (media == null)
        {
            return imageStorage.UriFor("default-image.jpg");
        }

        var path = imageStorage.UriFor(media);

        return path;

        //if (media.Type == MediaTypes.Image)
        //{
        //    return File(media.FilePath, "image/*");
        //}
        //else if (media.Type == MediaTypes.Video)
        //{
        //    return File(media.FilePath, "video/*");
        //}
        //else
        //{
        //    return File("/images/default-image.png", "image/png");
        //}

    }
}