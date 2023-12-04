namespace SocializR.Services.Interfaces;
public interface ILikeService : IBaseService<Like>
{
    Task AddLikeAsync(Guid postId);
    Task DeleteLikeAsync(Guid postId);
}
