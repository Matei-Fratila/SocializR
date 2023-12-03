namespace SocializR.Services.Interfaces;
public interface ICommentService : IBaseService<Comment>
{
    #region Read
    List<CommentViewModel> GetPaginated(Guid postId, int commentsPerPage, int page, string defaultProfilePicture);
    #endregion

    CommentViewModel GetCurrentUserCommentViewModel(string body);
}
