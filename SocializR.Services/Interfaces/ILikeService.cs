namespace SocializR.Services.Interfaces;
public interface ILikeService : IBaseService<Like>
{
    void AddLike(Guid postId);
    void DeleteLike(Guid postId);
}
