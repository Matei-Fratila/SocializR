namespace SocializR.Services.Interfaces;
public interface IAlbumService : IBaseService<Album>
{
    Task<List<AlbumViewModel>> GetAllAsync(Guid userId);
    Task<Album> GetAsync(string name, Guid userId);
    Task<AlbumViewModel> GetViewModelAsync(Guid id);
    void Update(AlbumViewModel model);
}
