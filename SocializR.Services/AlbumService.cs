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

    public string GetPostsAlbum()
    {
        var album = UnitOfWork.Albums.Query
            .Where(a => a.Name == "Posts Photos" && a.UserId.ToString() == currentUser.Id)
            .FirstOrDefault();

        if (album == null)
        {
            album = new Album
            {
                Name = "Posts Photos",
                UserId = new Guid(currentUser.Id)
            };

            UnitOfWork.Albums.Add(album);

            UnitOfWork.SaveChanges();
        }

        return album.Id.ToString();
    }

    public List<AlbumVM> GetAll()
    {
        var albums = UnitOfWork.Albums.Query
            .Where(u => u.UserId.ToString() == currentUser.Id)
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
            UserId = new Guid(currentUser.Id),
            Name = model.Name
        });

        return UnitOfWork.SaveChanges() != 0;
    }

    public string Create(string name, string id)
    {
        var album = new Album
        {
            UserId = new Guid(id),
            Name = name
        };

        UnitOfWork.Albums.Add(album);

        UnitOfWork.SaveChanges();

        return album.Id.ToString();
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

    public string GetId(string name, string id)
    {
        return UnitOfWork.Albums.Query
            .Where(u => u.UserId.ToString() == id && u.Name == "Profile Pictures")
            .Select(u => u.Id.ToString())
            .FirstOrDefault();
    }
}
