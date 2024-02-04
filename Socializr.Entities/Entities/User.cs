namespace SocializR.Models.Entities;

public partial class User : IdentityUser<Guid>, IEntity
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
    public Guid? CityId { get; set; }
    public Guid? ProfilePhotoId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime? BirthDate { get; set; }
    public GenderTypes Gender { get; set; }
    public bool IsPrivate { get; set; }
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? LastModifiedDate { get; set; }
    public byte[] TimeStamp { get; set; }

    public virtual City City { get; set; }
    public virtual Media ProfilePhoto { get; set; }
    public virtual ICollection<Album> Albums { get; set; }
    public virtual ICollection<Comment> Comments { get; set; }
    public virtual ICollection<FriendRequest> FriendRequestsRequestedUser { get; set; }
    public virtual ICollection<FriendRequest> FriendRequestsRequesterUser { get; set; }
    public virtual ICollection<Friendship> FriendsFirstUser { get; set; }
    public virtual ICollection<Friendship> FriendsSecondUser { get; set; }
    public virtual ICollection<Like> Likes { get; set; }
    public virtual ICollection<Media> Media { get; set; }
    public virtual ICollection<Post> Posts { get; set; }
    public virtual ICollection<UserInterest> UserInterests { get; set; }
}
