namespace SocializR.Services.Interfaces;
public interface ILikeService : IBaseService<Like>
{
    Task AddLikeAsync(Guid postId, Guid? userId = null);
    Task DeleteLikeAsync(Guid postId, Guid? userId = null);
}
