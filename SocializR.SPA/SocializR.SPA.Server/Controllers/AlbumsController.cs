using AutoMapper;
using Socializr.Models.ViewModels.Album;
using SocializR.Models.Entities;
using SocializR.Models.Enums;
using SocializR.Models.ViewModels.Album;
using SocializR.Models.ViewModels.Common;
using SocializR.Models.ViewModels.Feed;
using System.Net;

namespace SocializR.SPA.Server.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class AlbumsController(ApplicationUnitOfWork _applicationUnitOfWork,
    CurrentUser _currentUser,
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
        var album = await _albumService.GetViewModelAsync(id);

        foreach (var media in album.Media)
        {
            media.FileName = _imageStorage.UriFor(media.FileName);
        }

        return album;
    }

    [HttpPost]
    public async Task<ActionResult<AlbumViewModel>> CreateAsync([FromForm] CreateAlbumViewModel model)
    {
        var album = new Album 
        { 
            Name = model.Name, 
            UserId = _currentUser.Id 
        };

        for (var i = 0; i < model.Files.Length; i++)
        {
            var type = model.Files[i].ContentType.ToString().Split('/');
            if (type[0] == "image" || type[0] == "video")
            {
                var fileName = await _imageStorage.SaveImage(model.Files[i].OpenReadStream(), type[1]);
                var mediaType = type[0] == "image" ? MediaTypes.Image : MediaTypes.Video;

                album.Media.Add(new Media
                {
                    FileName = fileName,
                    Type = mediaType,
                    UserId = _currentUser.Id,
                    Caption = model.Captions[i]
                });
            }
        }

        _albumService.Add(album);

        if (!await _applicationUnitOfWork.SaveChangesAsync())
        {
            return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
        }

        var result = new AlbumViewModel();
        _mapper.Map(album, result);

        return result;
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await _albumService.DeleteAsync(id);

        if (_applicationUnitOfWork.SaveChanges() == 0)
        {
            return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
        }

        return Ok();
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
    public async Task<IActionResult> UpdateMediaAsync([FromForm] Media model)
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
}
