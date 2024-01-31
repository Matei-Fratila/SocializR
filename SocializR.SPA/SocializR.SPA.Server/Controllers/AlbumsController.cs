using AutoMapper;
using SocializR.Models.ViewModels.Album;
using System.Net;

namespace SocializR.SPA.Server.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class AlbumsController(ApplicationUnitOfWork _applicationUnitOfWork,
    IAlbumService _albumService,
    IMediaService _mediaService,
    IImageStorage _imageStorage,
    IMapper _mapper) : ControllerBase
{
    private readonly ILogger<PostsController> logger;

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<ActionResult<AlbumViewModel>> GetAsync(Guid id)
    {
        var album =  await _albumService.GetViewModelAsync(id);

        foreach (var media in album.Media)
        {
            media.FileName = _imageStorage.UriFor(media.FileName);
        }

        return album;
    }

    [HttpGet("media/{id}")]
    public async Task<ActionResult<Media>> GetMediaAsync(Guid id)
    {
        var media = await _applicationUnitOfWork.Media.GetAsync(id);
        media.FileName = _imageStorage.UriFor(media.FileName);

        return media;
    }

    [HttpDelete("media/{id}")]
    public async Task<IActionResult> DeleteMediaAsync(Guid id)
    {
        await _mediaService.DeleteAsync(id);

        if (_applicationUnitOfWork.SaveChanges() == 0)
        {
            return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
        }

        return Ok();
    }

    [HttpPut("media")]
    public async Task<IActionResult> DeleteMediaAsync([FromForm]Media model)
    {
        var media = await _mediaService.GetAsync(model.Id);

        media.Caption = model.Caption;
        _mediaService.Update(media);

        if (_applicationUnitOfWork.SaveChanges() == 0)
        {
            return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
        }

        return Ok();
    }

    //[HttpPost]
    //public async Task<ActionResult<PostViewModel>> CreateAsync([FromForm]AddPostViewModel model)
    //{
    //    var post = await _albumService.createAsync(model);

    //    try
    //    {
    //        if (!await _applicationUnitOfWork.SaveChangesAsync())
    //        {
    //            return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
    //        }
    //    } catch (Exception e)
    //    {

    //    }

    //    var result = new PostViewModel();
    //    _mapper.Map(post, result);

    //    return result;
    //}

    //[HttpDelete("delete/{id}")]
    //public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
    //{
    //    await _albumService.DeleteAsync(id);

    //    if (_applicationUnitOfWork.SaveChanges() == 0)
    //    {
    //        return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
    //    }

    //    return Ok();
    //}
}
