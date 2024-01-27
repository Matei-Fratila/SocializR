namespace SocializR.Services.Interfaces;
public interface IPostService : IBaseService<Post>
{
    Task AddAsync(AddPostViewModel model, string albumName = null);
    Task<Post> CreateAsync(AddPostViewModel model, string albumName = null);
    Task DeleteAsync(Guid id);
    Task<List<PostViewModel>> GetPaginatedAsync(Guid userId, Guid? authorizedUserId, int page, bool isProfileView = true);
}
