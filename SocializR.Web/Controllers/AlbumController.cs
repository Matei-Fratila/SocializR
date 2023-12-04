namespace SocializR.Web.Controllers;

[Authorize]
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
        var model = new AlbumsViewModel
        {
            Albums = await _albumService.GetAllAsync(_currentUser.Id)
        };

        foreach (var album in model.Albums)
        {
            album.CoverFilePath = _imageStorage.UriFor(album.CoverFilePath ?? _defaultAlbumCover);
        }

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateAlbumViewModel model)
    {
        if (!ModelState.IsValid)
        {
            RedirectToAction(nameof(Index));
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

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> EditAsync(CreateAlbumViewModel model)
    {
        if (ModelState.IsValid)
        {
            _albumService.Update(model);
            await _unitOfWork.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        _albumService.Remove(id);
        await _unitOfWork.SaveChangesAsync();

        return RedirectToAction("Index", "Album");
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
