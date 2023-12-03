namespace SocializR.DataAccess.UnitOfWork;

public class ApplicationUnitOfWork(ApplicationDbContext context) : BaseUnitOfWork(context)
{
    private IBaseRepository<User> _users;
    public IBaseRepository<User> Users
    {
        get
        {
            _users ??= new BaseRepository<User>(DbContext);

            return _users;
        }
    }


    private IBaseRepository<County> _counties;
    public IBaseRepository<County> Counties 
        => _counties ??= new BaseRepository<County>(DbContext);


    private IBaseRepository<City> _cities;
    public IBaseRepository<City> Cities 
        => _cities ??= new BaseRepository<City>(DbContext);


    private IBaseRepository<Media> _media;
    public IBaseRepository<Media> Media 
        => _media ??= new BaseRepository<Media>(DbContext);


    private IBaseRepository<Interest> _interests;
    public IBaseRepository<Interest> Interests 
        => _interests ??= new BaseRepository<Interest>(DbContext);


    private IBaseRepository<UserInterest> _userInterests;
    public IBaseRepository<UserInterest> UserInterests 
        => _userInterests ??= new BaseRepository<UserInterest>(DbContext);


    private IBaseRepository<Album> _albums;
    public IBaseRepository<Album> Albums 
        => _albums ??= new BaseRepository<Album>(DbContext);


    private IBaseRepository<Friendship> _friendships;
    public IBaseRepository<Friendship> Friendships
        => _friendships ??= new BaseRepository<Friendship>(DbContext);


    private IBaseRepository<FriendRequest> _friendRequests;
    public IBaseRepository<FriendRequest> FriendRequests 
        => _friendRequests ??= new BaseRepository<FriendRequest>(DbContext);


    private IBaseRepository<Post> _posts;
    public IBaseRepository<Post> Posts 
        => _posts ??= new BaseRepository<Post>(DbContext);


    private IBaseRepository<Comment> _comments;
    public IBaseRepository<Comment> Comments 
        => _comments ??= new BaseRepository<Comment>(DbContext);


    private IBaseRepository<Like> _likes;
    public IBaseRepository<Like> Likes 
        => _likes ??= new BaseRepository<Like>(DbContext);
}
