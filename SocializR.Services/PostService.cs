using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using SocializR.Models.Entities;

namespace SocializR.Services;

public class PostService(CurrentUser _currentUser, 
    ApplicationUnitOfWork _unitOfWork, 
    IMapper _mapper,
    IAlbumService _albumService,
    IImageStorage _imageStorage,
    IMediaService _mediaService,
    UserManager<User> _userManager,
    IOptionsMonitor<AppSettings> _appSettings,
    ICommentService _commentService) : BaseService<Post, PostService>(_unitOfWork), IPostService
{
    private readonly int _postsPerPage = _appSettings.CurrentValue.PostsPerPage;
    private readonly string _defaultProfilePicture = _appSettings.CurrentValue.DefaultProfilePicture;
    private readonly string _postsAlbumName = _appSettings.CurrentValue.PostsAlbumName;

    public async Task<List<PostViewModel>> GetPaginatedAsync(Guid userId, Guid? authorizedUserId, int page, bool isProfileView = true)
    {
        var posts = await UnitOfWork.Posts.Query
            .Include(p => p.User)
            .Include(p => p.Media)
            .Where(p => isProfileView 
                ? p.UserId == userId
                : p.UserId == userId || p.User.FriendsFirstUser.FirstOrDefault(f => f.SecondUserId == userId && f.FirstUser.IsDeleted == false) != null)
            .OrderByDescending(p => p.CreatedOn)
            .Skip(page * _postsPerPage)
            .Take(_postsPerPage)
            .ProjectTo<PostViewModel>(_mapper.ConfigurationProvider)
            .ToListAsync();

        foreach (var post in posts)
        {
            post.Comments = await _commentService.GetPaginatedAsync(post.Id, userId, 0);
            post.UserPhoto = _imageStorage.UriFor(post.UserPhoto ?? _defaultProfilePicture);
            if(authorizedUserId.HasValue) post.IsLikedByCurrentUser = post.Likes.Any(l => l.UserId == authorizedUserId);
        }

        foreach (var comment in posts.SelectMany(p => p.Comments))
        {
            comment.UserPhoto = _imageStorage.UriFor(comment.UserPhoto ?? _defaultProfilePicture);
            if(authorizedUserId.HasValue) comment.IsCurrentUserComment = comment.UserId == authorizedUserId;
        }

        foreach (var media in posts.SelectMany(x => x.Media))
        {
            media.FileName = _imageStorage.UriFor(media.FileName ?? _defaultProfilePicture);
        }

        return posts;
    }

    public async Task AddAsync(AddPostViewModel model, string albumName = null)
    {
        var media = new List<Media>();

        if (model.Media != null)
        {
            foreach (var file in model.Media)
            {
                var album = await _albumService.GetAsync(albumName ?? _postsAlbumName, _currentUser.Id);

                if (album == null)
                {
                    _albumService.Add(new Album { Name = albumName, UserId = _currentUser.Id });
                }

                if (file.Length > 0)
                {
                    var type = file.ContentType.ToString().Split('/');
                    if (type[0] == "image" || type[0] == "video")
                    {
                        var fileName = await _imageStorage.SaveImage(file.OpenReadStream(), type[1]);
                        var mediaType = type[0] == "image" ? MediaTypes.Image : MediaTypes.Video;

                        media.Add(_mediaService.Add(fileName, mediaType, album));
                    }
                }
            }
        }

        Add(new Post
        {
            UserId = _currentUser.Id,
            Title = model.Title,
            Body = model.Body,
            CreatedOn = DateTime.Now,
            Media = media
        });
    }

    public async Task<Post> CreateAsync(AddPostViewModel model, string albumName = null)
    {
        var media = new List<Media>();

        if (model.Media != null)
        {
            var album = await _albumService.GetAsync(albumName ?? _postsAlbumName, _currentUser.Id);

            if (album == null)
            {
                album = new Album { Name = albumName ?? _postsAlbumName, UserId = _currentUser.Id };
                _albumService.Add(album);
            }

            foreach (var file in model.Media)
            {
                if (file.Length > 0)
                {
                    var type = file.ContentType.ToString().Split('/');
                    if (type[0] == "image" || type[0] == "video")
                    {
                        var fileName = await _imageStorage.SaveImage(file.OpenReadStream(), type[1]);
                        var mediaType = type[0] == "image" ? MediaTypes.Image : MediaTypes.Video;

                        media.Add(_mediaService.Add(fileName, mediaType, album));
                    }
                }
            }
        }

        var post = new Post
        {
            UserId = _currentUser.Id,
            User = _userManager.Users.FirstOrDefault(x => x.Id == _currentUser.Id),
            Title = model.Title,
            Body = model.Body,
            CreatedOn = DateTime.Now,
            Media = media
        };

        Add(post);

        return post;
    }

    public async Task DeleteAsync(Guid id)
    {
        //todod fix npe
        var post = await UnitOfWork.Posts.Query
            .Include(c => c.Likes)
            .Include(c => c.Comments)
            .Include(c => c.Media)
            .Where(p => p.Id == id)
            .FirstOrDefaultAsync();

        if (post == null)
        {
            return;
        }

        Remove(post);

        return;
    }
}
