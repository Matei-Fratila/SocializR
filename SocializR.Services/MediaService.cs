namespace SocializR.Services;

public class MediaService(CurrentUser _currentUser, 
    ApplicationUnitOfWork unitOfWork, 
    IMapper _mapper, 
    IFriendshipService _friendshipService) : BaseService<Media, MediaService>(unitOfWork), IMediaService
{
    public bool IsAllowed(bool isAdmin, string id)
    {
        var owner = UnitOfWork.Media.Query
            .Where(m => m.Id.ToString() == id)
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

        if (owner.Id == _currentUser.Id)
        {
            return true;
        }

        if (isAdmin == true)
        {
            return true;
        }

        if (_friendshipService.AreFriends(_currentUser.Id, owner.Id) == true)
        {
            return true;
        }

        return false;
    }

    public List<MediaViewModel> GetAll(string id)
    {
        var images = UnitOfWork.Media.Query
            .Where(u => u.AlbumId.ToString() == id)
            .OrderByDescending(u => u.Id)
            .ProjectTo<MediaViewModel>(_mapper.ConfigurationProvider)
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
            .Where(m => m.Id.ToString() == id)
            .Select(m => m.FilePath)
            .FirstOrDefault();

        return result;
    }

    public Media Add(Album album, string fileName, MediaTypes type, Post associatedPost = null)
    {
        if (fileName == null || album == null)
        {
            return null;
        }

        associatedPost ??= new Post
            {
                Body = $"{_currentUser.FirstName} {_currentUser.LastName} uploaded a new profile picture",
                UserId = _currentUser.Id,
                CreatedOn = DateTime.Now
            };

        var media = new Media
        {
            FilePath = fileName,
            Album = album,
            Type = type,
            UserId = _currentUser.Id,
            Post = associatedPost
        };

        UnitOfWork.Media.Add(media);

        return media;
    }

    public void Update(EditedMediaViewModel model)
    {
        if (model.Media.Any())
        {
            var images = UnitOfWork.Media.Query
              .Where(a => a.AlbumId.ToString() == model.Id)
              .ToList();

            foreach (var image in model.Media)
            {
                var picture = images.Find(i => i.Id.ToString() == image.Id);
                picture.Caption = image.Caption;
            }

            UnitOfWork.SaveChanges();
        }
    }

    public bool Delete(string id)
    {
        var media = UnitOfWork.Media.Query
            .Where(a => a.Id.ToString() == id)
            .FirstOrDefault();

        if (media == null)
        {
            return false;
        }

        UnitOfWork.Media.Remove(media);

        return UnitOfWork.SaveChanges() != 0;
    }
}
