namespace SocializR.Services;

public class PostService(CurrentUser _currentUser, 
    ApplicationUnitOfWork _unitOfWork, 
    IMapper _mapper,
    IAlbumService _albumService,
    IImageStorage _imageStorage,
    IMediaService _mediaService,
    ICommentService _commentService) : BaseService<Post, PostService>(_unitOfWork), IPostService
{
    public async Task<List<PostVM>> GetPaginatedAsync(Guid userId, int page, int postsPerPage, int commentsPerPage, 
        string defaultProfilePicture, bool isProfileView = true)
    {
        var posts = await UnitOfWork.Posts.Query
            .Include(p => p.User)
            .Include(p => p.Media)
            .Where(p => isProfileView 
                ? p.UserId == userId
                : p.UserId == userId || p.User.FriendsFirstUser.FirstOrDefault(f => f.SecondUserId == userId && f.FirstUser.IsDeleted == false) != null)
            .OrderByDescending(p => p.CreatedOn)
            .Skip(page * postsPerPage)
            .Take(postsPerPage)
            .ProjectTo<PostVM>(_mapper.ConfigurationProvider)
            .ToListAsync();


        foreach (var post in posts)
        {
            post.Comments = await _commentService.GetPaginatedAsync(post.Id, commentsPerPage, 0, defaultProfilePicture);
            post.UserPhoto = _imageStorage.UriFor(post.UserPhoto ?? defaultProfilePicture);
            post.IsLikedByCurrentUser = post.Likes.Any(l => l.UserId == userId);
        }

        foreach (var comment in posts.SelectMany(p => p.Comments))
        {
            comment.UserPhoto = _imageStorage.UriFor(comment.UserPhoto ?? defaultProfilePicture);
            comment.IsCurrentUserComment = comment.UserId == userId;
        }

        foreach (var media in posts.SelectMany(x => x.Media))
        {
            media.FileName = _imageStorage.UriFor(media.FileName ?? defaultProfilePicture);
        }

        return posts;
    }

    public async Task AddAsync(AddPostViewModel model, string albumName)
    {
        var media = new List<Media>();

        if (model.Media != null)
        {
            foreach (var file in model.Media)
            {
                var album = await _albumService.GetAsync(albumName, _currentUser.Id);

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

    //public void Delete(Guid Id)
    //{
    //    //todod fix npe
    //    var post = UnitOfWork.Posts.Query
    //        .Include(c => c.Likes)
    //        .Include(c => c.Comments)
    //        .Include(c => c.Media)
    //        .Where(p => p.Id == postId)
    //        .FirstOrDefault();

    //    if (post == null)
    //    {
    //        return;
    //    }

    //    UnitOfWork.Posts.Remove(post);

    //    return UnitOfWork.SaveChanges() != 0;
    //}
}
