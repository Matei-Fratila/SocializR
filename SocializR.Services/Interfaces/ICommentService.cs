namespace SocializR.Services.Interfaces;
public interface ICommentService : IBaseService<Comment>
{
    Task<List<CommentViewModel>> GetPaginatedAsync(Guid postId, int commentsPerPage, int page, string defaultProfilePicture);

    CommentViewModel GetCurrentUserCommentViewModel(string body);
}
