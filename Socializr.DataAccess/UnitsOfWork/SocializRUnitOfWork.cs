namespace SocializR.DataAccess.UnitOfWork;

public class SocializRUnitOfWork(SocializRContext context) : BaseUnitOfWork(context)
{
    private IRepository<User> _users;
    public IRepository<User> Users
    {
        get
        {
            if (_users == null)
                _users = new BaseRepository<User>(DbContext);

            return _users;
        }
    }

    private IRepository<County> _counties;
    public IRepository<County> Counties 
        => _counties ??= new BaseRepository<County>(DbContext);


    private IRepository<City> _cities;
    public IRepository<City> Cities 
        => _cities ??= new BaseRepository<City>(DbContext);


    private IRepository<Media> _media;
    public IRepository<Media> Media 
        => _media ??= new BaseRepository<Media>(DbContext);


    private IRepository<Interest> _interests;
    public IRepository<Interest> Interests 
        => _interests ??= new BaseRepository<Interest>(DbContext);


    private IRepository<UserInterest> _userInterests;
    public IRepository<UserInterest> UserInterests 
        => _userInterests ??= new BaseRepository<UserInterest>(DbContext);


    private IRepository<Album> _albums;
    public IRepository<Album> Albums 
        => _albums ??= new BaseRepository<Album>(DbContext);


    private IRepository<Friendship> _friendships;
    public IRepository<Friendship> Friendships
        => _friendships ??= new BaseRepository<Friendship>(DbContext);


    private IRepository<FriendRequest> _friendRequests;
    public IRepository<FriendRequest> FriendRequests 
        => _friendRequests ??= new BaseRepository<FriendRequest>(DbContext);


    private IRepository<Post> _posts;
    public IRepository<Post> Posts 
        => _posts ??= new BaseRepository<Post>(DbContext);


    private IRepository<Comment> _comments;
    public IRepository<Comment> Comments 
        => _comments ??= new BaseRepository<Comment>(DbContext);


    private IRepository<Like> _likes;
    public IRepository<Like> Likes 
        => _likes ??= new BaseRepository<Like>(DbContext);
}
