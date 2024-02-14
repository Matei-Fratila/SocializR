using AutoMapper;
using Socializr.Models.ViewModels.Album;
using SocializR.Models.Enums;
using SocializR.Models.ViewModels.Album;
using SocializR.Models.ViewModels.Media;
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
    public async Task<IResult> GetAsync([FromRoute] Guid id)
    {
        var album = await _albumService.GetViewModelAsync(id);

        album.Media = album.Media.OrderByDescending(x => x.CreatedDate).ToList();
        foreach (var media in album.Media)
        {
            media.FileName = _imageStorage.UriFor(media.FileName);
        }

        return Results.Ok(album);
    }

    [HttpPost]
    public async Task<IResult> CreateAsync([FromForm] CreateAlbumViewModel model)
    {
        var album = new Album 
        { 
            Name = model.Name, 
            UserId = _currentUser.Id 
        };

        for (var i = 0; i < model.Files?.Length; i++)
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
            return Results.StatusCode(500);
        }

        var result = new AlbumViewModel();
        _mapper.Map(album, result);

        return Results.CreatedAtRoute(routeName: "/", routeValues: new {result.Id}, value: result);
    }

    [HttpDelete("{id}")]
    public async Task<IResult> DeleteAsync([FromRoute] Guid id)
    {
        await _albumService.DeleteAsync(id);

        if (_applicationUnitOfWork.SaveChanges() == 0)
        {
            return Results.StatusCode(500);
        }

        return Results.NoContent();
    }

    [HttpGet("media/{id}")]
    public async Task<IResult> GetMediaAsync([FromRoute] Guid id)
    {
        var media = await _applicationUnitOfWork.Media.GetAsync(id);

        var model = new MediaViewModel();
        _mapper.Map(media, model);
        model.FileName = _imageStorage.UriFor(media.FileName);

        return Results.Ok(model);
    }

    [HttpDelete("media/{id}")]
    public async Task<IResult> DeleteMediaAsync([FromRoute] Guid id)
    {
        await _mediaService.DeleteAsync(id);

        if (_applicationUnitOfWork.SaveChanges() == 0)
        {
            return Results.StatusCode(500);
        }

        return Results.NoContent();
    }

    [HttpPut("media")]
    public async Task<IResult> UpdateMediaAsync([FromForm] MediaViewModel model)
    {
        var media = await _mediaService.GetAsync(model.Id);
        var album = await _albumService.GetAsync(model.AlbumId);

        media.Caption = model.Caption;
        _mediaService.Update(media);
        _albumService.Update(album);

        if (_applicationUnitOfWork.SaveChanges() == 0)
        {
            return Results.StatusCode(500);
        }

        return Results.NoContent();
    }
}
