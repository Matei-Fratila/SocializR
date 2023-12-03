namespace SocializR.Web.Controllers;

[Authorize]
public class MediaController(IMapper _mapper,
    IAlbumService _albumService,
    IMediaService _mediaService,
    UserManager<User> _userManager,
    CurrentUser _currentUser,
    IImageStorage _imageStorage) : BaseController(_mapper)
{
    [HttpGet]
    public async Task<IActionResult> Index(string id)
    {
        var currentUser = await _userManager.GetUserAsync(User);
        var album = _albumService.GetEditAlbumVM(id);

        if (album == null)
        {
            return NotFoundView();
        }

        if (album.UserId.ToString() != currentUser.Id.ToString() && await _userManager.IsInRoleAsync(currentUser, "Administrator") == false)
        {
            return ForbidView();
        }

        return View(new ImagesViewModel { Album = album });
    }

    [HttpPost]
    public IActionResult Edit(EditedMediaViewModel model)
    {
        _mediaService.Update(model);

        return Ok();
    }

    [HttpPost]
    public IActionResult Delete(string id)
    {
        var result = _mediaService.Delete(id);

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
        return PartialView("Views/Media/_Media.cshtml", new MediaViewModel() { Id = null, Caption = "" });
    }

    [HttpGet]
    public ActionResult GetVideoWidget()
    {
        return PartialView("Views/Media/_Media.cshtml", new MediaViewModel() { Id = null, Caption = "" });
    }

    [HttpGet]
    public IActionResult GetGallery(string id)
    {
        var model = _mediaService.GetAll(id);
        return PartialView("Views/Media/_Gallery.cshtml", model);
    }

    [HttpPost]
    public async Task<JsonResult> Upload(List<IFormFile> media, Guid id, string albumName)
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
                        var imageName = await _imageStorage.SaveImage(file.OpenReadStream(), type[1]);

                        var album = _albumService.Get(albumName, _currentUser.Id);
                        if (album == null)
                        {
                            _albumService.Add(new Album { Name = albumName, UserId = _currentUser.Id });
                        }

                        var result = _mediaService.Add(album, imageName, type[0] == "image" ? MediaTypes.Image : MediaTypes.Video);

                        if (result == null)
                        {
                            return Json(false);
                        }

                        ids.Add(result.Id.ToString());
                    }
                    catch (Exception e)
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
            return _imageStorage.UriFor("default-image.jpg");
        }
        var currentUser = await _userManager.GetUserAsync(User);

        if (_mediaService.IsAllowed(await _userManager.IsInRoleAsync(currentUser, "Administrator"), id) == false)
        {
            return _imageStorage.UriFor("default-image.jpg");
        }

        var media = _mediaService.Get(id);

        if (media == null)
        {
            return _imageStorage.UriFor("default-image.jpg");
        }

        var path = _imageStorage.UriFor(media);

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