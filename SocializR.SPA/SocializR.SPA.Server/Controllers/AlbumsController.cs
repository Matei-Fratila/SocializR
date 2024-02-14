using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Socializr.Models.ViewModels.Album;
using SocializR.Models.Enums;
using SocializR.Models.ViewModels.Album;
using SocializR.Models.ViewModels.Media;

namespace SocializR.SPA.Server.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class AlbumsController(ApplicationUnitOfWork _applicationUnitOfWork,
    CurrentUser _currentUser,
    IAlbumService _albumService,
    IMediaService _mediaService,
    IImageStorage _imageStorage,
    IMapper _mapper) : ControllerBase
{
    [HttpGet("{id}", Name = "AlbumById")]
    [AllowAnonymous]
    public async Task<Results<Ok<AlbumViewModel>, NotFound>> GetAsync([FromRoute] Guid id)
    {
        var album = await _albumService.GetViewModelAsync(id);

        if (album is null)
        {
            return TypedResults.NotFound();
        }

        album.Media = album.Media.OrderByDescending(x => x.CreatedDate).ToList();

        foreach (var media in album.Media)
        {
            media.FileName = _imageStorage.UriFor(media.FileName);
        }

        return TypedResults.Ok(album);
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

        await _applicationUnitOfWork.SaveChangesAsync();

        var result = new AlbumViewModel();
        _mapper.Map(album, result);

        return Results.CreatedAtRoute(routeName: "AlbumById", routeValues: new {result.Id}, value: result);
    }

    [HttpDelete("{id}")]
    public async Task<Results<NotFound, NoContent>> DeleteAsync([FromRoute] Guid id)
    {
        if(!await _albumService.DeleteAsync(id))
        {
            return TypedResults.NotFound();
        }

        await _applicationUnitOfWork.SaveChangesAsync();

        return TypedResults.NoContent();
    }

    [HttpGet("media/{id}")]
    public async Task<Results<Ok<MediaViewModel>, NotFound>> GetMediaAsync([FromRoute] Guid id)
    {
        var media = await _applicationUnitOfWork.Media.GetAsync(id);

        if(media is null)
        {
            return TypedResults.NotFound();
        }

        var model = new MediaViewModel();
        _mapper.Map(media, model);
        model.FileName = _imageStorage.UriFor(media.FileName);

        return TypedResults.Ok(model);
    }

    [HttpDelete("media/{id}")]
    public async Task<IResult> DeleteMediaAsync([FromRoute] Guid id)
    {
        if(await _mediaService.DeleteAsync(id))
        {
            return Results.NotFound();
        }

        await _applicationUnitOfWork.SaveChangesAsync();

        return Results.NoContent();
    }

    [HttpPut("media")]
    public async Task<IResult> UpdateMediaAsync([FromForm] MediaViewModel model)
    {
        var media = await _mediaService.GetAsync(model.Id);

        if(media is null)
        {
            return Results.NotFound();
        }

        var album = await _albumService.GetAsync(model.AlbumId);

        if(album is null)
        {
            return Results.NotFound();
        }

        media.Caption = model.Caption;
        _mediaService.Update(media);
        _albumService.Update(album);

        await _applicationUnitOfWork.SaveChangesAsync();

        return Results.NoContent();
    }
}
