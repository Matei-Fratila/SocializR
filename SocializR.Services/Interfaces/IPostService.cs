namespace SocializR.Services.Interfaces;
public interface IPostService : IBaseService<Post>
{
    Task AddAsync(AddPostViewModel model, string albumName);
    Task<List<PostVM>> GetPaginatedAsync(Guid userId, int page, int postsPerPage, 
        int commentsPerPage, string defaultProfilePicture, bool isProfileView = true);
}
