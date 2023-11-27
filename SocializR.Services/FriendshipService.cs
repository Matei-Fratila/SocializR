namespace SocializR.Services;

public class FriendshipService : BaseService
{
    private readonly CurrentUser currentUser;
    private readonly IMapper mapper;

    public FriendshipService(CurrentUser currentUser, SocializRUnitOfWork unitOfWork, IMapper mapper)
        : base(unitOfWork)
    {
        this.currentUser = currentUser;
        this.mapper = mapper;
    }

    public int CountMutualFriends(Guid id)
    {
        return UnitOfWork.Friendships.Query
            .Where(u => u.FirstUserId.ToString() == currentUser.Id && u.SecondUser.FriendsFirstUser.Where(f => f.SecondUserId == id).Any())
            .Count();
    }

    public List<UserVM> GetAllFriends()
    {
        var friends = UnitOfWork.Friendships.Query
            .Where(u => u.FirstUserId.ToString() == currentUser.Id && u.SecondUser.IsDeleted == false)
            .ProjectTo<UserVM>(mapper.ConfigurationProvider)
            .ToList();

        return friends;
    }

    public List<UserVM> GetFriends(int pageIndex, int pageSize, out int totalFriendsCount)
    {
        totalFriendsCount = UnitOfWork.Friendships.Query
            .Include(u => u.SecondUser)
            .Where(u => u.FirstUserId.ToString() == currentUser.Id && u.SecondUser.IsDeleted == false).Count();

        return UnitOfWork.Friendships.Query
            .Where(u => u.FirstUserId.ToString() == currentUser.Id && u.SecondUser.IsDeleted == false)
            .ProjectTo<UserVM>(mapper.ConfigurationProvider)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToList();
    }

    public bool AreFriends(string firstUserId, string secondUserId)
    {
        return UnitOfWork.Friendships.Query
            .Where(f => (f.FirstUserId.ToString() == firstUserId && f.SecondUserId.ToString() == secondUserId ||
            f.FirstUserId.ToString() == secondUserId && f.SecondUserId.ToString() == firstUserId) &&
            f.FirstUser.IsDeleted == false && f.SecondUser.IsDeleted == false)
            .Any();
    }

    public bool AddFriend(string id)
    {
        var friendships = new List<Friendship>()
        {
            new Friendship
            {
                FirstUserId = new Guid(id),
                SecondUserId = new Guid(currentUser.Id),
                CreatedDate=DateTime.Now

            },
            new Friendship
            {
                FirstUserId = new Guid(currentUser.Id),
                SecondUserId = new Guid(id),
                CreatedDate=DateTime.Now
            }

        };

        UnitOfWork.Friendships.AddRange(friendships);

        return UnitOfWork.SaveChanges() != 0;
    }

    public bool Unfriend(string id)
    {
        var requests = UnitOfWork.Friendships.Query
            .Where(f => f.FirstUserId.ToString() == id && f.SecondUserId.ToString() == currentUser.Id ||
            f.SecondUserId.ToString() == id && f.FirstUserId.ToString() == currentUser.Id)
            .ToList();

        if (requests != null)
        {
            UnitOfWork.Friendships.RemoveRange(requests);
        }

        return UnitOfWork.SaveChanges() != 0;
    }
}
