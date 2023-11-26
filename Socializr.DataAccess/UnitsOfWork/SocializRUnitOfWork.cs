namespace SocializR.DataAccess.UnitOfWork;

public class SocializRUnitOfWork : BaseUnitOfWork
{
    public SocializRUnitOfWork(SocializRContext context)
        : base(context)
    {
    }

    private IRepository<User> users;
    public IRepository<User> Users
    {
        get
        {
            if (users == null)
                users = new BaseRepository<User>(DbContext);

            return users;
        }
    }

    private IRepository<County> counties;
    public IRepository<County> Counties => counties ?? (counties = new BaseRepository<County>(DbContext));

    private IRepository<City> cities;
    public IRepository<City> Cities => cities ?? (cities = new BaseRepository<City>(DbContext));

    private IRepository<Media> media;
    public IRepository<Media> Media => media ?? (media = new BaseRepository<Media>(DbContext));

    private IRepository<Interest> interests;
    public IRepository<Interest> Interests => interests ?? (interests = new BaseRepository<Interest>(DbContext));

    private IRepository<UserInterest> userInterests;
    public IRepository<UserInterest> UserInterests => userInterests ?? (userInterests = new BaseRepository<UserInterest>(DbContext));

    private IRepository<Album> albums;
    public IRepository<Album> Albums => albums ?? (albums = new BaseRepository<Album>(DbContext));

    private IRepository<Friendship> friendships;
    public IRepository<Friendship> Friendships=> friendships ?? (friendships = new BaseRepository<Friendship>(DbContext));

    private IRepository<FriendRequest> friendRequests;
    public IRepository<FriendRequest> FriendRequests => friendRequests ?? (friendRequests = new BaseRepository<FriendRequest>(DbContext));

    private IRepository<Post> posts;
    public IRepository<Post> Posts => posts ?? (posts = new BaseRepository<Post>(DbContext));

    private IRepository<Comment> comments;
    public IRepository<Comment> Comments => comments ?? (comments = new BaseRepository<Comment>(DbContext));

    private IRepository<Like> likes;
    public IRepository<Like> Likes => likes ?? (likes = new BaseRepository<Like>(DbContext));
}
