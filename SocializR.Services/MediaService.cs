namespace SocializR.Services;

public class MediaService : BaseService
{
    private readonly CurrentUser currentUser;
    private readonly FriendshipService friendshipService;
    private readonly IMapper mapper;

    public MediaService(CurrentUser currentUser, SocializRUnitOfWork unitOfWork, IMapper mapper, FriendshipService friendshipService)
        : base(unitOfWork)
    {
        this.currentUser = currentUser;
        this.friendshipService = friendshipService;
        this.mapper = mapper;
    }

    public bool IsAllowed(bool isAdmin, string id)
    {
        var owner = UnitOfWork.Media.Query
            .Where(m => m.Id == id)
            .Select(m => new
            {
                m.Album.User.Id,
                m.Album.User.IsPrivate,
                m.Album.User.IsDeleted,
            })
            .FirstOrDefault();

        if (owner.IsDeleted == true)
        {
            return false;
        }

        if (owner.Id == currentUser.Id)
        {
            return true;
        }

        if (isAdmin == true)
        {
            return true;
        }

        if (friendshipService.AreFriends(currentUser.Id, owner.Id) == true)
        {
            return true;
        }

        return false;
    }

    public List<MediaModel> GetAll(string id)
    {
        var images = UnitOfWork.Media.Query
            .Where(u => u.AlbumId == id)
            .OrderByDescending(u => u.Id)
            .ProjectTo<MediaModel>(mapper.ConfigurationProvider)
            .ToList();

        return images;
    }

    public string Get(string id)
    {
        if (id == null)
        {
            return null;
        }

        var result = UnitOfWork.Media.Query
            .Where(m => m.Id == id)
            .Select(m => m.FilePath)
            .FirstOrDefault();

        return result;
    }

    public Media Add(string id, string imageId, MediaTypes type)
    {
        if (imageId == null || id == null)
        {
            return null;
        }

        var media = new Media
        {
            Id = imageId,
            FilePath = imageId,
            AlbumId = id,
            Type = type,
            UserId = currentUser.Id
        };

        UnitOfWork.Media.Add(media);

        UnitOfWork.SaveChanges();

        return media;
    }

    public void Update(EditedMediaModel model)
    {
        if (model.Media.Any())
        {
            var images = UnitOfWork.Media.Query
              .Where(a => a.AlbumId == model.Id)
              .ToList();

            foreach (var image in model.Media)
            {
                var picture = images.Find(i => i.Id == image.Id);
                picture.Caption = image.Caption;
            }

            UnitOfWork.SaveChanges();
        }
    }

    public bool Delete(string id)
    {
        var media = UnitOfWork.Media.Query
            .Where(a => a.Id == id)
            .FirstOrDefault();

        if (media == null)
        {
            return false;
        }

        UnitOfWork.Media.Remove(media);

        return UnitOfWork.SaveChanges() != 0;
    }
}
