using Microsoft.Extensions.Options;

namespace SocializR.Services;

public class CommentService(CurrentUser _currentUser,
    ApplicationUnitOfWork unitOfWork,
    IImageStorage _imageStorage,
    IOptionsMonitor<AppSettings> _appSettings,
    IMapper _mapper) : BaseService<Comment, CommentService>(unitOfWork), ICommentService
{
    private readonly int _commentsPerFirstPage = _appSettings.CurrentValue.CommentsPerFirstPage;
    private readonly int _commentsPerPage = _appSettings.CurrentValue.CommentsPerPage;
    private readonly string _defaultProfilePicture = _appSettings.CurrentValue.DefaultProfilePicture;

    public async Task<List<CommentViewModel>> GetPaginatedAsync(Guid postId, Guid userId, int page)
    {
        var comments = await UnitOfWork.Comments.Query
            .Where(c => c.PostId == postId)
            .OrderByDescending(c => c.CreatedOn)
            .Skip(page > 0 ? _commentsPerFirstPage + (page - 1) * _commentsPerPage : page * _commentsPerFirstPage)
            .Take(page == 0 ? _commentsPerFirstPage : _commentsPerPage)
            .ProjectTo<CommentViewModel>(_mapper.ConfigurationProvider)
            .ToListAsync();

        foreach (var comment in comments)
        {
            comment.UserPhoto = _imageStorage.UriFor(comment.UserPhoto ?? _defaultProfilePicture);
            comment.IsCurrentUserComment = comment.UserId == userId;
        }

        return comments;
    }

    public CommentViewModel GetCurrentUserCommentViewModel(string body)
    {
        return new CommentViewModel
        {
            Body = body,
            FirstName = _currentUser.FirstName,
            LastName = _currentUser.LastName,
            UserPhoto = UnitOfWork.Users.Query
                .Include(u => u.ProfilePhoto)
                .Where(u => u.Id == _currentUser.Id)
                .Select(u => u.ProfilePhoto)
                .FirstOrDefault()?
                .FileName,
            CreatedOn = DateTime.Now.TimeAgo(),
            IsCurrentUserComment = true
        };
    }
}
