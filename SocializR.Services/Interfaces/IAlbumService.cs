namespace SocializR.Services.Interfaces;
public interface IAlbumService : IBaseService<Album>
{
    List<AlbumViewModel> GetAll();
    Album Get(string name, Guid userId);
    EditAlbumViewModel GetEditAlbumVM(string id);
    bool Create(CreateAlbumViewModel model);
    bool Update(CreateAlbumViewModel model);
    bool Delete(string id);
}
