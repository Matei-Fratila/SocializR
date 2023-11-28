namespace SocializR.Services;

public class AlbumService : BaseService
{
    private readonly CurrentUser currentUser;
    private readonly IMapper mapper;

    public AlbumService(CurrentUser currentUser, SocializRUnitOfWork unitOfWork, IMapper mapper)
        : base(unitOfWork)
    {
        this.currentUser = currentUser;
        this.mapper = mapper;
    }

    public Guid GetPostsAlbum()
    {
        var album = UnitOfWork.Albums.Query
            .Where(a => a.Name == "Posts Photos" && a.UserId == currentUser.Id)
            .FirstOrDefault();

        if (album == null)
        {
            album = new Album
            {
                Name = "Posts Photos",
                UserId = currentUser.Id
            };

            UnitOfWork.Albums.Add(album);

            UnitOfWork.SaveChanges();
        }

        return album.Id;
    }

    public List<AlbumVM> GetAll()
    {
        var albums = UnitOfWork.Albums.Query
            .Where(u => u.UserId == currentUser.Id)
            .ProjectTo<AlbumVM>(mapper.ConfigurationProvider)
            .OrderBy(i => i.Name)
            .ToList();

        return albums;
    }

    public EditAlbumVM GetEditAlbumVM(string id)
    {
        return UnitOfWork.Albums.Query
            .Where(a => a.Id.ToString() == id)
            .ProjectTo<EditAlbumVM>(mapper.ConfigurationProvider)
            .FirstOrDefault();
    }

    public bool Add(CreateAlbumVM model)
    {
        UnitOfWork.Albums.Add(new Album
        {
            UserId = currentUser.Id,
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

    public bool Update(CreateAlbumVM model)
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
