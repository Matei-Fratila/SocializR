namespace SocializR.Services;

public class CommentService : BaseService
{
    private readonly IMapper mapper;
    private readonly CurrentUser currentUser;

    public CommentService(CurrentUser currentUser, SocializRUnitOfWork unitOfWork, IMapper mapper)
        : base(unitOfWork)
    {
        this.mapper = mapper;
        this.currentUser = currentUser;
    }

    public string AddComment(Comment comment)
    {
        UnitOfWork.Comments.Add(comment);

        UnitOfWork.SaveChanges();

        return comment.Id.ToString();
    }

    public List<CommentVM> GetComments(Guid postId, int commentsPerPage, int page)
    {
        return UnitOfWork.Comments.Query
            .Where(c => c.PostId == postId)
            .OrderByDescending(c => c.CreatedOn)
            .Skip(page * commentsPerPage)
            .Take(commentsPerPage)
            .ProjectTo<CommentVM>(mapper.ConfigurationProvider)
            .ToList();
    }

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

    public CommentVM CurrentUserComment(string body)
    {
        return new CommentVM
        {
            Body = body,
            FirstName = currentUser.FirstName,
            LastName = currentUser.LastName,
            UserPhoto = UnitOfWork.Users.Query.Where(u => u.Id == currentUser.Id).Select(u => u.ProfilePhotoId).FirstOrDefault().ToString(),
            CreatedOn = DateTime.Now
        };
    }
}
