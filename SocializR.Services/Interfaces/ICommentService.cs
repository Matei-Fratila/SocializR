namespace SocializR.Services.Interfaces;
public interface ICommentService : IBaseService<Comment>
{
    Task<List<CommentViewModel>> GetPaginatedAsync(Guid postId, Guid? userId, int page);

    CommentViewModel GetCurrentUserCommentViewModel(string body);
}
