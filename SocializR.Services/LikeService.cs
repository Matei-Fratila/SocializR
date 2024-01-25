namespace SocializR.Services;

public class LikeService(ApplicationUnitOfWork unitOfWork, 
    CurrentUser _currentUser) : BaseService<Like, LikeService>(unitOfWork), ILikeService
{
    public async Task AddLikeAsync(Guid postId, Guid? userId = null)
    {
        var like = await UnitOfWork.Likes.Query
            .Where(l => l.PostId == postId && l.UserId == (userId ?? _currentUser.Id))
            .FirstOrDefaultAsync();

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

    public async Task DeleteLikeAsync(Guid postId, Guid? userId = null)
    {
        var like = await UnitOfWork.Likes.Query
            .Where(l => l.PostId == postId && l.UserId == (userId ?? _currentUser.Id))
            .FirstOrDefaultAsync();

        if (like != null)
        {
            Remove(like);
        }

        return;
    }
}
