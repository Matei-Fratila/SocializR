namespace SocializR.Services;

public class LikeService(ApplicationUnitOfWork unitOfWork, CurrentUser _currentUser) : BaseService<Like, LikeService>(unitOfWork), ILikeService
{
    public void AddLike(Guid postId)
    {
        var like = UnitOfWork.Likes.Query
            .Where(l => l.PostId == postId && l.UserId == _currentUser.Id)
            .FirstOrDefault();

        if (like != null)
        {
            return;
        }

        like = new Like
        {
            PostId = postId,
            UserId = _currentUser.Id
        };

        Add(like);

        return;
    }

    public void DeleteLike(Guid postId)
    {
        var like = UnitOfWork.Likes.Query
            .Where(l => l.PostId == postId && l.UserId == _currentUser.Id)
            .FirstOrDefault();

        if (like != null)
        {
            Remove(like);
        }

        return;
    }
}
