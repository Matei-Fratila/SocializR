using System.Threading.Tasks;

namespace SocializR.Services.Interfaces;
public interface IPostService : IBaseService<Post>
{
    Task AddPost(AddPostViewModel model, string albumName);
    List<PostVM> GetPaginated(Guid userId, int page, int postsPerPage, int commentsPerPage, string defaultProfilePicture);
    bool NotifyProfilePhotoChanged(Media photo, Guid userId);
}
