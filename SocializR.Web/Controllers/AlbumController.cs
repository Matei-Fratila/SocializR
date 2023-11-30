namespace SocializR.Web.Controllers;

[Authorize]
public class AlbumController(AlbumService _albumService,
    MediaService _mediaService,
    IImageStorage _imageStorage,
    IMapper _mapper,
    IOptionsMonitor<AppSettings> _configuration) : BaseController(_mapper)
{
    [HttpGet]
    public IActionResult Index()
    {
        var model = new AlbumsViewModel
        {
            Albums = _albumService.GetAll()
        };

        foreach (var album in model.Albums)
        {
            album.CoverFilePath = _imageStorage.UriFor(album.CoverFilePath ?? _configuration.CurrentValue.DefaultAlbumCover);
        }

        return View(model);
    }

    [HttpPost]
    public IActionResult Create(CreateAlbumViewModel model)
    {
        if (!ModelState.IsValid)
        {
            RedirectToAction("Index", "Album");
        }

        var result = _albumService.Add(model);

        if (!result)
        {
            return InternalServerErrorView();
        }

        return RedirectToAction("Index", "Album");
    }

    [HttpPost]
    public IActionResult Edit(CreateAlbumViewModel model)
    {
        if (ModelState.IsValid)
        {
            var result = _albumService.Update(model);

            if (!result)
            {
                return RedirectToAction("Index", "Album");
            }
        }

        return RedirectToAction("Index", "Album");
    }

    [HttpPost]
    public IActionResult Delete(string id)
    {
        var isSuccessStatusCode = _albumService.Delete(id);

        return RedirectToAction("Index", "Album");
    }

    [HttpGet]
    [AllowAnonymous]
    public string RenderCoverPicture(string id)
    {
        if (id == null)
        {
            return _imageStorage.UriFor("default-image.jpg");
        }

        var media = _mediaService.Get(id);

        if (media == null)
        {
            return _imageStorage.UriFor("default-image.jpg");
        }

        return _imageStorage.UriFor(media);

    }
}
