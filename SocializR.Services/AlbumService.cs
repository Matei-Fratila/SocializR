namespace SocializR.Services;

public class AlbumService(CurrentUser _currentUser, 
    SocializRUnitOfWork unitOfWork, 
    IMapper _mapper) : BaseService(unitOfWork)
{
    public Guid GetPostsAlbum()
    {
        var album = UnitOfWork.Albums.Query
            .Where(a => a.Name == "Posts Photos" && a.UserId == _currentUser.Id)
            .FirstOrDefault();

        if (album == null)
        {
            album = new Album
            {
                Name = "Posts Photos",
                UserId = _currentUser.Id
            };

            UnitOfWork.Albums.Add(album);
            UnitOfWork.SaveChanges();
        }

        return album.Id;
    }

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

    public bool Add(CreateAlbumViewModel model)
    {
        UnitOfWork.Albums.Add(new Album
        {
            UserId = _currentUser.Id,
            Name = model.Name
        });

        return UnitOfWork.SaveChanges() != 0;
    }

    public Guid Create(Guid userId, string albumName = "Profile Pictures")
    {
        var album = new Album
        {
            UserId = userId,
            Name = albumName
        };

        UnitOfWork.Albums.Add(album);
        UnitOfWork.SaveChanges();

        return album.Id;
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

    public Guid GetIdByUserId(Guid id, string albumName = "Profile Pictures")
    {
        return UnitOfWork.Albums.Query
            .Where(u => u.UserId == id && u.Name == albumName)
            .Select(u => u.Id)
            .FirstOrDefault();
    }
}
