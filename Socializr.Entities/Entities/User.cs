namespace SocializR.Entities;

public partial class User : IdentityUser, IEntity
{
    public User()
    {
        Albums = new HashSet<Album>();
        Comments = new HashSet<Comment>();
        FriendRequestsRequestedUser = new HashSet<FriendRequest>();
        FriendRequestsRequesterUser = new HashSet<FriendRequest>();
        FriendsFirstUser = new HashSet<Friendship>();
        FriendsSecondUser = new HashSet<Friendship>();
        Likes = new HashSet<Like>();
        Media = new HashSet<Media>();
        Posts = new HashSet<Post>();
        UserInterests = new HashSet<UserInterest>();
    }
    public string CityId { get; set; }
    public string ProfilePhotoId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime? BirthDate { get; set; }
    public GenderTypes Gender { get; set; }
    public bool IsPrivate { get; set; }
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedOn { get; set; }

    public City City { get; set; }
    public Media ProfilePhoto { get; set; }
    public ICollection<Album> Albums { get; set; }
    public ICollection<Comment> Comments { get; set; }
    public ICollection<FriendRequest> FriendRequestsRequestedUser { get; set; }
    public ICollection<FriendRequest> FriendRequestsRequesterUser { get; set; }
    public ICollection<Friendship> FriendsFirstUser { get; set; }
    public ICollection<Friendship> FriendsSecondUser { get; set; }
    public ICollection<Like> Likes { get; set; }
    public ICollection<Media> Media { get; set; }
    public ICollection<Post> Posts { get; set; }
    public ICollection<UserInterest> UserInterests { get; set; }
}
