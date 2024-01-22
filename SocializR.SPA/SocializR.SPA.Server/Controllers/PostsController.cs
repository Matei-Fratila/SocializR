using SocializR.Models.ViewModels.Feed;

namespace SocializR.SPA.Server.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class PostsController(IPostService _postService,
    IOptionsMonitor<AppSettings> _appSettings,
    CurrentUser _currentUser) : ControllerBase
{
    private readonly ILogger<PostsController> logger;
    private readonly int _postsPerPage = _appSettings.CurrentValue.PostsPerPage;
    private readonly int _commentsPerFirstPage = _appSettings.CurrentValue.CommentsPerFirstPage;
    private readonly int _commentsPerPage = _appSettings.CurrentValue.CommentsPerPage;
    private readonly string _defaultProfilePicture = _appSettings.CurrentValue.DefaultProfilePicture;
    private readonly string _defaultPostsAlbumName = _appSettings.CurrentValue.PostsAlbumName;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PostVM>>> GetPaginatedAsync([FromQuery] int page = 0, [FromQuery] int? pageSize = null)
    {
        return await _postService.GetPaginatedAsync(_currentUser.Id, 
            page, 
            pageSize ?? _postsPerPage, 
            _commentsPerFirstPage, 
            _defaultProfilePicture, 
            isProfileView: false
            );
    }
}
