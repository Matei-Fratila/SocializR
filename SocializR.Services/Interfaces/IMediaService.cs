namespace SocializR.Services.Interfaces;
public interface IMediaService : IBaseService<Media>
{
    Task<bool> IsAllowed(bool isAdmin, Guid id);
    Task<List<MediaViewModel>> GetByAlbumAsync(Guid id);
    Media Add(string fileName, MediaTypes type, Album album, Post associatedPost = null);
}
