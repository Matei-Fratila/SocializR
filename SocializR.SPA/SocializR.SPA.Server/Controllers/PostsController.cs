using AutoMapper;
using Socializr.Models.ViewModels.Paging;
using SocializR.Models.ViewModels.Feed;

namespace SocializR.SPA.Server.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class PostsController(ApplicationUnitOfWork _applicationUnitOfWork,
    ILogger<PostsController> _logger,
    IOptionsMonitor<AppSettings> _appSettings,
    IImageStorage _imageStorage,
    IPostService _postService,
    IMapper _mapper,
    ILikeService _likeService) : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    public async Task<IResult> GetAsync(Guid userId, int? pageIndex, int? pageSize, bool isProfileView = false)
    {
        Guid authorizedUserId = Guid.Empty;

        if (User.Identity.IsAuthenticated)
        {
            authorizedUserId = new Guid(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        }

        var postsResult = await _postService.GetPaginatedAsync(new PostsPagingDto
        {
            UserId = userId,
            AuthorizedUserId = authorizedUserId,
            PageIndex = pageIndex ?? 1,
            PageSize = pageSize ?? _appSettings.CurrentValue.PostsPerPage,
            IsProfileView = isProfileView
        });

        using (_logger.BeginScope("Getting posts with page index {pageIndex} and page size {pageSize}", pageIndex ?? 1, pageSize ?? _appSettings.CurrentValue.PostsPerPage))
        {
            _logger.LogInformation("Received {count} countires from the query", postsResult.Count);
        }

        return Results.Ok(postsResult);
    }

    [HttpPost]
    public async Task<IResult> CreateAsync([FromForm] AddPostViewModel model)
    {
        var post = await _postService.CreateAsync(model);
        await _applicationUnitOfWork.SaveChangesAsync();

        var result = new PostViewModel();
        _mapper.Map(post, result);

        foreach (var media in result.Media)
        {
            media.FileName = _imageStorage.UriFor(media.FileName);
        }

        return Results.Created("", result);
    }

    [HttpDelete("{id}")]
    public async Task<IResult> DeleteAsync([FromRoute] Guid id)
    {
        await _postService.DeleteAsync(id);
        await _applicationUnitOfWork.SaveChangesAsync();

        return Results.NoContent();
    }

    [HttpPost("like/{id}")]
    public async Task<IResult> LikeAsync([FromRoute] Guid id)
    {
        string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        await _likeService.AddLikeAsync(id, new Guid(userId));
        await _applicationUnitOfWork.SaveChangesAsync();

        return Results.Created();
    }

    [HttpDelete("like/{id}")]
    public async Task<IResult> DeleteLikeAsync([FromRoute] Guid id)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier);

        if (userId is null)
        {
            return Results.NotFound();
        }

        await _likeService.DeleteLikeAsync(id, new Guid(userId.Value));
        await _applicationUnitOfWork.SaveChangesAsync();

        return Results.NoContent();
    }
}
