namespace SocializR.Services;

public class AlbumService(CurrentUser _currentUser, 
    ApplicationUnitOfWork unitOfWork, 
    IMapper _mapper) : BaseService<Album, AlbumService>(unitOfWork), IAlbumService
{
    public Album Get(string name, Guid userId)
        => UnitOfWork.Albums.Query
            .Where(a => a.Name == name && a.UserId == userId)
            .FirstOrDefault();

    public List<AlbumViewModel> GetAll()
    {
        var albums = UnitOfWork.Albums.Query
            .Where(u => u.UserId == _currentUser.Id)
            .ProjectTo<AlbumViewModel>(_mapper.ConfigurationProvider)
            .OrderBy(i => i.Name)
            .ToList();

        return albums;
    }

    public EditAlbumViewModel GetEditAlbumVM(string id)
    {
        return UnitOfWork.Albums.Query
            .Where(a => a.Id.ToString() == id)
            .ProjectTo<EditAlbumViewModel>(_mapper.ConfigurationProvider)
            .FirstOrDefault();
    }

    public bool Create(CreateAlbumViewModel model)
    {
        UnitOfWork.Albums.Add(new Album
        {
            UserId = _currentUser.Id,
            Name = model.Name
        });

        return UnitOfWork.SaveChanges() != 0;
    }

    public bool Update(CreateAlbumViewModel model)
    {
        var album = UnitOfWork.Albums.Query
            .Where(a => a.Id.ToString() == model.Id)
            .FirstOrDefault();

        if (album == null)
        {
            return false;
        }

        album.Name = model.Name;
        UnitOfWork.SaveChanges();

        return true;
    }

    public bool Delete(string albumId)
    {
        var album = UnitOfWork.Albums.Query
            .Where(a => a.Id.ToString() == albumId)
            .Include(a => a.Media)
            .FirstOrDefault();

        if (album == null)
        {
            return false;
        }

        UnitOfWork.Albums.Remove(album);
        return UnitOfWork.SaveChanges() != 0;
    }
}