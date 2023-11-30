namespace SocializR.Services;

public class LikeService(SocializRUnitOfWork unitOfWork) : BaseService(unitOfWork)
{
    public bool AddLike(string currentUserId, string postId)
    {
        var like = UnitOfWork.Likes.Query
            .Where(l => l.PostId.ToString() == postId && l.UserId.ToString() == currentUserId)
            .FirstOrDefault();

        if (like != null)
        {
            return false;
        }

        like = new Like
        {
            PostId = new Guid(postId),
            UserId = new Guid(currentUserId)
        };

        UnitOfWork.Likes.Add(like);
        var sucess = UnitOfWork.SaveChanges() != 0;

        return sucess;
    }

    public bool DeleteLike(string currentUserId, string postId)
    {
        var like = UnitOfWork.Likes.Query
            .Where(l => l.PostId.ToString() == postId && l.UserId.ToString() == currentUserId)
            .FirstOrDefault();

        if (like != null)
        {
            UnitOfWork.Likes.Remove(like);
        }

        var sucess = UnitOfWork.SaveChanges() != 0;

        return sucess;
    }
}
