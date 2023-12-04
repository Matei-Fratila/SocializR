namespace SocializR.Services;

public class AlbumService(ApplicationUnitOfWork unitOfWork, 
    IMapper _mapper) : BaseService<Album, AlbumService>(unitOfWork), IAlbumService
{
    public Album Get(string name, Guid userId)
        => UnitOfWork.Albums.Query
            .Where(a => a.Name == name && a.UserId == userId)
            .FirstOrDefault();

    public async Task<List<AlbumViewModel>> GetAllAsync(Guid userId)
        => await UnitOfWork.Albums.Query
            .Where(u => u.UserId == userId)
            .ProjectTo<AlbumViewModel>(_mapper.ConfigurationProvider)
            .OrderBy(i => i.Name)
            .ToListAsync();

    public EditAlbumViewModel GetEditAlbumVM(Guid id)
    {
        return UnitOfWork.Albums.Query
            .Where(a => a.Id == id)
            .ProjectTo<EditAlbumViewModel>(_mapper.ConfigurationProvider)
            .FirstOrDefault();
    }

    public void Update(CreateAlbumViewModel model)
    {
        var album = Get(model.Id);

        if (album == null)
        {
            return;
        }

        album.Name = model.Name;
    }
}