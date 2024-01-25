using AutoMapper;
using Common.Interfaces;
using SocializR.Models.ViewModels.Feed;
using System.Net;

namespace SocializR.SPA.Server.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class CommentsController(ApplicationUnitOfWork _applicationUnitOfWork,
    IMapper _mapper,
    UserManager<User> _userManager,
    ICommentService _commentService) : ControllerBase
{
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
    {
        _commentService.Remove(id);

        if (!await _applicationUnitOfWork.SaveChangesAsync())
        {
            return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
        }

        return Ok();
    }

    [HttpPost]
    public async Task<CommentViewModel> AddCommentAsync(AddCommentViewModel comment)
    {
        var newComment = new Comment();
        _mapper.Map(comment, newComment);
        newComment.UserId = new Guid(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        newComment.User = _userManager.Users.FirstOrDefault(u => u.Id == newComment.UserId);

        _commentService.Add(newComment);
        if (!await _applicationUnitOfWork.SaveChangesAsync())
        {
            throw new Exception("Ups we could not save the comment");
        }

        var result = new CommentViewModel();
        _mapper.Map(newComment, result);
        result.IsCurrentUserComment = true;

        return result;
    }

    [HttpGet]
    public async Task<List<CommentViewModel>> NextCommentsAsync(Guid postId, int pageNumber)
    {
        var comments = await _commentService.GetPaginatedAsync(postId,
            userId: new Guid(User.FindFirst(ClaimTypes.NameIdentifier)?.Value),
            pageNumber);

        return comments;
    }
}
