namespace SocializR.Services;

public class MediaService(CurrentUser _currentUser,
    ApplicationUnitOfWork unitOfWork,
    IMapper _mapper,
    IFriendshipService _friendshipService) : BaseService<Media, MediaService>(unitOfWork), IMediaService
{
    public async Task<bool> IsAllowed(bool isAdmin, Guid id)
    {
        var owner = await UnitOfWork.Media.Query
            .Where(m => m.Id == id)
            .Select(m => new
            {
                m.Album.User.Id,
                m.Album.User.IsPrivate,
                m.Album.User.IsDeleted,
            })
            .FirstOrDefaultAsync();

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

        if (await _friendshipService.AreFriendsAsync(_currentUser.Id, owner.Id))
        {
            return true;
        }

        return false;
    }

    public async Task<List<MediaViewModel>> GetByAlbumAsync(Guid id)
        => await UnitOfWork.Media.Query
            .Where(u => u.AlbumId == id)
            .OrderByDescending(u => u.Id)
            .ProjectTo<MediaViewModel>(_mapper.ConfigurationProvider)
            .ToListAsync();

    public Media Add(string fileName, MediaTypes type, Album album, Post associatedPost = null)
    {
        if (fileName == null || album == null)
        {
            return null;
        }

        var media = new Media
        {
            FileName = fileName,
            Album = album,
            Type = type,
            UserId = _currentUser.Id,
            Post = associatedPost
        };

        Add(media);

        return media;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var media = await UnitOfWork.Media.Query.FirstOrDefaultAsync(m => m.Id == id);

        if(media == null) 
        { 
            return false; 
        }

        Remove(media);

        return true;
    }
}
