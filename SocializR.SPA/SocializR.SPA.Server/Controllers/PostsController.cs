using SocializR.Models.ViewModels.Feed;
using System.Net;

namespace SocializR.SPA.Server.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class PostsController(ApplicationUnitOfWork _applicationUnitOfWork,
    IPostService _postService,
    ILikeService _likeService) : ControllerBase
{
    private readonly ILogger<PostsController> logger;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PostVM>>> GetPaginatedAsync(int pageNumber = 0)
    {
        string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        return await _postService.GetPaginatedAsync(new Guid(userId), 
            pageNumber, 
            isProfileView: false
            );
    }

    [HttpPost("like/{id}")]
    public async Task<IActionResult> LikeAsync([FromRoute]Guid id)
    {
        string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        await _likeService.AddLikeAsync(id, new Guid(userId));

        if (_applicationUnitOfWork.SaveChanges() == 0)
        {
            return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
        }

        return Ok();
    }

    [HttpDelete("like/delete/{id}")]
    public async Task<IActionResult> DeleteLikeAsync([FromRoute]Guid id)
    {
        string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        await _likeService.DeleteLikeAsync(id, new Guid(userId));

        if (_applicationUnitOfWork.SaveChanges() == 0)
        {
            return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
        }

        return Ok();
    }
}
