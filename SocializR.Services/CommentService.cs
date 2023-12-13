namespace SocializR.Services;

public class CommentService(CurrentUser _currentUser,
    ApplicationUnitOfWork unitOfWork,
    IImageStorage _imageStorage,
    IMapper _mapper) : BaseService<Comment, CommentService>(unitOfWork), ICommentService
{
    public async Task<List<CommentViewModel>> GetPaginatedAsync(Guid postId, int commentsPerPage, int page, string defaultProfilePicture)
    {
        var comments = await UnitOfWork.Comments.Query
            .Where(c => c.PostId == postId)
            .OrderByDescending(c => c.CreatedOn)
            .Skip(page * commentsPerPage)
            .Take(commentsPerPage)
            .ProjectTo<CommentViewModel>(_mapper.ConfigurationProvider)
            .ToListAsync();

        //To do: try to map this
        foreach (var comment in comments)
        {
            comment.UserPhoto = _imageStorage.UriFor(comment.UserPhoto ?? defaultProfilePicture);
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
