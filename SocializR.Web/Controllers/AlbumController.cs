namespace SocializR.Web.Controllers;

public class AlbumController(ApplicationUnitOfWork _unitOfWork,
    CurrentUser _currentUser,
    IOptionsMonitor<AppSettings> _appSettings,
    IMediaService _mediaService,
    IImageStorage _imageStorage,
    IAlbumService _albumService,
    IMapper _mapper) : BaseController(_mapper)
{
    private readonly string _defaultAlbumCover = _appSettings.CurrentValue.DefaultAlbumCover;

    [HttpGet]
    public async Task<IActionResult> IndexAsync()
    {
        var model = await _albumService.GetAllAsync(_currentUser.Id);

        foreach (var album in model)
        {
            album.CoverFilePath = _imageStorage.UriFor(album.CoverFilePath ?? _defaultAlbumCover);
        }

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> CreateAsync()
    {
        var model = new AlbumViewModel { UserId = _currentUser.Id };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(AlbumViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        _albumService.Add(new Album
        {
            UserId = _currentUser.Id,
            Name = model.Name
        });

        if (!await _unitOfWork.SaveChangesAsync())
        {
            return InternalServerErrorView();
        }

        return RedirectToAction(nameof(IndexAsync).RemoveAsyncSuffix());
    }

    [HttpGet]
    public async Task<IActionResult> EditAsync(Guid id)
    {
        var model = await _albumService.GetViewModelAsync(id);

        foreach(var media in model.Media)
        {
            media.FilePath = _imageStorage.UriFor(media.FilePath);
        }

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> EditAsync(AlbumViewModel model)
    {
        if (ModelState.IsValid)
        {
            await _albumService.Update(model);

            if(! await _unitOfWork.SaveChangesAsync())
            {
                return InternalServerErrorView();
            }

            return RedirectToAction(nameof(IndexAsync).RemoveAsyncSuffix());
        }

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        _albumService.Remove(id);
        await _unitOfWork.SaveChangesAsync();

        return RedirectToAction(nameof(IndexAsync).RemoveAsyncSuffix());
    }

    [HttpGet]
    public async Task<IActionResult> DetailsAsync(Guid id)
    {
        var model = await _albumService.GetViewModelAsync(id);

        foreach(var media in model.Media)
        {
            media.FilePath = _imageStorage.UriFor(media.FilePath);
        }

        return View(model);
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<string> RenderCoverPicture(Guid id)
    {
        if (id == Guid.Empty)
        {
            return _imageStorage.UriFor(_defaultAlbumCover);
        }

        var media = await _mediaService.GetAsync(id);

        if (media == null)
        {
            return _imageStorage.UriFor(_defaultAlbumCover);
        }

        return _imageStorage.UriFor(media.FilePath);

    }
}
