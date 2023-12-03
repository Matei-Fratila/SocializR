namespace SocializR.Services;

public class PostService(CurrentUser _currentUser, 
    ApplicationUnitOfWork unitOfWork, 
    IMapper _mapper,
    IAlbumService _albumService,
    IImageStorage _imageStorage,
    IMediaService _mediaService,
    ICommentService _commentService) : BaseService<Post, PostService>(unitOfWork), IPostService
{
    public List<PostVM> GetPaginated(Guid userId, int page, int postsPerPage, int commentsPerPage, string defaultProfilePicture)
    {
        var posts = UnitOfWork.Posts.Query
            .Include(p => p.User)
            .Include(u => u.Media)
            .Where(p => p.User.FriendsFirstUser.FirstOrDefault(f => f.SecondUserId == _currentUser.Id 
            && f.FirstUser.IsDeleted == false) != null 
            || p.UserId == userId)
            .OrderByDescending(p => p.CreatedOn)
            .Skip(page * postsPerPage)
            .Take(postsPerPage)
            .ProjectTo<PostVM>(_mapper.ConfigurationProvider)
            .ToList();

        //To do: try to move this to mappers
        foreach (var post in posts)
        {
            post.Comments = _commentService.GetPaginated(post.Id, commentsPerPage, 0, defaultProfilePicture);
            post.UserPhoto = _imageStorage.UriFor(post.UserPhoto ?? defaultProfilePicture);
            post.IsCurrentUserPost = post.UserId == _currentUser.Id;
            post.IsLikedByCurrentUser = post.Likes.Any(l => l.UserId == _currentUser.Id);
        }

        foreach (var comment in posts.SelectMany(p => p.Comments))
        {
            comment.UserPhoto = _imageStorage.UriFor(comment.UserPhoto ?? defaultProfilePicture);
            comment.IsCurrentUserComment = comment.UserId == _currentUser.Id;
        }

        foreach (var media in posts.SelectMany(x => x.Media))
        {
            media.FilePath = _imageStorage.UriFor(media.FilePath ?? defaultProfilePicture);
        }

        return posts;
    }

    public async Task AddPost(AddPostViewModel model, string albumName)
    {
        var media = new List<Media>();

        if (model.Media != null)
        {
            foreach (var file in model.Media)
            {
                var album = _albumService.Get(albumName, _currentUser.Id);

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
                        media.Add(_mediaService.Add(album, fileName, type[0] == "image" ? MediaTypes.Image : MediaTypes.Video));
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

    public bool NotifyProfilePhotoChanged(Media photo, Guid userId)
    {
        var post = new Post
        {
            UserId = userId,
            Title = "added a new profile photo",
            Body = "",
            CreatedOn = DateTime.Now,
            Media = new List<Media>
            {
                photo
            }
        };

        UnitOfWork.Posts.Add(post);

        return UnitOfWork.SaveChanges() != 0;
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
