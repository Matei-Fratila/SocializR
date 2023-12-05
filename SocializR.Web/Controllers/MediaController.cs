namespace SocializR.Web.Controllers;

[Authorize]
public class MediaController(ApplicationUnitOfWork _unitOfWork,
    IMapper _mapper,
    IAlbumService _albumService,
    IMediaService _mediaService,
    UserManager<User> _userManager,
    CurrentUser _currentUser,
    IImageStorage _imageStorage) : BaseController(_mapper)
{
    [HttpGet]
    public async Task<IActionResult> IndexAsync(Guid id)
    {
        var currentUser = await _userManager.GetUserAsync(User);
        var album = await _albumService.GetViewModelAsync(id);

        if (album == null)
        {
            return NotFoundView();
        }

        if (album.UserId.ToString() != currentUser.Id.ToString() 
            && await _userManager.IsInRoleAsync(currentUser, "Administrator") == false)
        {
            return ForbidView();
        }

        return View(new ImagesViewModel { Album = album });
    }

    [HttpPost]
    public async Task<IActionResult> EditAsync(List<MediaViewModel> model)
    {
        List<Media> entities = new List<Media>();
        _mapper.Map(model, entities);

        _mediaService.UpdateRange(entities);

        if(!await _unitOfWork.SaveChangesAsync())
        {
            return InternalServerErrorView();
        }

        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        _mediaService.Remove(id);

        if (!await _unitOfWork.SaveChangesAsync())
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
        return PartialView("Views/Media/_Media.cshtml", new MediaViewModel() { Id = Guid.Empty, Caption = "" });
    }

    [HttpGet]
    public ActionResult GetVideoWidget()
    {
        return PartialView("Views/Media/_Media.cshtml", new MediaViewModel() { Id = Guid.Empty, Caption = "" });
    }

    [HttpGet]
    public async Task<IActionResult> GetGalleryAsync(Guid id)
    {
        var model = await _mediaService.GetByAlbumAsync(id);
        return PartialView("Views/Media/_Gallery.cshtml", model);
    }

    //[HttpPost]
    //public async Task<JsonResult> UploadAsync(List<IFormFile> media, string albumName)
    //{
    //    var ids = new List<string>();

    //    foreach (var file in media)
    //    {
    //        if (file.Length > 0)
    //        {
    //            var type = file.ContentType.ToString().Split('/');

    //            if (type[0] == "image" || type[0] == "video")
    //            {
    //                try
    //                {
    //                    var imageName = await _imageStorage.SaveImage(file.OpenReadStream(), type[1]);

    //                    var album = await _albumService.GetAsync(albumName, _currentUser.Id);
    //                    if (album == null)
    //                    {
    //                        _albumService.Add(new Album { Name = albumName, UserId = _currentUser.Id });
    //                    }

    //                    var mediaType = type[0] == "image" ? MediaTypes.Image : MediaTypes.Video;
    //                    var result = _mediaService.Add(imageName, mediaType, album);

    //                    if (result == null)
    //                    {
    //                        return Json(false);
    //                    }

    //                    ids.Add(result.Id.ToString());
    //                }
    //                catch (Exception e)
    //                {

    //                }

    //            }
    //            else
    //            {
    //                ids.Add("null");
    //            }
    //        }
    //    }

    //    return Json(ids);
    //}

    [HttpPost]
    public async Task<IActionResult> UploadAsync(List<IFormFile> media, Guid albumId)
    {
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

                        var album = _albumService.Get(albumId);
                        if (album == null)
                        {
                            return NotFound();
                        }

                        var mediaType = type[0] == "image" ? MediaTypes.Image : MediaTypes.Video;
                        var result = _mediaService.Add(imageName, mediaType, album);

                        if (!await _unitOfWork.SaveChangesAsync())
                        {
                            return InternalServerErrorView();
                        }
                    }
                    catch (Exception e)
                    {

                    }

                }
            }
        }

        return Ok();
    }

    [HttpGet]
    public async Task<string> RenderAsync(Guid id)
    {

        if (id == Guid.Empty)
        {
            return _imageStorage.UriFor("default-image.jpg");
        }
        var currentUser = await _userManager.GetUserAsync(User);

        if (await _mediaService.IsAllowed(await _userManager.IsInRoleAsync(currentUser, "Administrator"), id) == false)
        {
            return _imageStorage.UriFor("default-image.jpg");
        }

        var media = _mediaService.Get(id);

        if (media == null)
        {
            return _imageStorage.UriFor("default-image.jpg");
        }

        var path = _imageStorage.UriFor(media.FilePath);

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