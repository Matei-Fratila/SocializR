namespace SocializR.Services.Interfaces;
public interface IPostService : IBaseService<Post>
{
    Task AddAsync(AddPostViewModel model, string albumName = null);
    Task<List<PostVM>> GetPaginatedAsync(Guid userId, int page, bool isProfileView = true);
}
