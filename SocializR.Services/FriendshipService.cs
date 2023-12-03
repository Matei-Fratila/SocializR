namespace SocializR.Services;

public class FriendshipService(CurrentUser _currentUser, 
    ApplicationUnitOfWork unitOfWork, 
    IMapper _mapper) 
    : BaseService<Friendship, FriendshipService>(unitOfWork), IFriendshipService
{
    public int CountMutualFriends(Guid id)
    {
        return UnitOfWork.Friendships.Query
            .Where(u => u.FirstUserId == _currentUser.Id && u.SecondUser.FriendsFirstUser.Where(f => f.SecondUserId == id).Any())
            .Count();
    }

    public List<UserViewModel> GetAllFriends()
    {
        var friends = UnitOfWork.Friendships.Query
            .Where(u => u.FirstUserId == _currentUser.Id && u.SecondUser.IsDeleted == false)
            .ProjectTo<UserViewModel>(_mapper.ConfigurationProvider)
            .ToList();

        return friends;
    }

    public List<UserViewModel> GetFriends(int pageIndex, int pageSize, out int totalFriendsCount)
    {
        totalFriendsCount = UnitOfWork.Friendships.Query
            .Include(u => u.SecondUser)
            .Where(u => u.FirstUserId == _currentUser.Id 
                && u.SecondUser.IsDeleted == false)
            .Count();

        return UnitOfWork.Friendships.Query
            .Where(u => u.FirstUserId == _currentUser.Id 
                && u.SecondUser.IsDeleted == false)
            .ProjectTo<UserViewModel>(_mapper.ConfigurationProvider)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToList();
    }

    public bool AreFriends(Guid firstUserId, Guid secondUserId)
    {
        return UnitOfWork.Friendships.Query
            .Where(f => (f.FirstUserId == firstUserId && f.SecondUserId == secondUserId ||
            f.FirstUserId == secondUserId && f.SecondUserId == firstUserId) &&
            f.FirstUser.IsDeleted == false && f.SecondUser.IsDeleted == false)
            .Any();
    }

    public bool AddFriend(Guid id)
    {
        var friendships = new List<Friendship>()
        {
            new() {
                FirstUserId = id,
                SecondUserId = _currentUser.Id,
                CreatedDate=DateTime.Now

            },
            new() {
                FirstUserId = _currentUser.Id,
                SecondUserId = id,
                CreatedDate=DateTime.Now
            }

        };

        UnitOfWork.Friendships.AddRange(friendships);

        return UnitOfWork.SaveChanges() != 0;
    }

    public bool Unfriend(string id)
    {
        var requests = UnitOfWork.Friendships.Query
            .Where(f => f.FirstUserId.ToString() == id && f.SecondUserId == _currentUser.Id ||
            f.SecondUserId.ToString() == id && f.FirstUserId == _currentUser.Id)
            .ToList();

        if (requests != null)
        {
            UnitOfWork.Friendships.RemoveRange(requests);
        }

        return UnitOfWork.SaveChanges() != 0;
    }
}
