using AutoMapper;
using SocializR.Models.ViewModels.Feed;

namespace SocializR.SPA.Server.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class CommentsController(ApplicationUnitOfWork _applicationUnitOfWork,
    IMapper _mapper,
    UserManager<User> _userManager,
    ICommentService _commentService) : ControllerBase
{
    [HttpDelete("{id}")]
    public async Task<IResult> DeleteAsync([FromRoute] Guid id)
    {
        _commentService.Remove(id);

        await _applicationUnitOfWork.SaveChangesAsync();

        return Results.NoContent();
    }

    [HttpPost]
    public async Task<IResult> CreateAsync([FromBody] AddCommentViewModel comment)
    {
        var newComment = new Comment();
        _mapper.Map(comment, newComment);
        newComment.UserId = new Guid(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        newComment.User = _userManager.Users.FirstOrDefault(u => u.Id == newComment.UserId);

        _commentService.Add(newComment);
        await _applicationUnitOfWork.SaveChangesAsync();

        var result = new CommentViewModel();
        _mapper.Map(newComment, result);
        result.IsCurrentUserComment = true;

        return Results.Created("", result);
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IResult> NextCommentsAsync([FromQuery] Guid postId, [FromQuery] int pageNumber)
    {
        Guid? authorizedUserId = null;

        if (User.Identity.IsAuthenticated)
        {
            authorizedUserId = new Guid(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        }

        var comments = await _commentService.GetPaginatedAsync(postId,
            userId: authorizedUserId,
            pageNumber);

        return Results.Ok(comments);
    }
}
