namespace SocializR.Services.Interfaces;
public interface IMediaService : IBaseService<Media>
{
    bool IsAllowed(bool isAdmin, string id);
    List<MediaViewModel> GetAll(string id);
    string Get(string id);
    Media Add(Album album, string fileName, MediaTypes type, Post associatedPost = null);
    void Update(EditedMediaViewModel model);
    bool Delete(string id);
}
