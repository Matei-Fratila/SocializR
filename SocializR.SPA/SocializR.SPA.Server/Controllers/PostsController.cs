using AutoMapper;
using SocializR.Models.ViewModels.Feed;
using System.Net;

namespace SocializR.SPA.Server.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class PostsController(ApplicationUnitOfWork _applicationUnitOfWork,
    IImageStorage _imageStorage,
    IPostService _postService,
    IMapper _mapper,
    ILikeService _likeService) : ControllerBase
{
    private readonly ILogger<PostsController> logger;

    [HttpGet]
    [AllowAnonymous]
    public async Task<IResult> GetPaginatedAsync(Guid userId, int pageNumber = 0, bool isProfileView = false)
    {
        Guid? authorizedUserId = null;

        if (User.Identity.IsAuthenticated)
        {
            authorizedUserId = new Guid(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        }

        var postsResult = await _postService.GetPaginatedAsync(userId, 
            authorizedUserId,
            pageNumber, 
            isProfileView: isProfileView);

        return Results.Ok(postsResult);
    }

    [HttpPost]
    public async Task<IResult> CreateAsync([FromForm] AddPostViewModel model)
    {
        var post = await _postService.CreateAsync(model);

        try
        {
            if (!await _applicationUnitOfWork.SaveChangesAsync())
            {
                return Results.StatusCode(500);
            }
        } catch (Exception e)
        {

        }

        var result = new PostViewModel();
        _mapper.Map(post, result);

        foreach (var media in result.Media)
        {
            media.FileName = _imageStorage.UriFor(media.FileName);
        }

        return Results.Created();
    }

    [HttpDelete("{id}")]
    public async Task<IResult> DeleteAsync([FromRoute] Guid id)
    {
        await _postService.DeleteAsync(id);

        if (_applicationUnitOfWork.SaveChanges() == 0)
        {
            return Results.StatusCode(500);
        }

        return Results.NoContent();
    }

    [HttpPost("like/{id}")]
    public async Task<IResult> LikeAsync([FromRoute] Guid id)
    {
        string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        await _likeService.AddLikeAsync(id, new Guid(userId));

        if (_applicationUnitOfWork.SaveChanges() == 0)
        {
            return Results.StatusCode(500);
        }

        return Results.Created();
    }

    [HttpDelete("like/{id}")]
    public async Task<IResult> DeleteLikeAsync([FromRoute] Guid id)
    {
        string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        await _likeService.DeleteLikeAsync(id, new Guid(userId));

        if (_applicationUnitOfWork.SaveChanges() == 0)
        {
            return Results.StatusCode(500);
        }

        return Results.NoContent();
    }
}
