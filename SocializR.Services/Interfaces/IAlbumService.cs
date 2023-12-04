namespace SocializR.Services.Interfaces;
public interface IAlbumService : IBaseService<Album>
{
    Task<List<AlbumViewModel>> GetAllAsync(Guid userId);
    Album Get(string name, Guid userId);
    EditAlbumViewModel GetEditAlbumVM(Guid id);
    void Update(CreateAlbumViewModel model);
}
