using Utils;

namespace SocializR.Services;

public class CommentService(CurrentUser _currentUser,
    SocializRUnitOfWork unitOfWork,
    IMapper _mapper) : BaseService(unitOfWork)
{
    public string AddComment(Comment comment)
    {
        UnitOfWork.Comments.Add(comment);
        UnitOfWork.SaveChanges();
        return comment.Id.ToString();
    }

    public List<CommentViewModel> GetComments(Guid postId, int commentsPerPage, int page)
        => UnitOfWork.Comments.Query
            .Where(c => c.PostId == postId)
            .OrderByDescending(c => c.CreatedOn)
            .Skip(page * commentsPerPage)
            .Take(commentsPerPage)
            .ProjectTo<CommentViewModel>(_mapper.ConfigurationProvider)
            .ToList();


    public bool DeleteComment(string commentId)
    {
        var comment = UnitOfWork.Comments.Query
            .Where(c => c.Id.ToString() == commentId)
            .FirstOrDefault();

        if (comment == null)
        {
            return false;
        }

        UnitOfWork.Comments.Remove(comment);

        return UnitOfWork.SaveChanges() != 0;
    }

    public CommentViewModel CurrentUserComment(string body)
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
                .FilePath,
            CreatedOn = DateTime.Now.TimeAgo(),
            IsCurrentUserComment = true
        };
    }
}
